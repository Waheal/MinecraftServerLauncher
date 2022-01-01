using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using static 我的世界开服器Metro.MainWindow;

namespace 我的世界开服器Metro
{
    /// <summary>
    /// CreateServer.xaml 的交互逻辑
    /// </summary>
    public partial class CreateServer : Window
    {
        public static string DownjavaName;
        public static string Url;
        public static string Filename;
        public static bool safeClose = false;
        //public static Process CmdProcess = new Process();
        DispatcherTimer timer1 = new DispatcherTimer();
        public CreateServer()
        {
            InitializeComponent();
        }
        void DownloadFile()
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                outlog.Content = "连接下载地址中...";
            });
            try
            {
                HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(Url);
                HttpWebResponse myrp;
                myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                Stream st = myrp.GetResponseStream();
                FileStream so = new FileStream(Filename, FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    DispatcherHelper.DoEvents();
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                    float percent = 0;
                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                            outlog.Content = "下载中：" + percent.ToString("f2") + "%";
                        
                    });
                    DispatcherHelper.DoEvents();
                }
                so.Close();
                st.Close();
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    //string upbat = ":del\r\n del \"{0}\"\r\nif exist \"{0}\" goto del\r\n del %0\r\n";
                    //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"update.bat", upbat, Encoding.Default);
                    try
                    {
                        Process CmdProcess = new Process();
                        CmdProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe";
                        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory + "ServerLauncher");
                        CmdProcess.Start();
                        timer1.Tick += new EventHandler(timer1_Tick);
                        timer1.Interval = TimeSpan.FromSeconds(3);
                        timer1.Start();
                        outlog.Content = "安装中...";
                        /*
                        try
                        {
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe");
                            outlog.Content = "完成";
                            sJVM.IsSelected = true;
                            sJVM.IsEnabled = true;
                            sserver.IsEnabled = false;
                            MainWindow.serverserver = txb3.Text;
                            next3.IsEnabled = true;
                        }
                        catch
                        {
                            return;
                        }*/
                    }
                    catch
                    {
                        MessageBox.Show("安装失败，请查看是否有杀毒软件进行拦截！请确保添加信任或关闭杀毒软件后进行重新安装！","错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        next3.IsEnabled = true;
                        outlog.Content = "安装失败！";
                    }
                });
            }
            catch
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    outlog.Content = "发生错误，请前往群文件下载或重试:" + "m0x1";
                });
            }
        }
        private void next3_Click(object sender, RoutedEventArgs e)
        {
            next3.IsEnabled = false;
            if (usejv8.IsChecked == true)
            {
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8"))
                {
                    MainWindow.serverjava = "\"" + AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8\bin\java.exe" + "\"";
                    sJVM.IsSelected = true;
                    sJVM.IsEnabled = true;
                    sserver.IsEnabled = false;
                    MainWindow.serverserver = "\""+txb3.Text + "\"";
                    next3.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("下载Java即代表您接受Java的服务条款https://www.oracle.com/downloads/licenses/javase-license1.html");
                    if (Environment.Is64BitOperatingSystem)
                    {
                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8\bin\java.exe"))
                        {
                            lodingIco.Visibility = Visibility.Visible;
                            DownjavaName = "Java8";
                            //RealAction("https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=Ecs65caK7blGgZipDS1d76IBKDID3YUy9ak-HUzY_vDQUQ");
                            Url = "https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=Ecs65caK7blGgZipDS1d76IBKDID3YUy9ak-HUzY_vDQUQ";
                            Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe";
                            Thread thread = new Thread(DownloadFile);
                            thread.Start();
                            /*
                            Form4 fw = new Form4();
                            fw.ShowDialog();*/
                        }
                    }
                    else
                    {
                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8\bin\java.exe"))
                        {
                            lodingIco.Visibility = Visibility.Visible;
                            DownjavaName = "Java8";
                            //RealAction("https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=ES74HP6tN6dKuyTPUVOfEaYBJAecYATfZKXahAN_EZDC8Q");
                            Url = "https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=ES74HP6tN6dKuyTPUVOfEaYBJAecYATfZKXahAN_EZDC8Q";
                            Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe";
                            Thread thread = new Thread(DownloadFile);
                            thread.Start();
                            /*
                            Form4 fw = new Form4();
                            fw.ShowDialog();*/
                        }
                    }
                    MainWindow.serverjava = "\"" +AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8\bin\java.exe"+ "\"";
                }
                
                /*
                sJVM.IsSelected = true;
                sJVM.IsEnabled = true;
                sserver.IsEnabled = false;
                MainWindow.serverserver = txb3.Text;*/
            }
            if (usejv16.IsChecked == true)
            {
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java16"))
                {
                    MainWindow.serverjava = "\"" +AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java16\bin\java.exe"+ "\"";
                    sJVM.IsSelected = true;
                    sJVM.IsEnabled = true;
                    sserver.IsEnabled = false;
                    MainWindow.serverserver = "\"" +txb3.Text+ "\"";
                    next3.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("下载Java即代表您接受Java的服务条款https://www.oracle.com/downloads/licenses/javase-license1.html");
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java16\bin\java.exe"))
                    {
                        lodingIco.Visibility = Visibility.Visible;
                        DownjavaName = "Java16";
                        //RealAction("https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=EbapBNLCCwRLoFr2kxeCUdcBYNtGdsQO2h1MlzgFU3VZbQ");
                        Url = "https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=EbapBNLCCwRLoFr2kxeCUdcBYNtGdsQO2h1MlzgFU3VZbQ";
                        Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe";
                        Thread thread = new Thread(DownloadFile);
                        thread.Start();
                        /*
                        Form4 fw = new Form4();
                        fw.ShowDialog();*/
                    }
                    MainWindow.serverjava = "\"" +AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java16\bin\java.exe"+ "\"";
                }/*
                sJVM.IsSelected = true;
                sJVM.IsEnabled = true;
                sserver.IsEnabled = false;
                MainWindow.serverserver = txb3.Text;*/
            }
            if (usejv17.IsChecked == true)
            {
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java17"))
                {
                    MainWindow.serverjava = "\"" +AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java17\bin\java.exe"+ "\"";
                    sJVM.IsSelected = true;
                    sJVM.IsEnabled = true;
                    sserver.IsEnabled = false;
                    MainWindow.serverserver = "\"" +txb3.Text+ "\"";
                    next3.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("下载Java即代表您接受Java的服务条款https://www.oracle.com/downloads/licenses/javase-license1.html");
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java17\bin\java.exe"))
                    {
                        lodingIco.Visibility = Visibility.Visible;
                        DownjavaName = "Java17";
                        //RealAction("https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=EUxH8cdGAlxOkNiZGunIefEBwrdoMM5wPIb5h9xDpiWd_A");
                        Url = "https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=EUxH8cdGAlxOkNiZGunIefEBwrdoMM5wPIb5h9xDpiWd_A";
                        Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe";
                        Thread thread = new Thread(DownloadFile);
                        thread.Start();
                        /*
                        Form4 fw = new Form4();
                        fw.ShowDialog();*/
                    }
                    MainWindow.serverjava = "\"" +AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java17\bin\java.exe"+"\"" ;
                }
                    /*
                sJVM.IsSelected = true;
                sJVM.IsEnabled = true;
                sserver.IsEnabled = false;
                MainWindow.serverserver = txb3.Text;*/
            }
            if (useJVself.IsChecked == true)
            {
                MainWindow.serverjava = "\"" +txjava.Text+ "\"";
                sJVM.IsSelected = true;
                sJVM.IsEnabled = true;
                sserver.IsEnabled = false;
                MainWindow.serverserver = "\"" +txb3.Text+"\"";
                next3.IsEnabled = true;
            }
            if (usejvPath.IsChecked == true)
            {
                MainWindow.serverjava = "Java";
                sJVM.IsSelected = true;
                sJVM.IsEnabled = true;
                sserver.IsEnabled = false;
                MainWindow.serverserver = "\"" +txb3.Text+ "\"";
                next3.IsEnabled = true;
            }
        }

        private void next4_Click(object sender, RoutedEventArgs e)
        {
            sserverbase.IsSelected = true;
            sserverbase.IsEnabled = true;
            sJVM.IsEnabled = false;
            if (usedefault.IsChecked == true)
            {
                MainWindow.serverJVM = "";
            }
            else
            {
                MainWindow.serverJVM = "-Xms" + txb4.Text + "M -Xmx" + txb5.Text + "M";
            }
        }

        private void usedefault_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                txb4.IsEnabled = false;
                txb5.IsEnabled = false;
            }
        }

        private void useJVM_Checked(object sender, RoutedEventArgs e)
        {
            txb4.IsEnabled = true;
            txb5.IsEnabled = true;
        }

        private void done_Click(object sender, RoutedEventArgs e)
        {
            safeClose = true;
            MainWindow.serverbase = txb6.Text;
            try
            {
                Directory.CreateDirectory(MainWindow.serverbase);
                StreamWriter sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini");
                sw.Write("*|-j " + "\"" + MainWindow.serverjava + "\"" + "|-s " + "\"" + MainWindow.serverserver + "\"" + "|-a " + MainWindow.serverJVM + "|-b " + MainWindow.serverbase + "|*");
                sw.Flush();
                sw.Close();
                sw.Dispose();
                MessageBox.Show("创建完毕，请点击“开启服务器”按钮以开服", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch
            {
                MessageBox.Show("出现错误，请重试" + "c0x1", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void return3_Click(object sender, RoutedEventArgs e)
        {
            sserver.IsSelected = true;
            sserver.IsEnabled = true;
            sJVM.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window wn = new Window4();
            wn.ShowDialog();
            txb3.Text = MainWindow.serverserver.Replace("\"", "");
        }

        private void a0002_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openfile.Title = "请选择文件";
            openfile.Filter = "JAR文件|*.jar|所有文件类型|*.*";
            var res = openfile.ShowDialog();
            if (res == true)
            {
                txb3.Text = openfile.FileName;
            }
        }

        private void a0003_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txb6.Text = dialog.SelectedPath;
            }
        }

        private void return4_Click(object sender, RoutedEventArgs e)
        {
            sJVM.IsSelected = true;
            sJVM.IsEnabled = true;
            sserverbase.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Environment.Is64BitOperatingSystem)
            {
                jvhelp.Content = "您的电脑为32为系统，暂时只能使用Java8，感谢您的理解！";
                usejv16.IsEnabled = false;
                usejv17.IsEnabled = false;
            }
        }

        private void a0002_Copy_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openfile.Title = "请选择文件，通常为java.exe";
            openfile.Filter = "EXE文件|*.exe|所有文件类型|*.*";
            var res = openfile.ShowDialog();
            if (res == true)
            {
                txjava.Text = openfile.FileName;
            }
        }

        private void useJVself_Checked(object sender, RoutedEventArgs e)
        {
            txjava.IsEnabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\" + DownjavaName + @"\bin\java.exe"))
            {
                //CmdProcess.CancelOutputRead();
                //ReadStdOutput = null;
                //CmdProcess.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived);
                try
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory+@"ServerLauncher\Java.exe");
                    outlog.Content = "完成";
                    sJVM.IsSelected = true;
                    sJVM.IsEnabled = true;
                    sserver.IsEnabled = false;
                    MainWindow.serverserver = "\"" +txb3.Text+ "\"";
                    next3.IsEnabled = true;
                    lodingIco.Visibility = Visibility.Hidden;
                    timer1.Stop();
                }
                catch
                {
                    return;
                }
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            sserver.IsSelected = true;
            sserver.IsEnabled = true;
            welcome.IsEnabled = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (safeClose == false)
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        private void usejvPath_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                txjava.IsEnabled = false;
            }
        }

        private void usejv8_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                txjava.IsEnabled = false;
            }
        }

        private void usejv16_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                txjava.IsEnabled = false;
            }
        }

        private void usejv17_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                txjava.IsEnabled = false;
            }
        }
    }

}
