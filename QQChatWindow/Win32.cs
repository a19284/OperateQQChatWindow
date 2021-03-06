﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QQHelper
{
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
        public const int SW_SHOW = 0x5;
        public const int SW_HIDE = 0x0;
        public const int SWP_NOSIZE = 0x0001;

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
        /// <summary>
        /// 显示或隐藏窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="bRepaint"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        /// <summary>
        /// 设置前台窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// 设置窗口位置
        /// </summary>
        /// <param name="win_handle"></param>
        /// <param name="win_handle_insert_after"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public  static extern int SetWindowPos(IntPtr win_handle, IntPtr win_handle_insert_after, int x, int y, int width, int height, uint flags);
    }
}
