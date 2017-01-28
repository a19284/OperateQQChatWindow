using QQHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sample
{
    public partial class Form1 : Form
    {
        //String winTitle = "0";
        StringBuilder sb = new StringBuilder();
        QQChatWindow a = new QQChatWindow("0");
        Rectangle Screenrect = new Rectangle();
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //a.Dispose(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ThreadStart ts = new ThreadStart(keep);
            Screenrect  = Screen.GetWorkingArea(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            a.sendQQMessage("0", "1234567890,数字,abcdefghijklmnopqrstuvwxy还有汉字的的一句非常长的信息是不是能发送出去？");
            sb.Append(a.readQQMessage("0")+"\n");
            sb.Append(DateTime.Now.ToString() + "--" + "send\n");
            textBox1.Text = sb.ToString();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                IntPtr hwnd = Win32.FindWindow(null, "0");
                Win32.SendMessageInt(hwnd, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);//还原QQ窗口,要等QQ响应
                Win32.SetWindowPos(hwnd, IntPtr.Zero, Screenrect.Right, 100, 500, 300, Win32.SWP_NOSIZE);
                Win32.PostMessage(hwnd, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
            }
            else
            {
                IntPtr hwnd = Win32.FindWindow(null, "0");
                Win32.SendMessageInt(hwnd, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);//还原QQ窗口,要等QQ响应
                Win32.SetWindowPos(hwnd, IntPtr.Zero, 300, 100, 500, 300, Win32.SWP_NOSIZE);
                Win32.PostMessage(hwnd, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
            }
        }
    }
}
