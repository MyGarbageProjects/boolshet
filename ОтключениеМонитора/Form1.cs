using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace ОтключениеМонитора
{
    public partial class Form1 : Form
    {
        private const int MOVE = 0x0001;
        private static int HWND_BROADCAST = 0xffff;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MONITORPOWER = 0xF170;
        private bool Play = true;
        private bool start = true;
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //private static extern void mouse_event(IntPtr dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
        public Form1()
        {
            InitializeComponent();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            txt1.Enabled = false;
            txt2.Enabled = false;
            btnStart.Enabled = false;
            Play = true;
            if (start)
            {
                new Thread(() => SomeMethod()).Start();
                start = false;
            }   
        }
        private void SomeMethod()
        {
            do
            {
                SendMessage((IntPtr)HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, 2);
                System.Threading.Thread.Sleep(int.Parse(txt1.Text));
                SendMessage((IntPtr)HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, -1);
                button2.Invoke(new Action(ChangeSelection));
                Application.DoEvents();
                System.Threading.Thread.Sleep(int.Parse(txt2.Text));
            } while (Play);
        }
        private void btnStop_Click(object sender, EventArgs e)
        {

            Play = false;
            txt1.Enabled = true;
            txt2.Enabled = true;
            btnStart.Enabled = true;
        }
        private void ChangeSelection()
        {
            SendKeys.Send("{TAB}");
            SendKeys.Send("{ENTER}");
        }
        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            if (!Char.IsDigit(key) && key!=8)
                e.Handled = true;
        }
        private void txt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            if (!Char.IsDigit(key) && key != 8)
                e.Handled = true;
        }
    }
}
