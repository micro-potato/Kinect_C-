using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Utils
{
    public class TaskBarHelper
    {
        //任务栏控制
        private const int SW_HIDE = 0;//API参数表示隐藏窗口
        private const int SW_SHOW = 5;//API参数表示用当前的大小和位置显示窗口
        [DllImportAttribute("user32.dll")]
        private static extern int ShowWindow(int handle, int cmdShow);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        public void ShowTaskBar()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), SW_SHOW);
            ShowWindow(FindWindow("Button", null), SW_SHOW); ;
        }
        /// <summary>
        /// 隐藏任务栏
        /// </summary>
        public void HideTaskBar()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), SW_HIDE);
            ShowWindow(FindWindow("Button", null), SW_HIDE);
        }

        private void MainClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShowTaskBar();
            Process.GetCurrentProcess().Kill();
        }
    }
}
