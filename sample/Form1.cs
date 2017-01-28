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
        Thread t;
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (t.IsAlive) t.Abort();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ThreadStart ts = new ThreadStart(keep);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show();
            a.sendQQMessage("0", "1234567890,数字,abcdefghijklmnopqrstuvwxy还有汉字的的一句非常长的信息是不是能发送出去？");
            //a.sendMessage("123\n456\nabc\n汉字啊", SendMessageHotKey.ENTER);
            //MessageBox.Show("1");
            sb.Append(a.readQQMessage("0")+"\n");
            sb.Append(DateTime.Now.ToString() + "--" + "send\n");
            textBox1.Text = sb.ToString();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                t = new Thread(a.keepQQWindowsMinisize);
                t.Start();
            }
            else
            {
                t.Abort();
            }
        }
    }
}
