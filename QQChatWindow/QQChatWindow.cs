using System;
using Accessibility;
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
        //private int[] mHistoryBoxArray = new int[] { 3, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 3, 0 };//消息输出框的层级
        //private int[] mInputBoxArray = new int[] { 3, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0 };//消息输入框的层级
        public QQChatWindow(string tittle)
        {
            _winTitle = tittle;
            //_QQWindowHandle = Win32.FindWindow(null, tittle);
        }
        /*
        public string getQQmessageHistory()
        {
            string strResult = "";
            Process[] process = Process.GetProcessesByName("QQ");
            string a = process[0].ProcessName;
            //获取根节点
            AutomationElement aeTop = AutomationElement.RootElement;
            foreach (Process p in process)
            {
                if (p.MainWindowHandle != null)
                {
                    //查找窗体名
                    AutomationElement aeForm = aeTop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, _winTitle));
                    if (aeForm != null)
                    {
                        //寻找类型为Document的控件。在ui spy里可以查看到          
                        //UIA_EditControlTypeId                      
                        AutomationElementCollection aeAllEdit = aeForm.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
                        Thread.Sleep(1000);
                        for (int i = 0; i < aeAllEdit.Count; i++)
                        {
                            try
                            {
                                //判断控件ID
                                if (aeAllEdit[i].Current.Name == "消息")
                                {
                                    ValuePattern valuepattern = aeAllEdit[i].GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                                    strResult = valuepattern.Current.Value;
                                    strResult = strResult.Replace("￼", "");//删除乱码
                                    strResult = strResult.Replace("\r", "\r\n");//删除换行符
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                                //SetState("没有找到正确的消息资源~!");
                            }
                        }
                    }
                    else
                    {
                        // zt("当前没有打开的QQ聊天窗口~！");
                    }
                }
                else
                {
                    //SetState("没有找到QQ程序，是否已启动？");
                }
            }
            return strResult;
        }
        */
        /*
        /// <summary>
        /// 使用sendkey向QQ聊天窗口发消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="sendMessageHotKey">发送消息快捷键(ENTER/CTRL+ENTER)</param>
        /// <param name="closeIMHotKey"></param>
        public void sendMessage(string message,SendMessageHotKey sendMessageHotKey, string closeIMHotKey)
        {
            IntPtr foreTopWindow = Win32.GetForegroundWindow();
            if ((uint)this._QQWindowHandle > 0)
            {
                Win32.PostMessage(_QQWindowHandle, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
                Win32.SetFocus(_QQWindowHandle);
                //SendKeys.Send("^ ");//closeIMHotKey
                System.Threading.Thread.Sleep(1000);
                SendKeys.Send(message);//发送消息到窗口
                System.Threading.Thread.Sleep(100);
                if (sendMessageHotKey == 0)
                {
                    SendKeys.Send("{ENTER}");//""发送发消息快捷键
                }
                else
                {
                    SendKeys.Send("^{ENTER}");
                }
                System.Threading.Thread.Sleep(100);
                Win32.PostMessage(_QQWindowHandle, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
            }
            else
            {
                //没有窗口
                //MessageBox.Show("1");
            }
        }
        */
        /*
        public void sendMessage(string message, SendMessageHotKey sendMessageHotKey)
        {
            IntPtr foreTopWindow = Win32.GetForegroundWindow();//得到当前前台窗口           
            try
            {
                _QQWindowHandle = Win32.FindWindow("TXGuiFoundation", _winTitle);//每次发送消息都查找一下窗口
                IAccessible _inputBox;
                GetAccessibleObjects(_QQWindowHandle, mHistoryBoxArray, out _inputBox);//得到出入窗口
                int L, R, W, H;
                object o = new object();
                _inputBox.accLocation(out L, out R, out W, out H);//得到输入框的位置
                if ((uint)this._QQWindowHandle > 0)
                {
                    //Win32.PostMessage(_QQWindowHandle, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
                    Win32.SendMessageInt(_QQWindowHandle, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);//还原QQ窗口,要等QQ响应
                    //如果不获得焦点在有些时候会出现问题导致消息发不出去.其实QQ窗口在还原的时候已经自动获取到的输入框的焦点
                    //下面这一句设置焦点可以不用,为了安全还是保留
                    Win32.SetFocus(_QQWindowHandle);//让QQ窗口得到焦点,
                    //
                    //Thread.Sleep(300);
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
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private void GetAccessibleObjects(System.IntPtr imWindowHwnd, int[] classArray, out IAccessible obj)
        {
            Guid guidCOM = new Guid(0x618736E0, 0x3C3D, 0x11CF, 0x81, 0xC, 0x0, 0xAA, 0x0, 0x38, 0x9B, 0x71);
            Accessibility.IAccessible IACurrent = null;
            Win32.AccessibleObjectFromWindow(imWindowHwnd, (int)Win32.OBJID_CLIENT, ref guidCOM, ref IACurrent);
            IACurrent = (IAccessible)IACurrent.accParent;
            obj = null;
            obj = GetAccessibleChild(IACurrent, classArray);//
        }
        private IAccessible[] GetAccessibleChildren(IAccessible paccContainer)
        {
            IAccessible[] rgvarChildren = new IAccessible[paccContainer.accChildCount];
            int pcObtained;
            Win32.AccessibleChildren(paccContainer, 0, paccContainer.accChildCount, rgvarChildren, out pcObtained);
            return rgvarChildren;
        }
        /// <summary>
        /// 查找指定对象
        /// </summary>
        /// <param name="paccContainer">对象</param>
        /// <param name="array">对象层级关系</param>
        /// <returns></returns>
        private IAccessible GetAccessibleChild(IAccessible paccContainer, int[] array)
        {
            if (array.Length > 0)
            {
                IAccessible result = GetAccessibleChildren(paccContainer)[array[0]];

                int[] array_1 = new int[array.Length - 1];
                for (int i = 0; i < array.Length - 1; i++)
                {
                    array_1[i] = array[i + 1];
                }
                return GetAccessibleChild(result, array_1);
            }
            else
            {
                return paccContainer;
            }
        }
        */
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
                    Win32.SendMessageInt(_QQWindowHandle, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);//还原QQ窗口,要等QQ响应
                    //如果不获得焦点在有些时候会出现问题导致消息发不出去.其实QQ窗口在还原的时候已经自动获取到的输入框的焦点
                    Win32.SetFocus(_QQWindowHandle);//让QQ窗口得到焦点,
                  
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
    /// <summary>
    /// win32 API 类
    /// </summary>
    public class Win32
    {
        public const int WM_SETTEXT = 0x000C;
        public const int WM_CLICK = 0x00F5;
        public const int CHILDID_SELF = 0;
        public const int CHILDID_1 = 1;
        public const int OBJID_CLIENT = -4;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_RESTORE = 0xF120;
        public const int SC_MINIMIZE = 0xF020;
        public const int WM_CHAR = 0x0102;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;

        /// <summary>
        /// 向窗口发送消息
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "PostMessage", CallingConvention = CallingConvention.Winapi)]
        public static extern bool PostMessage(IntPtr hwnd, int msg, uint wParam, uint lParam);

        /// <summary>
        /// 是窗口或者子窗口获得焦点
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        /// <summary>
        /// 查找窗口
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 获得当前前台窗口的句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessageChar(IntPtr hwnd, int wMsg, byte bChar, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessageString(IntPtr hwnd, int wMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessageInt(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(
            IntPtr parentHandle,
            IntPtr childAfter,
            string lpszClass,
            int sWindowTitle  /*HWND*/);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("Oleacc.dll")]
        public static extern int AccessibleObjectFromWindow(
        IntPtr hwnd,
        int dwObjectID,
        ref Guid refID,
        ref IAccessible ppvObject);

        [DllImport("Oleacc.dll")]
        public static extern int WindowFromAccessibleObject(
            IAccessible pacc,
            out IntPtr phwnd);

        [DllImport("Oleacc.dll")]
        public static extern int AccessibleChildren(
        Accessibility.IAccessible paccContainer,
        int iChildStart,
        int cChildren,
        [Out] object[] rgvarChildren,
        out int pcObtained);
    }
}
