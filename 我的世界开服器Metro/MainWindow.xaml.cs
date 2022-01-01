using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace 我的世界开服器Metro
{
    public delegate void DelReadStdOutput(string result);
    public delegate void DelReadStdOutput1(string result);
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static string servername;
        public static string serverjava = "Java";
        public static string serverserver;
        public static string serverJVM;
        public static string serverJVMcmd = "";
        public static string serverbase;
        public static string update = "5.7.1.0";
        public static string aaa;
        public static string aaa2 = "none";
        public static bool aa = false;
        public static string aa2 = "0";
        public static string Url;
        public static string Filename;
        public static bool updatedown = false;
        public static bool isgetserverOpen = false;
        public static string frpc;
        public static bool notifyIcon;
        public static bool autoserver = false;
        public static string DownjavaName;
        public static Process CmdProcess1 = new Process();
        public event DelReadStdOutput1 ReadStdOutput1;
        string line;
        DispatcherTimer timer1 = new DispatcherTimer();
        public static Process CmdProcess = new Process();
        //public static Process Sfrpc = new Process();
        public event DelReadStdOutput ReadStdOutput;
        //public static Process p = new Process();
        DispatcherTimer timer3 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
            ReadStdOutput1 += new DelReadStdOutput1(ReadStdOutputAction1);
        }
        //加载模块
        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher");
            }
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc.exe"))
            {
                byte[] res;
                res = new byte[Properties.Resources.frpc.Length];
                Properties.Resources.frpc.CopyTo(res, 0);
                FileStream sw = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc.exe", FileMode.Create, FileAccess.Write);
                sw.Write(res, 0, res.Length);
                sw.Close();
            }
            /*
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\aria2c.exe"))
            {
                byte[] res;
                res = new byte[Properties.Resources.aria2c.Length];
                Properties.Resources.aria2c.CopyTo(res, 0);
                FileStream sw = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\aria2c.exe", FileMode.Create, FileAccess.Write);
                sw.Write(res, 0, res.Length);
                sw.Close();
            }*/
            //JObject aaaaa = (JObject)JToken.ReadFrom("\"server\" :\"null\"\n\"java\" :\"null\"\n\"b\" :\"null\"\n\"frpc\" :\"null\"");
            //string output = Newtonsoft.Json.JsonConvert.SerializeObject(aaaaa, Newtonsoft.Json.Formatting.Indented);
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json"))
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", String.Format("{{{0}}}", "\n\"frpc\": \"\",\n\"notifyIcon\": \"True\",\n\"notice\": \"0\",\n\"frpcversion\": \"1\"\n"));
                Process.Start(Application.ResourceAssembly.Location);
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                try
                {
                    StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json");
                    JsonTextReader jsonTextReader = new JsonTextReader(reader);
                    JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                    if (jsonObject["frpc"] == null)
                    {
                        reader.Close();
                        MessageBox.Show("配置文件错误，即将修复");
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", String.Format("{{{0}}}", "\n\"frpc\": \"\",\n\"notifyIcon\": \"True\",\n\"notice\": \"0\",\n\"frpcversion\": \"1\"\n"));
                        Process.Start(Application.ResourceAssembly.Location);
                        Process.GetCurrentProcess().Kill();
                    }
                    frpc = jsonObject["frpc"].ToString();
                    reader.Close();
                    if (frpc == "")
                    {
                        frpc = null;
                    }
                }
                catch
                {
                    frpc = null;
                }
                try
                {
                    StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json");
                    JsonTextReader jsonTextReader = new JsonTextReader(reader);
                    JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                    if (jsonObject["frpcversion"] == null)
                    {
                        reader.Close();
                        MessageBox.Show("配置文件错误，即将修复");
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\aria2c.exe"))
                        {
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\aria2c.exe");
                        }
                        byte[] res;
                        res = new byte[Properties.Resources.frpc.Length];
                        Properties.Resources.frpc.CopyTo(res, 0);
                        FileStream sw = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc.exe", FileMode.Create, FileAccess.Write);
                        sw.Write(res, 0, res.Length);
                        sw.Close();
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", String.Format("{{{0}}}", "\n\"frpc\": \"\",\n\"notifyIcon\": \"True\",\n\"notice\": \"0\",\n\"frpcversion\": \"1\"\n"));
                        Process.Start(Application.ResourceAssembly.Location);
                        Process.GetCurrentProcess().Kill();
                    }
                    if (jsonObject["frpcversion"].ToString() != "1")
                    {
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\aria2c.exe"))
                        {
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\aria2c.exe");
                        }
                        byte[] res;
                        res = new byte[Properties.Resources.frpc.Length];
                        Properties.Resources.frpc.CopyTo(res, 0);
                        FileStream sw = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc.exe", FileMode.Create, FileAccess.Write);
                        sw.Write(res, 0, res.Length);
                        sw.Close();
                    }
                    reader.Close();
                }
                catch
                { }
                try
                {
                    StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json");
                    JsonTextReader jsonTextReader = new JsonTextReader(reader);
                    JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                    if (jsonObject["notifyIcon"] == null)
                    {
                        reader.Close();
                        MessageBox.Show("配置文件错误，即将修复");
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", String.Format("{{{0}}}", "\n\"frpc\": \"\",\n\"notifyIcon\": \"True\",\n\"notice\": \"0\",\n\"frpcversion\": \"1\"\n"));
                        Process.Start(Application.ResourceAssembly.Location);
                        Process.GetCurrentProcess().Kill();
                    }
                    if (jsonObject["notifyIcon"].ToString() == "True")
                    {
                        notifyIcon = true;
                    }
                    else
                    {
                        notifyIcon = false;
                        setnotifyIcon.Content = "开启托盘图标";
                    }
                    reader.Close();
                }
                catch
                {
                    notifyIcon = true;
                }
                welcomelabel.Content = "欢迎使用我的世界开服器！版本：" + update;
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini"))
                {
                    Window wn = new CreateServer();
                    wn.ShowDialog();
                }
                else
                {
                    string line = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini");

                    int IndexofA2 = line.IndexOf("-j ");
                    string Ru2 = line.Substring(IndexofA2 + 3);
                    string a200 = Ru2.Substring(0, Ru2.IndexOf("|"));
                    //serverjavalist.Items.Add(a200);
                    serverjava = a200;

                    int IndexofA3 = line.IndexOf("-s ");
                    string Ru3 = line.Substring(IndexofA3 + 3);
                    string a300 = Ru3.Substring(0, Ru3.IndexOf("|"));
                    //serverserverlist.Items.Add(a300);
                    serverserver = a300;

                    int IndexofA4 = line.IndexOf("-a ");
                    string Ru4 = line.Substring(IndexofA4 + 3);
                    string a400 = Ru4.Substring(0, Ru4.IndexOf("|"));
                    //serverJVMlist.Items.Add(a400);
                    serverJVM = a400;

                    int IndexofA5 = line.IndexOf("-b ");
                    string Ru5 = line.Substring(IndexofA5 + 3);
                    string a500 = Ru5.Substring(0, Ru5.IndexOf("|"));
                    //serverbaselist.Items.Add(a500);
                    serverbase = a500;
                }
                try
                {
                    WebClient MyWebClient = new WebClient();
                    MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                    byte[] pageData = MyWebClient.DownloadData("http://115.220.5.81:8081/web/update.txt");
                    string pageHtml = Encoding.UTF8.GetString(pageData);
                    string strtempa = "#";
                    int IndexofA = pageHtml.IndexOf(strtempa);
                    string Ru = pageHtml.Substring(IndexofA + 1);
                    aaa = Ru.Substring(0, Ru.IndexOf("#"));
                    string strtempa2 = "&";
                    int IndexofA2 = pageHtml.IndexOf(strtempa2);
                    string Ru2 = pageHtml.Substring(IndexofA2 + 1);
                    aaa2 = Ru2.Substring(0, Ru2.IndexOf("&"));
                    if (aaa != update)
                    {
                        aa = true;
                        aa2 = "发现新版本！建议您更新！\n版本号" + aaa + "\n" + "更新日志：" + aaa2;
                        Window window = new Window1();
                        window.ShowDialog();
                        if (aa == true)
                        {
                            string strtempa1 = "*";
                            int IndexofA1 = pageHtml.IndexOf(strtempa1);
                            string Ru1 = pageHtml.Substring(IndexofA1 + 1);
                            string aaa1 = Ru1.Substring(0, Ru1.IndexOf("*"));
                            Url = aaa1;
                            Filename = AppDomain.CurrentDomain.BaseDirectory + @"\我的世界开服器" + aaa + ".exe";
                            updatedown = true;
                            Thread thread = new Thread(DownloadFile);
                            thread.Start();
                        }
                        aa = false;
                    }
                }
                catch
                {
                    aaa2 = "";
                }
                if (notifyIcon == true)
                {
                    Form2 fw = new Form2();
                    fw.Show();
                    fw.form1ShowEvent += form1Show;
                }
                try
                {
                    WebClient MyWebClient = new WebClient();
                    MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                    byte[] pageData = MyWebClient.DownloadData("http://115.220.5.81:8081/web/notice.txt");
                    string pageHtml = Encoding.UTF8.GetString(pageData);
                    string strtempa = "*";
                    int IndexofA = pageHtml.IndexOf(strtempa);
                    string Ru = pageHtml.Substring(IndexofA + 1);
                    string aa = Ru.Substring(0, Ru.IndexOf("*"));
                    string strtempa2 = "#";
                    int IndexofA2 = pageHtml.IndexOf(strtempa2);
                    string Ru2 = pageHtml.Substring(IndexofA2 + 1);
                    aa2 = Ru2.Substring(0, Ru2.IndexOf("#"));
                    try
                    {
                        noticeLab.Text = aa2;
                        StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json");
                        JsonTextReader jsonTextReader = new JsonTextReader(reader);
                        JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                        if (jsonObject["notice"] == null)
                        {
                            reader.Close();
                            MessageBox.Show("配置文件错误，即将修复");
                            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", String.Format("{{{0}}}", "\n\"frpc\": \"\",\n\"notifyIcon\": \"True\",\n\"notice\": \"0\",\n\"frpcversion\": \"1\"\n"));
                            Process.Start(Application.ResourceAssembly.Location);
                            Process.GetCurrentProcess().Kill();
                        }
                        string aa3 = jsonObject["notice"].ToString();
                        reader.Close();
                        if (aa3 != aa)
                        {
                            Window window = new Window1();
                            window.ShowDialog();
                            try
                            {
                                //StreamReader reader = File.OpenText(Application.StartupPath+@"\server\ServerLauncher.json", System.Text.Encoding.UTF8);
                                string jsonString = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", System.Text.Encoding.UTF8);
                                JObject jobject = JObject.Parse(jsonString);
                                jobject["notice"] = aa.ToString();
                                string convertString = Convert.ToString(jobject);
                                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", convertString, System.Text.Encoding.UTF8);
                            }
                            catch
                            { }
                        }
                    }
                    catch
                    { }
                }
                catch
                { }
                if (aaa2 == "")
                {
                    updatelab.Text = "无法连接至更新服务器，更新日志获取失败！";
                }
                else
                {
                    updatelab.Text = aaa2;
                }
            }
        }
        private void form1Show()
        {
            this.Visibility = Visibility.Visible;
        }
        //下载模块
        public static class DispatcherHelper
        {
            [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            public static void DoEvents()
            {
                DispatcherFrame frame = new DispatcherFrame();
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrames), frame);
                try { Dispatcher.PushFrame(frame); }
                catch (InvalidOperationException) { }
            }
            private static object ExitFrames(object frame)
            {
                ((DispatcherFrame)frame).Continue = false;
                return null;
            }
        }
        [DllImport("kernel32.dll")]
        public static extern uint WinExec(string lpCmdLine, uint uCmdShow);
        void DownloadFile()
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                if (updatedown == true)
                {
                    welcomelabel.Content = "连接下载地址中...";
                }
                else
                {
                    downout.Content = "连接下载地址中...";
                }
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
                        if (updatedown == true)
                        {
                            welcomelabel.Content = "下载中：" + percent.ToString("f2") + "%";
                        }
                        else
                        {
                            downout.Content = "下载中：" + percent.ToString("f2") + "%";
                        }
                    });
                    DispatcherHelper.DoEvents();
                }
                so.Close();
                st.Close();
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    //string upbat = ":del\r\n del \"{0}\"\r\nif exist \"{0}\" goto del\r\n del %0\r\n";
                    //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"update.bat", upbat, Encoding.Default);
                    if (updatedown == true)
                    {
                        string vBatFile = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "DEL.bat";
                        using (StreamWriter vStreamWriter = new StreamWriter(vBatFile, false, Encoding.Default))
                        {
                            vStreamWriter.Write(string.Format(":del\r\n del \"" + System.Windows.Forms.Application.ExecutablePath + "\"\r\n \"" + AppDomain.CurrentDomain.BaseDirectory + @"我的世界开服器.exe" + "\"\r\nif exist \"" + System.Windows.Forms.Application.ExecutablePath + "\" goto del\r\n " + System.Windows.Forms.Application.ExecutablePath + "\"\r\n \"" + AppDomain.CurrentDomain.BaseDirectory + @"我的世界开服器" + aaa + ".exe" + "\"\r\n" + " del %0\r\n", AppDomain.CurrentDomain.BaseDirectory));
                        }
                        WinExec(vBatFile, 0);
                        Process.GetCurrentProcess().Kill();
                    }
                    else
                    {
                        try
                        {
                            Process CmdProcess = new Process();
                            CmdProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe";
                            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory + "ServerLauncher");
                            CmdProcess.Start();
                            timer3.Tick += new EventHandler(timer3_Tick);
                            timer3.Interval = TimeSpan.FromSeconds(3);
                            timer3.Start();
                            downout.Content = "下载完毕，安装中...";
                        }
                        catch
                        {
                            MessageBox.Show("安装失败，请查看是否有杀毒软件进行拦截！请确保添加信任或关闭杀毒软件后进行重新安装！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                            doneBtn1.IsEnabled = true;
                            downout.Content = "安装失败！";
                        }

                    }
                });
            }
            catch
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    welcomelabel.Content = "发生错误，请前往群文件下载或重试:" + "m0x1";
                });
            }
        }
        /*
        private void StartFrpc(string StartFileName, string StartFileArg)
        {
            try
            {
                Sfrpc.StartInfo.FileName = StartFileName;
                Sfrpc.StartInfo.Arguments = StartFileArg;
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\");
                //Sfrpc.StartInfo.CreateNoWindow = false;
                //Sfrpc.StartInfo.UseShellExecute = false;
                //Sfrpc.StartInfo.RedirectStandardInput = true;
                //Sfrpc.StartInfo.RedirectStandardOutput = true;
                //Sfrpc.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived1);
                Sfrpc.Start();
                //Sfrpc.BeginOutputReadLine();
            }
            catch
            {
                MessageBox.Show("出现错误，请重试！" + "m0x2", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/
        //关闭检测
        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (notifyIcon == true)
            {
                e.Cancel = true;
                this.Visibility = Visibility.Hidden;

            }
            else
            {
                try
                {
                    if (CmdProcess.HasExited == false || CmdProcess1.HasExited == false)
                    {

                        System.Windows.Forms.MessageBox.Show("您的服务器或内网映射正在运行中，请确保完全关闭后再关闭软件！", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }
                catch
                {
                    try
                    {
                        if (CmdProcess1.HasExited == false)
                        {

                            System.Windows.Forms.MessageBox.Show("内网映射正在运行中，请确保完全关闭后再关闭软件！", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                    }
                    catch
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }

        }

        private void setdefault_Click(object sender, RoutedEventArgs e)
        {
            //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", String.Format("{{{0}}}", "\n\"server\": \"\",\n\"java\": \"\",\n\"b\": \"\",\n\"frpc\": \"\",\n\"bcolor\": \"\",\n\"fcolor\": \"\",\n\"notifyIcon\": \"true\"\n"));
            try
            {
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher", true);
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher");
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", String.Format("{{{0}}}", "\n\"frpc\": \"\",\n\"notifyIcon\": \"True\",\n\"notice\": \"0\",\n\"frpcversion\": \"1\"\n"));
            }
            catch
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", String.Format("{{{0}}}", "\n\"frpc\": \"\",\n\"notifyIcon\": \"True\",\n\"notice\": \"0\",\n\"frpcversion\": \"1\"\n"));
            }
            Process.Start(Application.ResourceAssembly.Location);
            Process.GetCurrentProcess().Kill();
        }

        private void setnotifyIcon_Click(object sender, RoutedEventArgs e)
        {
            if (setnotifyIcon.Content.ToString() == "关闭托盘图标")
            {
                notifyIcon = false;
                try
                {
                    //StreamReader reader = File.OpenText(Application.StartupPath+@"\server\ServerLauncher.json", System.Text.Encoding.UTF8);
                    string jsonString = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", System.Text.Encoding.UTF8);
                    JObject jobject = JObject.Parse(jsonString);
                    jobject["notifyIcon"] = notifyIcon.ToString();
                    string convertString = Convert.ToString(jobject);
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", convertString, System.Text.Encoding.UTF8);
                }
                catch
                {
                    return;
                }
                setnotifyIcon.Content = "开启托盘图标";
            }
            else
            {
                notifyIcon = true;
                Form2 fw = new Form2();
                fw.Show();
                try
                {
                    fw.form1ShowEvent += form1Show;
                }
                catch
                {
                    return;
                }
                try
                {
                    //StreamReader reader = File.OpenText(Application.StartupPath+@"\server\ServerLauncher.json", System.Text.Encoding.UTF8);
                    string jsonString = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", System.Text.Encoding.UTF8);
                    JObject jobject = JObject.Parse(jsonString);
                    jobject["notifyIcon"] = notifyIcon.ToString();
                    string convertString = Convert.ToString(jobject);
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", convertString, System.Text.Encoding.UTF8);
                }
                catch
                {
                    return;
                }
                setnotifyIcon.Content = "关闭托盘图标";
            }
        }

        private void useidea_Click(object sender, RoutedEventArgs e)
        {
            Window wn = new Window2();
            wn.Show();
        }

        private void startServer_Click(object sender, RoutedEventArgs e)
        {
            if (startServer.Content.ToString() == "开启服务器")
            {
                tabControl1.SelectedIndex = 1;
                setServer.IsEnabled = false;
                /*
                tabbtn1.IsEnabled = true;
                tabbtn2.IsEnabled = false;
                tabbtn3.IsEnabled = false;*/
                setdefault.IsEnabled = false;
                startServer.Content = "关闭服务器";
                startServer.Background = new SolidColorBrush(Color.FromRgb(139, 0, 0));
                cmdtext.IsEnabled = true;
                sendcmd.IsEnabled = true;
                fastCMD.IsEnabled = true;
                outlog.Document.Blocks.Clear();
                ShowLog("正在开启服务器，请稍等...", Brushes.Black);
                serverState.Content = "开服中";
                //MessageBox.Show(serverJVM + " -jar " + serverserver + " nogui");
                StartServer(serverJVM + " -jar " + serverserver + " nogui " + serverJVMcmd);
            }
            else
            {
                startServer.Content = "正在关服...";
                CmdProcess.StandardInput.WriteLine("stop");
            }
        }
        private void StartServer(string StartFileArg)
        {
            try
            {
                //cmdtext.IsEnabled = true;
                //sendcmd.IsEnabled = true;
                CmdProcess.StartInfo.FileName = serverjava;
                //CmdProcess.StartInfo.FileName = StartFileName;
                CmdProcess.StartInfo.Arguments = StartFileArg;
                Directory.SetCurrentDirectory(serverbase);
                CmdProcess.StartInfo.CreateNoWindow = true;
                CmdProcess.StartInfo.UseShellExecute = false;
                CmdProcess.StartInfo.RedirectStandardInput = true;
                CmdProcess.StartInfo.RedirectStandardOutput = true;
                CmdProcess.StartInfo.RedirectStandardError = true;
                CmdProcess.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                CmdProcess.Start();
                CmdProcess.BeginOutputReadLine();
                CmdProcess.BeginErrorReadLine();
                timer1.Tick += new EventHandler(timer1_Tick);
                timer1.Interval = TimeSpan.FromSeconds(1);
                timer1.Start();
                //CmdProcess.StandardInput.WriteLine(StartFileArg + "&exit");
                //serverTime = 0;
                //timer2.Tick += new EventHandler(timer2_Tick);
                //timer2.Interval = TimeSpan.FromSeconds(1);
                //timer2.Start();
            }
            catch
            {
                ShowLog("出现错误，正在检查问题...", Brushes.Black);
                string a = serverjava.Replace("\"", "");
                if (File.Exists(a))
                {
                    ShowLog("Java路径正常", Brushes.Green);
                }
                else
                {
                    ShowLog("Java路径有误", Brushes.Red);
                }
                string b = serverserver.Replace("\"", "");
                if (File.Exists(b))
                {
                    ShowLog("服务端路径正常", Brushes.Green);
                }
                else
                {
                    ShowLog("服务端路径有误", Brushes.Red);
                }
                //string c = serverbase.Replace("\"", "");
                if (Directory.Exists(serverbase))
                {
                    ShowLog("服务器目录正常", Brushes.Green);
                }
                else
                {
                    ShowLog("服务器目录有误", Brushes.Red);
                }
                MessageBox.Show("出现错误，开服器已检测完毕，请根据检测信息对服务器设置进行更改！错误代码:" + "r0x1", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                startServer.Content = "开启服务器";
                startServer.Background = new SolidColorBrush(Color.FromRgb(0, 139, 68));
                try
                {
                    CmdProcess.CancelOutputRead();
                    CmdProcess.CancelErrorRead();
                    //ReadStdOutput = null;

                    CmdProcess.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived);
                    CmdProcess.ErrorDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived);

                    //ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
                    //outlog.AppendText("\n服务器已关闭！输入start来开启服务器.");
                }
                catch { }
                //tabbtn3.IsEnabled = true;
                setServer.IsEnabled = true;
                setdefault.IsEnabled = true;
                cmdtext.IsEnabled = false;
                sendcmd.IsEnabled = false;
                fastCMD.IsEnabled = false;

            }
        }
        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Dispatcher.Invoke(ReadStdOutput, new object[] { e.Data });
            }
        }
        void ShowLog(string msg, Brush color)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, (ThreadStart)delegate ()
            {
                string s = string.Format("{1}", DateTime.Now, msg);
                Paragraph p = new Paragraph(new Run(s));
                p.Foreground = color;
                outlog.Document.Blocks.Add(p);
                outlog.ScrollToEnd();
            });
        }
        private delegate void AddMessageHandler(string msg);
        private void ReadStdOutputAction(string msg)
        {
            if (msg.IndexOf("EULA") + 1 != 0)
            {
                //outlog.AppendText("[信息]" + msg);
                ShowLog("[信息]" + msg, Brushes.Green);
                MessageBoxResult msgr = MessageBox.Show("检测到您没有接受Mojang的EULA条款！是否阅读并接受EULA条款并继续开服？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (msgr == MessageBoxResult.Yes)
                {
                    try
                    {
                        string path1 = serverbase + @"\eula.txt";
                        FileStream fs = new FileStream(path1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                        string line;
                        line = sr.ReadToEnd();
                        line = line.Replace("eula=false", "eula=true");
                        string path = serverbase + @"\eula.txt";
                        StreamWriter streamWriter = new StreamWriter(path);
                        streamWriter.WriteLine(line);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    catch
                    {
                        MessageBox.Show("出现错误，请手动修改eula文件或重试:" + "r0x2", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    Process.Start("https://account.mojang.com/documents/minecraft_eula");
                    /*
                    outlog.Text = "";
                    efflabel.Content = "已开启";
                    timelabel.Content = string.Format("{0:00}:{1:00}:{2:00}", 0, 0, 0);
                    sw = new Stopwatch();
                    timer4.Tick += new EventHandler(timer4_Tick);
                    timer4.Interval = TimeSpan.FromSeconds(1);
                    sw.Start();
                    timer4.Start();
                    */
                    MessageBox.Show("阅读完毕后请点击“开启服务器”按钮以开服！");
                }

            }
            else
            {
                if (msg.IndexOf("Done") + 1 != 0)
                {
                    //outlog.AppendText("[信息]" + msg + "已成功开启服务器！在没有改动服务器IP和端口的情况下，请使用127.0.0.1:25565进入服务器；要让他人进服，需要进行内网映射或使用公网IP。");
                    ShowLog("[信息]" + msg + "\r\n已成功开启服务器！\r\n你可以输入stop来关闭服务器！", Brushes.Black);
                    serverState.Content = "已开服";
                    try
                    {
                        string path1 = serverbase + @"\server.properties";
                        FileStream fs = new FileStream(path1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                        string line;
                        line = sr.ReadToEnd();
                        if (line.IndexOf("online-mode=true") + 1 != 0)
                        {
                            onlineMode.IsEnabled = true;
                            ShowLog("\r\n检测到您没有关闭正版验证，如果您的游戏登录方式为离线登录的话，请点击下面“关闭正版验证”按钮以关闭正版验证。否则离线账户将无法进入服务器！\r\n", Brushes.Black);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("出现错误" + "r0x4", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    if (msg.IndexOf("Stopping server") + 1 != 0)
                    {
                        //outlog.AppendText("[信息]" + msg + "正在关闭服务器！");
                        ShowLog("[信息]" + msg + "\r\n正在关闭服务器！", Brushes.Black);
                        serverState.Content = "关服中";
                    }
                    else
                    {
                        if (msg.IndexOf("FAILED") + 1 != 0)
                        {
                            if (msg.IndexOf("PORT") + 1 != 0)
                            {
                                ShowLog("[错误]" + msg + "\r\n警告：由于端口占用，服务器已自动关闭！请检查您的服务器是否多开或者有其他软件占用端口！", Brushes.Red);
                                //outlog.AppendText("[错误]" + msg + "\r\n警告：由于端口占用，服务器已自动关闭！请检查您的服务器是否多开或者有其他软件占用端口！\r\n");

                            }
                        }
                        else
                        {
                            if (msg.IndexOf("INFO") + 1 != 0)
                            {
                                //outlog.AppendText("[信息]" + msg);
                                ShowLog("[信息]" + msg, Brushes.Green);

                            }
                            else
                            {
                                if (msg.IndexOf("WARN") + 1 != 0)
                                {
                                    //outlog.AppendText("[警告]" + msg);
                                    ShowLog("[警告]" + msg, Brushes.Orange);
                                }
                                else
                                {
                                    if (msg.IndexOf("ERROR") + 1 != 0)
                                    {
                                        //outlog.AppendText("[错误]" + msg);
                                        ShowLog("[错误]" + msg, Brushes.Red);

                                    }
                                    else
                                    {
                                        //outlog.AppendText(msg);
                                        ShowLog(msg, Brushes.Green);

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        void timer1_Tick(object sender, EventArgs e)
        {
            if (CmdProcess.HasExited == true)
            {
                timer1.Stop();
                if (autoserver == true)
                {
                    ShowLog("正在重启服务器...", Brushes.Black);
                    serverState.Content = "重启中";
                    CmdProcess.CancelOutputRead();
                    CmdProcess.CancelErrorRead();
                    //ReadStdOutput = null;
                    CmdProcess.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived);
                    CmdProcess.ErrorDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived);
                    //ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
                    outlog.Document.Blocks.Clear();
                    //outlog.AppendText("正在重启服务器...");
                    StartServer(serverJVM + " -jar " + serverserver + " nogui");
                }
                else
                {
                    ShowLog("\n服务器已关闭！", Brushes.Black);
                    serverState.Content = "已关服";
                    startServer.Content = "开启服务器";
                    startServer.Background = new SolidColorBrush(Color.FromRgb(0, 139, 68));
                    //tabbtn3.IsEnabled = true;
                    setServer.IsEnabled = true;
                    setdefault.IsEnabled = true;
                    cmdtext.IsEnabled = false;
                    sendcmd.IsEnabled = false;
                    fastCMD.IsEnabled = false;
                    CmdProcess.CancelOutputRead();
                    CmdProcess.CancelErrorRead();
                    //ReadStdOutput = null;
                    CmdProcess.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived);
                    CmdProcess.ErrorDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived);
                    //ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
                    //outlog.AppendText("\n服务器已关闭！输入start来开启服务器.");
                    
                }
            }
        }

        private void sendcmd_Click(object sender, RoutedEventArgs e)
        {
            if (fastCMD.SelectedIndex == 1)
            {
                CmdProcess.StandardInput.WriteLine("op " + cmdtext.Text);
                cmdtext.Text = "";
                fastCMD.SelectedIndex = 0;
            }
            if (fastCMD.SelectedIndex == 2)
            {
                CmdProcess.StandardInput.WriteLine("deop " + cmdtext.Text);
                cmdtext.Text = "";
                fastCMD.SelectedIndex = 0;
            }
            if (fastCMD.SelectedIndex == 3)
            {
                CmdProcess.StandardInput.WriteLine("ban " + cmdtext.Text);
                cmdtext.Text = "";
                fastCMD.SelectedIndex = 0;
            }
            if (fastCMD.SelectedIndex == 4)
            {
                CmdProcess.StandardInput.WriteLine("say " + cmdtext.Text);
                cmdtext.Text = "";
                fastCMD.SelectedIndex = 0;
            }
            if (fastCMD.SelectedIndex == 5)
            {
                CmdProcess.StandardInput.WriteLine("pardon " + cmdtext.Text);
                cmdtext.Text = "";
                fastCMD.SelectedIndex = 0;
            }
            CmdProcess.StandardInput.WriteLine(cmdtext.Text);
            cmdtext.Text = "";
        }

        private void cmdtext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (fastCMD.SelectedIndex == 1)
                {
                    CmdProcess.StandardInput.WriteLine("op " + cmdtext.Text);
                    cmdtext.Text = "";
                    fastCMD.SelectedIndex = 0;
                }
                if (fastCMD.SelectedIndex == 2)
                {
                    CmdProcess.StandardInput.WriteLine("deop " + cmdtext.Text);
                    cmdtext.Text = "";
                    fastCMD.SelectedIndex = 0;
                }
                if (fastCMD.SelectedIndex == 3)
                {
                    CmdProcess.StandardInput.WriteLine("ban " + cmdtext.Text);
                    cmdtext.Text = "";
                    fastCMD.SelectedIndex = 0;
                }
                if (fastCMD.SelectedIndex == 4)
                {
                    CmdProcess.StandardInput.WriteLine("say " + cmdtext.Text);
                    cmdtext.Text = "";
                    fastCMD.SelectedIndex = 0;
                }
                if (fastCMD.SelectedIndex == 5)
                {
                    CmdProcess.StandardInput.WriteLine("pardon " + cmdtext.Text);
                    cmdtext.Text = "";
                    fastCMD.SelectedIndex = 0;
                }
                CmdProcess.StandardInput.WriteLine(cmdtext.Text);
                cmdtext.Text = "";
            }
        }

        private void autoStartserver_Click(object sender, RoutedEventArgs e)
        {
            if (autoStartserver.Content.ToString() == "关服自动开服:禁用")
            {
                autoserver = true;
                autoStartserver.Content = "关服自动开服:启用";
            }
            else
            {
                autoserver = false;
                autoStartserver.Content = "关服自动开服:禁用";
            }
        }
        private void onlineMode_Click(object sender, RoutedEventArgs e)
        {
            if (CmdProcess.HasExited == false)
            {
                MessageBox.Show("检测到服务器正在运行，正在关闭服务器...");
                CmdProcess.StandardInput.WriteLine("stop");
                //CmdProcess.WaitForExit();
                //CmdProcess.Kill();
                /*
                CmdProcess.CancelOutputRead();
                ReadStdOutput = null;
                CmdProcess.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived);
                ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
                timer1.Stop();*/
            }
            try
            {
                string path1 = serverbase + @"\server.properties";
                FileStream fs = new FileStream(path1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                string line;
                line = sr.ReadToEnd();
                line = line.Replace("online-mode=true", "online-mode=false");
                string path = serverbase + @"\server.properties";
                StreamWriter streamWriter = new StreamWriter(path);
                streamWriter.WriteLine(line);
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch
            {
                MessageBox.Show("出现错误，请手动修改server.properties文件或重试:" + "r0x3", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            /*
            outlog.Text = "";
            efflabel.Content = "已开启";
            timelabel.Content = string.Format("{0:00}:{1:00}:{2:00}", 0, 0, 0);
            sw = new Stopwatch();
            timer4.Tick += new EventHandler(timer4_Tick);
            timer4.Interval = TimeSpan.FromSeconds(1);
            sw.Start();
            timer4.Start();
            */
            MessageBox.Show("修改完毕！请重新启动服务器！");
        }

        private void setServerconfig_Click(object sender, RoutedEventArgs e)
        {
            Window wn = new Window7();
            wn.ShowDialog();
        }

        private void startServer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (startServer.Content.ToString() == "关闭服务器")
            {
                CmdProcess.Kill();
            }
        }

        private void moreTools_Click(object sender, RoutedEventArgs e)
        {
            if (moreTools.Content.ToString() != "收起")
            {
                sendcmd.Visibility = Visibility.Hidden;
                cmdtext.Visibility = Visibility.Hidden;
                moreTools.Content = "收起";
            }
            else
            {
                sendcmd.Visibility = Visibility.Visible;
                cmdtext.Visibility = Visibility.Visible;
                moreTools.Content = "更多功能";
            }
        }

        private void doneBtn1_Click(object sender, RoutedEventArgs e)
        {
            doneBtn1.IsEnabled = false;
            if (useJv8.IsChecked == true)
            {
                jAva.Text = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8\bin\java.exe";
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8"))
                {
                    if (useJVMauto.IsChecked == true)
                    {
                        line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a |-b " + bAse.Text + "|*";
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(line);
                        sw.Flush();
                        sw.Close();
                        fs.Close();

                        serverjava = "\"" +jAva.Text+ "\"";
                        serverserver = "\"" + server.Text + "\"";
                        serverJVM = "";
                        serverbase = bAse.Text;
                    }
                    else
                    {
                        line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a " + "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M" + "|-b " + bAse.Text + "|*";
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(line);
                        sw.Flush();
                        sw.Close();
                        fs.Close();

                        serverjava = "\"" + jAva.Text + "\"";
                        serverserver = "\"" + server.Text + "\"";
                        serverJVM = "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M";
                        serverbase = bAse.Text;
                    }

                    MessageBox.Show("保存完毕！", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    doneBtn1.IsEnabled = true;
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
                            updatedown = false;
                            jAva.Text = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8\bin\java.exe";
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
                            updatedown = false;
                            jAva.Text = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8\bin\java.exe";
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

                }

                /*
                sJVM.IsSelected = true;
                sJVM.IsEnabled = true;
                sserver.IsEnabled = false;
                MainWindow.serverserver = txb3.Text;*/
            }
            if (useJv16.IsChecked == true)
            {
                jAva.Text = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java16\bin\java.exe";
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java16"))
                {
                    if (useJVMauto.IsChecked == true)
                    {
                        line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a |-b " + bAse.Text + "|*";
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(line);
                        sw.Flush();
                        sw.Close();
                        fs.Close();

                        serverjava = "\"" + jAva.Text + "\"";
                        serverserver = "\"" + server.Text + "\"";
                        serverJVM = "";
                        serverbase = bAse.Text;
                    }
                    else
                    {
                        line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a " + "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M" + "|-b " + bAse.Text + "|*";
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(line);
                        sw.Flush();
                        sw.Close();
                        fs.Close();

                        serverjava = "\"" + jAva.Text + "\"";
                        serverserver = "\"" + server.Text + "\"";
                        serverJVM = "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M";
                        serverbase = bAse.Text;
                    }

                    MessageBox.Show("保存完毕！", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    doneBtn1.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("下载Java即代表您接受Java的服务条款https://www.oracle.com/downloads/licenses/javase-license1.html");
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java16\bin\java.exe"))
                    {
                        lodingIco.Visibility = Visibility.Visible;
                        DownjavaName = "Java16";
                        updatedown = false;
                        jAva.Text = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java16\bin\java.exe";
                        //RealAction("https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=EbapBNLCCwRLoFr2kxeCUdcBYNtGdsQO2h1MlzgFU3VZbQ");
                        Url = "https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=EbapBNLCCwRLoFr2kxeCUdcBYNtGdsQO2h1MlzgFU3VZbQ";
                        Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe";
                        Thread thread = new Thread(DownloadFile);
                        thread.Start();
                        /*
                        Form4 fw = new Form4();
                        fw.ShowDialog();*/
                    }

                }

                /*
                sJVM.IsSelected = true;
                sJVM.IsEnabled = true;
                sserver.IsEnabled = false;
                MainWindow.serverserver = txb3.Text;*/
            }
            if (useJv17.IsChecked == true)
            {
                jAva.Text = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java17\bin\java.exe";
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java17"))
                {
                    if (useJVMauto.IsChecked == true)
                    {
                        line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a |-b " + bAse.Text + "|*";
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(line);
                        sw.Flush();
                        sw.Close();
                        fs.Close();

                        serverjava = "\"" + jAva.Text + "\"";
                        serverserver = "\"" + server.Text + "\"";
                        serverJVM = "";
                        serverbase = bAse.Text;
                    }
                    else
                    {
                        line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a " + "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M" + "|-b " + bAse.Text + "|*";
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(line);
                        sw.Flush();
                        sw.Close();
                        fs.Close();

                        serverjava = "\"" + jAva.Text + "\"";
                        serverserver = "\"" + server.Text + "\"";
                        serverJVM = "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M";
                        serverbase = bAse.Text;
                    }

                    MessageBox.Show("保存完毕！", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    doneBtn1.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("下载Java即代表您接受Java的服务条款https://www.oracle.com/downloads/licenses/javase-license1.html");
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java17\bin\java.exe"))
                    {
                        lodingIco.Visibility = Visibility.Visible;
                        DownjavaName = "Java17";
                        updatedown = false;
                        jAva.Text = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java17\bin\java.exe";
                        Url = "https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=EUxH8cdGAlxOkNiZGunIefEBwrdoMM5wPIb5h9xDpiWd_A";
                        Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe";
                        Thread thread = new Thread(DownloadFile);
                        thread.Start();
                        //RealAction("https://oceansky12337-my.sharepoint.com/personal/makabaka_oceansky12337_onmicrosoft_com/_layouts/52/download.aspx?share=EUxH8cdGAlxOkNiZGunIefEBwrdoMM5wPIb5h9xDpiWd_A");
                        /*
                        Form4 fw = new Form4();
                        fw.ShowDialog();*/
                    }

                }
                /*
            sJVM.IsSelected = true;
            sJVM.IsEnabled = true;
            sserver.IsEnabled = false;
            MainWindow.serverserver = txb3.Text;*/
            }
            if (useSelf.IsChecked == true)
            {
                if (useJVMauto.IsChecked == true)
                {
                    line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a |-b " + bAse.Text + "|*";
                    FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(line);
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                    serverjava = "\"" + jAva.Text + "\"";
                    serverserver = "\"" + server.Text + "\"";
                    serverJVM = "";
                    serverbase = bAse.Text;
                }
                else
                {
                    line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a " + "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M" + "|-b " + bAse.Text + "|*";
                    FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(line);
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                    serverjava = "\"" + jAva.Text + "\"";
                    serverserver = "\"" + server.Text + "\"";
                    serverJVM = "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M";
                    serverbase = bAse.Text;
                }

                MessageBox.Show("保存完毕！", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
                doneBtn1.IsEnabled = true;
            }
            if (useJvpath.IsChecked == true)
            {
                if (useJVMauto.IsChecked == true)
                {
                    line = "*|-j Java|-s " + "\"" + server.Text + "\"" + "|-a |-b " + bAse.Text + "|*";
                    FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(line);
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                    serverjava = "Java";
                    serverserver = "\"" + server.Text + "\"";
                    serverJVM = "";
                    serverbase = bAse.Text;
                }
                else
                {
                    line = "*|-j Java|-s " + "\"" + server.Text + "\"" + "|-a " + "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M" + "|-b " + bAse.Text + "|*";
                    FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(line);
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                    serverjava = "Java";
                    serverserver = "\"" + server.Text + "\"";
                    serverJVM = "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M";
                    serverbase = bAse.Text;
                }

                MessageBox.Show("保存完毕！", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
                doneBtn1.IsEnabled = true;
            }
        }
        private void StartFrpc()
        {
            try
            {
                CmdProcess1.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc.exe";
                //CmdProcess.StartInfo.FileName = StartFileName;
                CmdProcess1.StartInfo.Arguments = "-c frpc";
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory + "ServerLauncher");
                CmdProcess1.StartInfo.CreateNoWindow = true;
                CmdProcess1.StartInfo.UseShellExecute = false;
                CmdProcess1.StartInfo.RedirectStandardInput = true;
                CmdProcess1.StartInfo.RedirectStandardOutput = true;
                CmdProcess1.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived1);
                CmdProcess1.Start();
                CmdProcess1.BeginOutputReadLine();
            }
            catch (Exception e)
            {
                MessageBox.Show("出现错误，请重试:" + "r0x1" + e, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        private void p_OutputDataReceived1(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Dispatcher.Invoke(ReadStdOutput1, new object[] { e.Data });
            }
        }
        public static string DecodeBase64(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
        private void ReadStdOutputAction1(string msg)
        {
            frpcOutlog.Text = frpcOutlog.Text + msg;
            if (msg.IndexOf("login") + 1 != 0)
            {
                if (msg.IndexOf("success") + 1 != 0)
                {
                    frpcOutlog.Text = frpcOutlog.Text + "\n登录服务器成功！\n";
                }
                if (msg.IndexOf("match") + 1 != 0)
                {
                    if (msg.IndexOf("token") + 1 != 0)
                    {
                        frpcOutlog.Text = frpcOutlog.Text + "\n重新连接服务器...\n";
                        string frpcserver = frpc.Substring(0, frpc.IndexOf(".")) + "*";
                        WebClient MyWebClient = new WebClient();
                        MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                        byte[] pageData = MyWebClient.DownloadData("http://115.220.5.81:8081/web/getfrpc.txt");
                        string pageHtml11 = Encoding.UTF8.GetString(pageData);

                        int IndexofA1 = pageHtml11.IndexOf("*");
                        string Ru1 = pageHtml11.Substring(IndexofA1 + 1);
                        string pageHtml2 = Ru1.Substring(0, Ru1.IndexOf("*"));

                        WebClient MyWebClient11 = new WebClient();
                        MyWebClient11.Credentials = CredentialCache.DefaultCredentials;
                        byte[] pageData11 = MyWebClient11.DownloadData(pageHtml2);
                        string pageHtml1 = Encoding.UTF8.GetString(pageData11);
                        try
                        {
                            int IndexofA = pageHtml1.IndexOf(frpcserver);
                            string Ru = pageHtml1.Substring(IndexofA + 3);
                            string a111 = Ru.Substring(0, Ru.IndexOf("*"));
                            //MessageBox.Show(a111);

                            WebClient MyWebClient1 = new WebClient();
                            MyWebClient1.Credentials = CredentialCache.DefaultCredentials;
                            byte[] pageData1 = MyWebClient1.DownloadData(a111);
                            string pageHtml = Encoding.UTF8.GetString(pageData1);
                            string decode = DecodeBase64(pageHtml);
                            string aaa = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc");
                            int IndexofA2 = aaa.IndexOf("token=");
                            string Ru2 = aaa.Substring(IndexofA2 + 6);
                            string a200 = Ru2.Substring(0, Ru2.IndexOf("\n"));
                            aaa = aaa.Replace(a200, decode);
                            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc", aaa);
                            CmdProcess1.CancelOutputRead();
                            //ReadStdOutput = null;
                            CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                            StartFrpc();
                        }
                        catch
                        {
                            MessageBox.Show("内网映射桥接失败！请重试！\n错误代码：" + "Mw000x0001", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            try
                            {
                                CmdProcess1.CancelOutputRead();
                                //ReadStdOutput = null;
                                CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                                startfrpc.Content = "启动内网映射";
                            }
                            catch
                            {
                                startfrpc.Content = "启动内网映射";
                            }

                        }


                        //MessageBox.Show(frpcserver);
                        /*
                        if (frpc.IndexOf("sh") + 1 != 0)
                        {
                            WebClient MyWebClient = new WebClient();
                            MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                            byte[] pageData = MyWebClient.DownloadData("http://1.116.201.220/");
                            string pageHtml = Encoding.UTF8.GetString(pageData);
                            string decode = DecodeBase64(pageHtml);
                            string aaa = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc");
                            int IndexofA2 = aaa.IndexOf("token=");
                            string Ru2 = aaa.Substring(IndexofA2 + 6);
                            string a200 = Ru2.Substring(0, Ru2.IndexOf("\n"));
                            aaa = aaa.Replace(a200, decode);
                            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc", aaa);
                            CmdProcess1.CancelOutputRead();
                            //ReadStdOutput = null;
                            CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                            StartFrpc();
                        }
                        if (frpc.IndexOf("gz") + 1 != 0)
                        {
                            WebClient MyWebClient = new WebClient();
                            MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                            byte[] pageData = MyWebClient.DownloadData("http://119.29.66.223/");
                            string pageHtml = Encoding.UTF8.GetString(pageData);
                            string decode = DecodeBase64(pageHtml);
                            string aaa = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc");
                            int IndexofA2 = aaa.IndexOf("token=");
                            string Ru2 = aaa.Substring(IndexofA2 + 6);
                            string a200 = Ru2.Substring(0, Ru2.IndexOf("\n"));
                            aaa = aaa.Replace(a200, decode);
                            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc", aaa);
                            CmdProcess1.CancelOutputRead();
                            //ReadStdOutput = null;
                            CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                            StartFrpc();
                        }*/
                    }
                }
            }
            if (msg.IndexOf("start") + 1 != 0)
            {
                if (msg.IndexOf("success") + 1 != 0)
                {
                    frpcOutlog.Text = frpcOutlog.Text + "\n内网映射桥接成功！\n";
                }
                if (msg.IndexOf("error") + 1 != 0)
                {
                    frpcOutlog.Text = frpcOutlog.Text + "\n内网映射桥接失败！\n";
                    try
                    {
                        CmdProcess1.Kill();
                        CmdProcess1.CancelOutputRead();
                        //ReadStdOutput = null;
                        CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                        startfrpc.Content = "启动内网映射";
                    }
                    catch
                    {
                        try
                        {
                            CmdProcess1.CancelOutputRead();
                            //ReadStdOutput = null;
                            CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                            startfrpc.Content = "启动内网映射";
                        }
                        catch
                        {
                            startfrpc.Content = "启动内网映射";
                        }
                    }
                    if (msg.IndexOf("port already used") + 1 != 0)
                    {
                        frpcOutlog.Text = frpcOutlog.Text + "\n远程端口被占用，请重新配置！\n";
                    }
                    if (msg.IndexOf("proxy name") + 1 != 0)
                    {
                        if (msg.IndexOf("already in use") + 1 != 0)
                        {
                            frpcOutlog.Text = frpcOutlog.Text + "\n此QQ号已被占用！请重启电脑再试或联系作者！\n";
                        }
                    }
                    //frpcOutlog.Text = frpcOutlog.Text + "\n端口被占用！请检查是否有进程占用端口或重新配置内网映射！\n";
                }
                if (msg.IndexOf("reconnect") + 1 != 0)
                {
                    if (msg.IndexOf("error") + 1 != 0)
                    {
                        if (msg.IndexOf("token") + 1 != 0)
                        {
                            frpcOutlog.Text = frpcOutlog.Text + "\n重新连接服务器...\n";
                            string frpcserver = frpc.Substring(0, frpc.IndexOf(".")) + "*";
                            WebClient MyWebClient = new WebClient();
                            MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                            byte[] pageData = MyWebClient.DownloadData("http://115.220.5.81:8081/web/getfrpc.txt");
                            string pageHtml11 = Encoding.UTF8.GetString(pageData);

                            int IndexofA1 = pageHtml11.IndexOf("*");
                            string Ru1 = pageHtml11.Substring(IndexofA1 + 1);
                            string pageHtml2 = Ru1.Substring(0, Ru1.IndexOf("*"));

                            WebClient MyWebClient11 = new WebClient();
                            MyWebClient11.Credentials = CredentialCache.DefaultCredentials;
                            byte[] pageData11 = MyWebClient11.DownloadData(pageHtml2);
                            string pageHtml1 = Encoding.UTF8.GetString(pageData11);
                            try
                            {
                                int IndexofA = pageHtml1.IndexOf(frpcserver);
                                string Ru = pageHtml1.Substring(IndexofA + 3);
                                string a111 = Ru.Substring(0, Ru.IndexOf("*"));
                                //MessageBox.Show(a111);

                                WebClient MyWebClient1 = new WebClient();
                                MyWebClient1.Credentials = CredentialCache.DefaultCredentials;
                                byte[] pageData1 = MyWebClient1.DownloadData(a111);
                                string pageHtml = Encoding.UTF8.GetString(pageData1);
                                string decode = DecodeBase64(pageHtml);
                                string aaa = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc");
                                int IndexofA2 = aaa.IndexOf("token=");
                                string Ru2 = aaa.Substring(IndexofA2 + 6);
                                string a200 = Ru2.Substring(0, Ru2.IndexOf("\n"));
                                aaa = aaa.Replace(a200, decode);
                                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc", aaa);
                                CmdProcess1.CancelOutputRead();
                                //ReadStdOutput = null;
                                CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                                StartFrpc();
                            }
                            catch
                            {
                                MessageBox.Show("内网映射桥接失败！请重试！\n错误代码：" + "Mw000x0001", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                try
                                {
                                    CmdProcess1.CancelOutputRead();
                                    //ReadStdOutput = null;
                                    CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                                    startfrpc.Content = "启动内网映射";
                                }
                                catch
                                {
                                    startfrpc.Content = "启动内网映射";
                                }

                            }


                            //MessageBox.Show(frpcserver);
                            /*
                            if (frpc.IndexOf("sh") + 1 != 0)
                            {
                                WebClient MyWebClient = new WebClient();
                                MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                                byte[] pageData = MyWebClient.DownloadData("http://1.116.201.220/");
                                string pageHtml = Encoding.UTF8.GetString(pageData);
                                string decode = DecodeBase64(pageHtml);
                                string aaa = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc");
                                int IndexofA2 = aaa.IndexOf("token=");
                                string Ru2 = aaa.Substring(IndexofA2 + 6);
                                string a200 = Ru2.Substring(0, Ru2.IndexOf("\n"));
                                aaa = aaa.Replace(a200, decode);
                                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc", aaa);
                                CmdProcess1.CancelOutputRead();
                                //ReadStdOutput = null;
                                CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                                StartFrpc();
                            }
                            if (frpc.IndexOf("gz") + 1 != 0)
                            {
                                WebClient MyWebClient = new WebClient();
                                MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                                byte[] pageData = MyWebClient.DownloadData("http://119.29.66.223/");
                                string pageHtml = Encoding.UTF8.GetString(pageData);
                                string decode = DecodeBase64(pageHtml);
                                string aaa = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc");
                                int IndexofA2 = aaa.IndexOf("token=");
                                string Ru2 = aaa.Substring(IndexofA2 + 6);
                                string a200 = Ru2.Substring(0, Ru2.IndexOf("\n"));
                                aaa = aaa.Replace(a200, decode);
                                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc", aaa);
                                CmdProcess1.CancelOutputRead();
                                //ReadStdOutput = null;
                                CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                                StartFrpc();
                            }*/
                        }
                    }
                }
            }
            frpcOutlog.ScrollToEnd();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\" + DownjavaName + @"\bin\java.exe"))
            {
                try
                {
                    if (useJVMauto.IsChecked == true)
                    {
                        line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a |-b " + bAse.Text + "|*";
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(line);
                        sw.Flush();
                        sw.Close();
                        fs.Close();

                        serverjava = "\"" + jAva.Text + "\"";
                        serverserver = "\"" + server.Text + "\"";
                        serverJVM = "";
                        serverbase = bAse.Text;
                    }
                    else
                    {
                        line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a " + "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M" + "|-b " + bAse.Text + "|*";
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(line);
                        sw.Flush();
                        sw.Close();
                        fs.Close();

                        serverjava = "\"" + jAva.Text + "\"";
                        serverserver = "\"" + server.Text + "\"";
                        serverJVM = "-Xms" + memorySlider.LowerValue.ToString("f0") + "M" + " -Xmx" + memorySlider.UpperValue.ToString("f0") + "M";
                        serverbase = bAse.Text;
                    }
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe");
                    MessageBox.Show("保存完毕！", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    downout.Content = "安装成功！";
                    doneBtn1.IsEnabled = true;
                    lodingIco.Visibility = Visibility.Hidden;
                    timer3.Stop();
                }
                catch
                {
                    return;
                }
                /*
                CmdProcess1.CancelOutputRead();
                //ReadStdOutput = null;
                CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                try
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java.exe");
                    downout.Content = "完成";
                    line = "*|-j " + "\"" + jAva.Text + "\"" + "|-s " + "\"" + server.Text + "\"" + "|-a " + jVM.Text + "|-b " + bAse.Text + "|*";
                    FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\server.ini", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(line);
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                    serverjava = jAva.Text;
                    serverserver = server.Text;
                    serverJVM = jVM.Text;
                    serverbase = bAse.Text;

                    MessageBox.Show("保存完毕！", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    doneBtn1.IsEnabled = true;
                    timer2.Stop();
                }
                catch
                {}*/
            }
        }

        private void a01_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openfile.Title = "请选择文件";
            openfile.Filter = "JAR文件|*.jar|所有文件类型|*.*";
            var res = openfile.ShowDialog();
            if (res == true)
            {
                server.Text = openfile.FileName;
            }
        }

        private void downServer_Click(object sender, RoutedEventArgs e)
        {
            Window wn = new Window4();
            wn.ShowDialog();
            server.Text = serverserver.Replace("\"", "");
        }

        private void a02_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bAse.Text = dialog.SelectedPath;
            }
        }

        private void a03_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openfile.Title = "请选择文件，通常为java.exe";
            openfile.Filter = "EXE文件|*.exe|所有文件类型|*.*";
            var res = openfile.ShowDialog();
            if (res == true)
            {
                jAva.Text = openfile.FileName;
            }
        }

        private void startfrpc_Click(object sender, RoutedEventArgs e)
        {
            if (startfrpc.Content.ToString() == "启动内网映射")
            {
                StartFrpc();
                startfrpc.Content = "关闭内网映射";
                frpcOutlog.Text = "";
            }
            else
            {
                try
                {
                    CmdProcess1.Kill();
                    CmdProcess1.CancelOutputRead();
                    //ReadStdOutput = null;
                    CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                    startfrpc.Content = "启动内网映射";
                }
                catch
                {
                    try
                    {
                        CmdProcess1.CancelOutputRead();
                        //ReadStdOutput = null;
                        CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                        startfrpc.Content = "启动内网映射";
                    }
                    catch
                    {
                        startfrpc.Content = "启动内网映射";
                    }
                }
            }
        }

        private void setfrpc_Click(object sender, RoutedEventArgs e)
        {
            Window fw = new Window3();
            fw.ShowDialog();
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc"))
                {
                    if (frpc != null)
                    {
                        frplab1.Content = "您的内网映射已就绪，请点击“启动内网映射”来开启";
                        frplab3.Content = frpc;
                        copyFrpc.IsEnabled = true;
                        startfrpc.IsEnabled = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("出现错误，请重试:" + "m0x3");
            }
        }
        /*
        private void tabbtn1_Click(object sender, RoutedEventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            tabbtn1.IsEnabled = false;
            timer3.Stop();
            try
            {
                if (CmdProcess.HasExited != true)
                {
                    tabbtn2.IsEnabled = true;
                    tabbtn3.IsEnabled = false;
                    tabbtn4.IsEnabled = true;
                    tabbtn5.IsEnabled = true;
                }
                else
                {
                    tabbtn2.IsEnabled = true;
                    tabbtn3.IsEnabled = true;
                    tabbtn4.IsEnabled = true;
                    tabbtn5.IsEnabled = true;
                }
            }
            catch
            {
                tabbtn2.IsEnabled = true;
                tabbtn3.IsEnabled = true;
                tabbtn4.IsEnabled = true;
                tabbtn5.IsEnabled = true;
            }
        }

        private void tabbtn2_Click(object sender, RoutedEventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            tabbtn2.IsEnabled = false;
            timer3.Stop();
            try
            {
                if (CmdProcess.HasExited != true)
                {
                    tabbtn1.IsEnabled = true;
                    tabbtn3.IsEnabled = false;
                    tabbtn4.IsEnabled = true;
                    tabbtn5.IsEnabled = true;
                }
                else
                {
                    tabbtn1.IsEnabled = true;
                    tabbtn3.IsEnabled = true;
                    tabbtn4.IsEnabled = true;
                    tabbtn5.IsEnabled = true;
                }
            }
            catch
            {
                tabbtn1.IsEnabled = true;
                tabbtn3.IsEnabled = true;
                tabbtn4.IsEnabled = true;
                tabbtn5.IsEnabled = true;
            }

        }*/

        private static int GetPhisicalMemory()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher();   //用于查询一些如系统信息的管理对象 
            searcher.Query = new SelectQuery("Win32_PhysicalMemory ", "", new string[] { "Capacity" });//设置查询条件 
            ManagementObjectCollection collection = searcher.Get();   //获取内存容量 
            ManagementObjectCollection.ManagementObjectEnumerator em = collection.GetEnumerator();

            long capacity = 0;
            while (em.MoveNext())
            {
                ManagementBaseObject baseObj = em.Current;
                if (baseObj.Properties["Capacity"].Value != null)
                {
                    try
                    {
                        capacity += long.Parse(baseObj.Properties["Capacity"].Value.ToString());
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
            return (int)(capacity / 1024 / 1024);
        }
        /*
        private void tabbtn3_Click(object sender, RoutedEventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            server.Text = serverserver.Replace("\"", "");
            //jVM.Text = serverJVM;
            memorySlider.Maximum = GetPhisicalMemory();
            bAse.Text = serverbase;
            jAva.Text = serverjava.Replace("\"", "");
            if (jAva.Text == "Java")
            {
                useJvpath.IsChecked = true;
            }
            if (jAva.Text == AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8\bin\java.exe")
            {
                useJv8.IsChecked = true;
            }
            if (jAva.Text == AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java16\bin\java.exe")
            {
                useJv16.IsChecked = true;
            }
            if (jAva.Text == AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java17\bin\java.exe")
            {
                useJv17.IsChecked = true;
            }
            if (serverJVM == "")
            {
                memorySlider.IsEnabled = false;
                useJVMself.IsChecked = false;
                useJVMauto.IsChecked = true;
            }
            else
            {
                memorySlider.IsEnabled = true;
                useJVMauto.IsChecked = false;
                useJVMself.IsChecked = true;
                int IndexofA2 = serverJVM.IndexOf("-Xms");
                string Ru2 = serverJVM.Substring(IndexofA2 + 4);
                string a200 = Ru2.Substring(0, Ru2.IndexOf("M"));

                int IndexofA6 = serverJVM.IndexOf("-Xmx");
                string Ru6 = serverJVM.Substring(IndexofA6 + 4);
                string a600 = Ru6.Substring(0, Ru6.IndexOf("M"));

                memorySlider.LowerValue = int.Parse(a200);
                memorySlider.UpperValue = int.Parse(a600);
                memoryInfo.Content = "最小内存：" + a200 + "M 最大内存：" + a600 + "M";
            }
            timer3.Tick += new EventHandler(timer3_Tick);
            timer3.Interval = TimeSpan.FromMilliseconds(100);
            timer3.Start();
            tabbtn3.IsEnabled = false;
            tabbtn1.IsEnabled = true;
            tabbtn2.IsEnabled = true;
            tabbtn4.IsEnabled = true;
            tabbtn5.IsEnabled = true;
        }

        private void tabbtn4_Click(object sender, RoutedEventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            tabbtn4.IsEnabled = false;
            timer3.Stop();
            try
            {
                if (CmdProcess.HasExited != true)
                {
                    tabbtn1.IsEnabled = true;
                    tabbtn2.IsEnabled = true;
                    tabbtn3.IsEnabled = false;
                    tabbtn5.IsEnabled = true;
                }
                else
                {
                    tabbtn1.IsEnabled = true;
                    tabbtn2.IsEnabled = true;
                    tabbtn3.IsEnabled = true;
                    tabbtn5.IsEnabled = true;
                }
            }
            catch
            {
                tabbtn1.IsEnabled = true;
                tabbtn2.IsEnabled = true;
                tabbtn3.IsEnabled = true;
                tabbtn5.IsEnabled = true;
            }
        }

        private void tabbtn5_Click(object sender, RoutedEventArgs e)
        {
            tabControl1.SelectedIndex = 4;
            tabbtn5.IsEnabled = false;
            timer3.Stop();
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc"))
                {
                    if (frpc!=null)
                    {
                        frplab1.Content = "您的内网映射已就绪，请点击“启动内网映射”来开启";
                        frplab3.Content = frpc;
                        copyFrpc.IsEnabled = true;
                        startfrpc.IsEnabled = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("出现错误，请重试:" + "m0x3");
            }
            try
            {
                if (CmdProcess.HasExited != true)
                {
                    tabbtn1.IsEnabled = true;
                    tabbtn2.IsEnabled = true;
                    tabbtn3.IsEnabled = false;
                    tabbtn4.IsEnabled = true;
                }
                else
                {
                    tabbtn1.IsEnabled = true;
                    tabbtn2.IsEnabled = true;
                    tabbtn3.IsEnabled = true;
                    tabbtn4.IsEnabled = true;
                }
            }
            catch
            {
                tabbtn1.IsEnabled = true;
                tabbtn2.IsEnabled = true;
                tabbtn3.IsEnabled = true;
                tabbtn4.IsEnabled = true;
            }
        }*/
        void timer2_Tick(object sender, EventArgs e)
        {
            if (memorySlider.IsEnabled == true)
            {
                memoryInfo.Content = "最小内存：" + memorySlider.LowerValue.ToString("f0") + "M 最大内存：" + memorySlider.UpperValue.ToString("f0") + "M";
            }
        }

        private void useJVMauto_Click(object sender, RoutedEventArgs e)
        {
            if (useJVMauto.IsChecked == true)
            {
                memorySlider.IsEnabled = false;
                useJVMself.IsChecked = false;
            }
            else
            {
                useJVMauto.IsChecked = true;
            }
        }

        private void useJVMself_Click(object sender, RoutedEventArgs e)
        {
            if (useJVMself.IsChecked == true)
            {
                memorySlider.IsEnabled = true;
                useJVMauto.IsChecked = false;
            }
            else
            {
                useJVMself.IsChecked = true;
            }
        }

        private void copyFrpc_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetDataObject(frpc);
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                timer2.Stop();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                timer2.Stop();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                server.Text = serverserver.Replace("\"", "");
                //jVM.Text = serverJVM;
                memorySlider.Maximum = GetPhisicalMemory();
                bAse.Text = serverbase;
                jAva.Text = serverjava.Replace("\"", "");
                if (jAva.Text == "Java")
                {
                    useJvpath.IsChecked = true;
                }
                if (jAva.Text == AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java8\bin\java.exe")
                {
                    useJv8.IsChecked = true;
                }
                if (jAva.Text == AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java16\bin\java.exe")
                {
                    useJv16.IsChecked = true;
                }
                if (jAva.Text == AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Java17\bin\java.exe")
                {
                    useJv17.IsChecked = true;
                }
                if (serverJVM == "")
                {
                    memorySlider.IsEnabled = false;
                    useJVMself.IsChecked = false;
                    useJVMauto.IsChecked = true;
                    memoryInfo.Content = "现在为自动分配";
                }
                else
                {
                    memorySlider.IsEnabled = true;
                    useJVMauto.IsChecked = false;
                    useJVMself.IsChecked = true;
                    int IndexofA2 = serverJVM.IndexOf("-Xms");
                    string Ru2 = serverJVM.Substring(IndexofA2 + 4);
                    string a200 = Ru2.Substring(0, Ru2.IndexOf("M"));

                    int IndexofA6 = serverJVM.IndexOf("-Xmx");
                    string Ru6 = serverJVM.Substring(IndexofA6 + 4);
                    string a600 = Ru6.Substring(0, Ru6.IndexOf("M"));

                    memorySlider.LowerValue = int.Parse(a200);
                    memorySlider.UpperValue = int.Parse(a600);
                    memoryInfo.Content = "最小内存：" + a200 + "M 最大内存：" + a600 + "M";
                }
                timer2.Tick += new EventHandler(timer2_Tick);
                timer2.Interval = TimeSpan.FromMilliseconds(100);
                timer2.Start();
            }
            if (tabControl1.SelectedIndex == 3)
            {
                timer2.Stop();
            }
            if (tabControl1.SelectedIndex == 4)
            {
                timer2.Stop();
                try
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc"))
                    {
                        if (frpc != null)
                        {
                            frplab1.Content = "您的内网映射已就绪，请点击“启动内网映射”来开启";
                            frplab3.Content = frpc;
                            copyFrpc.IsEnabled = true;
                            startfrpc.IsEnabled = true;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("出现错误，请重试:" + "m0x3");
                }
            }
            if (tabControl1.SelectedIndex == 5)
            {
                timer2.Stop();
            }
        }

        private void plugins_mods_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Directory.Exists(serverbase + @"\plugins"))
            {
                pluginslist.Items.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(serverbase + @"\plugins");
                FileInfo[] file = directoryInfo.GetFiles("*.jar");
                foreach (FileInfo f in file)
                {
                    pluginslist.Items.Add(f.Name);
                }
                if (Directory.Exists(serverbase + @"\mods"))
                {
                    lab001.Content = "已检测到plugins和mods文件夹，以下为您的插件及模组";
                    openpluginsDir.IsEnabled = true;
                    openmodsDir.IsEnabled = true;
                    modslist.Items.Clear();
                    DirectoryInfo directoryInfo1 = new DirectoryInfo(serverbase + @"\mods");
                    FileInfo[] file1 = directoryInfo1.GetFiles("*.jar");
                    foreach (FileInfo f1 in file1)
                    {
                        modslist.Items.Add(f1.Name);
                    }
                }
                else
                {
                    modslist.Items.Clear();
                    lab001.Content = "已检测到plugins文件夹，以下为您的插件";
                    openpluginsDir.IsEnabled = true;
                    openmodsDir.IsEnabled = false;
                }
            }
            else
            {
                if (Directory.Exists(serverbase + @"\mods"))
                {
                    pluginslist.Items.Clear();
                    lab001.Content = "已检测到mods文件夹，以下为您的模组";
                    openmodsDir.IsEnabled = true;
                    modslist.Items.Clear();
                    DirectoryInfo directoryInfo = new DirectoryInfo(serverbase + @"\mods");
                    FileInfo[] file = directoryInfo.GetFiles("*.jar");
                    foreach (FileInfo f in file)
                    {
                        modslist.Items.Add(f.Name);
                    }
                }
                else
                {
                    openpluginsDir.IsEnabled = false;
                    openmodsDir.IsEnabled = false;
                }
            }
            
        }

        private void openpluginsDir_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.Arguments = serverbase + @"\plugins";
            p.Start();
        }

        private void openmodsDir_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.Arguments = serverbase + @"\mods";
            p.Start();
        }

        private void openspigot_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.spigotmc.org/");
        }

        private void opencurseforge_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.curseforge.com/");
        }
    }
}
