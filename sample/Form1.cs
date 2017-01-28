using QQHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sample
{
    public partial class Form1 : Form
    {
        //String winTitle = "0";
        QQChatWindow a = new QQChatWindow("0");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(a.readQQMessage("0"));
            a.sendQQMessage("0", "1234567890,数字,abcdefghijklmnopqrstuvwxy还有汉字的的一句非常长的信息是不是能发送出去？");
            //a.sendMessage("123\n456\nabc\n汉字啊", SendMessageHotKey.ENTER);
            //string str = a.getQQmessageHistory();
            //Console.WriteLine(str);
            // a.sendMessage("123\n456\nabc\n汉字啊", SendMessageHotKey.ENTER, "^ ");
            //MessageBox.Show("OK");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show(a.readQQMessage("0"));
            //a.sendQQMessage("0", "1234567890,数字,abcdefghijklmnopqrstuvwxy还有汉字的的一句非常长的信息是不是能发送出去？");
            //a.sendMessage("123\n456\nabc\n汉字啊", SendMessageHotKey.ENTER);
            //MessageBox.Show("1");
        }
    }
}
