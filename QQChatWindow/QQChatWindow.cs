using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Automation;
using System.Threading;
using System.Windows.Forms;

namespace QQHelper
{
    public enum SendMessageHotKey
    {
        ENTER = 0,
        CTR_LENTER = 1
    }
    /// <summary>
    /// QQ聊天窗口类
    /// </summary>
    public class QQChatWindow
    {
        private IntPtr _QQWindowHandle;//QQ聊天窗口的句柄
        private string _winTitle;//聊天窗口标题
        public QQChatWindow(string tittle)
        {
            _winTitle = tittle;
            //_QQWindowHandle = Win32.FindWindow(null, tittle);
        }
        /// <summary>
        /// 查找控件
        /// </summary>
        /// <param name="winName">窗口名</param>
        /// <param name="controlName">控件明</param>
        /// <returns>AE</returns>
        private AutomationElement findElement(string winName, string controlName)
        {
            AutomationElement aeEdit = null;
            Process[] process = Process.GetProcessesByName("QQ");
            if (process.Length < 1) throw new ArgumentNullException();//没有找到QQ进程
            //获取根节点
            AutomationElement aeTop = AutomationElement.RootElement;
            foreach (Process p in process)
            {
                if (p.MainWindowHandle != null)
                {
                    //查找窗体名
                    AutomationElement aeForm = aeTop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, winName));
                    if (aeForm != null)
                    {
                        aeEdit = aeForm.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NameProperty, controlName));
                        break;
                    }
                    else
                    {
                        throw new ElementNotEnabledException();
                        //当前没有打开的QQ聊天窗口~！
                    }
                }
            }
            return aeEdit;
        }
        /// <summary>
        /// 读取QQ窗口的历史消息
        /// </summary>
        /// <param name="aeControl">控件</param>
        /// <returns>消息内容(string)</returns>
        public string readQQMessage(string winName)
        {
            AutomationElement aeControl = findElement(winName, "消息");
            string Result = "";
            #region 读取
            try
            {
                ValuePattern valuepattern = aeControl.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                if (valuepattern != null)
                {
                    //获取控件内的文本
                    Result = valuepattern.Current.Value;
                    Result = Result.Replace("￼", "");
                    Result = Result.Replace("\n", "");
                    Result = Result.Replace("\r", "\n");
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //没有找到正确的消息资源~!"
            }
            #endregion
            return Result;
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="winName">窗口名</param>
        /// <param name="message">消息内容</param>
        public void sendQQMessage(string winName, string message)
        {
            IntPtr foreTopWindow = Win32.GetForegroundWindow();//得到当前前台窗口           
            try
            {
                _QQWindowHandle = Win32.FindWindow("TXGuiFoundation", _winTitle);//每次发送消息都查找一下窗口、                
                if ((uint)this._QQWindowHandle > 0)
                {
                    //Win32.PostMessage(_QQWindowHandle, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
                    //Win32.SendMessageInt(_QQWindowHandle, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);//还原QQ窗口,要等QQ响应
                    //如果不获得焦点在有些时候会出现问题导致消息发不出去.其实QQ窗口在还原的时候已经自动获取到的输入框的焦点
                    //Win32.SetFocus(_QQWindowHandle);//让QQ窗口得到焦点,
                  
                    //AutomationElement aeControl = findElement(winName, "输入");
                    //得到控件的位置
                    //System.Windows.Rect boundingRect = (System.Windows.Rect)aeControl.GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty);
                    //发送消息在窗口中点击一下，防止出现有时焦点在消息记录的情况
                    //int x = (int)(boundingRect.Left + boundingRect.Width / 2);
                    //int y = (int)(boundingRect.Top + boundingRect.Height / 2);
                    //int pos =x+y*65536;
                    //message = pos.ToString() + "_" + message;
                    //Win32.SendMessageInt(_QQWindowHandle, 0x201, 0, pos);//按下
                    //Thread.Sleep(20);
                    //Win32.SendMessageInt(_QQWindowHandle, 0x202, 0, pos);//松开
                    //把消息打散为字节
                    byte[] bMessage = System.Text.Encoding.GetEncoding("GB2312").GetBytes(message);
                    for (int i = 0; i < bMessage.Length; i++)//发送消息
                    {
                        Win32.SendMessageChar(_QQWindowHandle, Win32.WM_CHAR, bMessage[i], 0);
                    }
                    Win32.SendMessageInt(_QQWindowHandle, Win32.WM_KEYDOWN, 0x0D, 0);//发送回车按下
                    Win32.SendMessageInt(_QQWindowHandle, Win32.WM_KEYUP, 0x0D, 0);//发送回车抬起
                    //最小化QQ窗口
                    Win32.PostMessage(_QQWindowHandle, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
                }
            }
            catch
            {

            }
        }
    }
}
