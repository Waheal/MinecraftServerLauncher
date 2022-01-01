using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 我的世界开服器Metro
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.Hide();
        }
        public event Action form1ShowEvent;
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            form1ShowEvent();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainWindow.CmdProcess.HasExited == false || MainWindow.CmdProcess1.HasExited == false)
                {
                    System.Windows.Forms.MessageBox.Show("您的服务器或内网映射正在运行中，请确保完全关闭后再关闭软件！", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                else
                {
                    Close();
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch
            {
                try
                {
                    if (MainWindow.CmdProcess1.HasExited == false)
                    {

                        System.Windows.Forms.MessageBox.Show("内网映射正在运行中，请确保完全关闭后再关闭软件！", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Close();
                        Process.GetCurrentProcess().Kill();
                    }
                }
                catch
                {
                    Close();
                    Process.GetCurrentProcess().Kill();
                }

            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MainWindow.notifyIcon == false)
            {
                notifyIcon1.Visible = false;
                this.Dispose();
                this.Close();
            }
        }
    }
}
