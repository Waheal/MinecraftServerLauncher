using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    /// Window4.xaml 的交互逻辑
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
        }
        //DispatcherTimer timer7 = new DispatcherTimer();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //timer7.Tick += new EventHandler(timer7_Tick);
            //timer7.Interval = TimeSpan.FromSeconds(2);
            // timer7.Start();
            Thread thread = new Thread(GetServer);
            thread.Start();
            isgetserverOpen = true;
        }
        //服务端下载
        private void serverlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (serverlist.SelectedIndex.ToString() != "-1")
            {
                int url = serverlist.SelectedIndex;
                string filename = serverlist.SelectedItem.ToString();
                Url = serverdownlist.Items[url].ToString();
                Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Spigot-" + filename + ".jar";
                Thread thread = new Thread(DownloadFile);
                thread.Start();
            }
        }
        //服务端下载
        private void serverlist2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (serverlist2.SelectedIndex.ToString() != "-1")
            {
                int url = serverlist2.SelectedIndex;
                string filename = serverlist2.SelectedItem.ToString();
                Url = serverdownlist2.Items[url].ToString();
                Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\CraftBukkit-" + filename + ".jar";
                Thread thread = new Thread(DownloadFile);
                thread.Start();
            }
        }
        //服务端下载
        private void serverlist3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (serverlist3.SelectedIndex.ToString() != "-1")
            {
                int url = serverlist3.SelectedIndex;
                string filename = serverlist3.SelectedItem.ToString();
                Url = serverdownlist3.Items[url].ToString();
                Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\Paper-" + filename + ".jar";
                Thread thread = new Thread(DownloadFile);
                thread.Start();
            }
        }
        //服务端下载
        private void serverlist4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (serverlist4.SelectedIndex.ToString() != "-1")
            {
                int url = serverlist4.SelectedIndex;
                string filename = serverlist4.SelectedItem.ToString();
                Url = serverdownlist4.Items[url].ToString();
                Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\MojangServer-" + filename + ".jar";
                Thread thread = new Thread(DownloadFile);
                thread.Start();
            }
        }
        //服务端下载
        private void serverlist5_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (serverlist5.SelectedIndex.ToString() != "-1")
            {
                int url = serverlist5.SelectedIndex;
                string filename = serverlist5.SelectedItem.ToString();
                Url = serverdownlist5.Items[url].ToString();
                Filename = AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\" + filename + ".jar";
                Thread thread = new Thread(DownloadFile);
                thread.Start();
            }
        }
        void DownloadFile()
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                serverlist.IsEnabled = false;
                serverlist2.IsEnabled = false;
                serverlist3.IsEnabled = false;
                serverlist4.IsEnabled = false;
                serverlist5.IsEnabled = false;
                downmsg1.Content = "连接下载地址中...";
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
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    if (downprog != null)
                    {
                        downprog.Maximum = (int)totalBytes;
                    }
                });
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
                        if (downprog != null)
                        {
                            downprog.Value = (int)totalDownloadedByte;
                        }
                        downmsg1.Content = "下载中，进度" + percent.ToString("f2") + "%";
                    });
                    DispatcherHelper.DoEvents();
                }
                so.Close();
                st.Close();
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    downmsg1.Content = "下载成功，已自动为您选择该服务端（默认下载目录为软件运行目录的ServerLauncher文件夹）";
                    MainWindow.serverserver = Filename;
                    serverlist.IsEnabled = true;
                    serverlist2.IsEnabled = true;
                    serverlist3.IsEnabled = true;
                    serverlist4.IsEnabled = true;
                    serverlist5.IsEnabled = true;
                });
            }
            catch
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    downmsg1.Content = "发生错误，请重试:" + "w4x1";
                });
            }
        }
        void GetServer()
        {
            try
            {
                WebClient MyWebClient1 = new WebClient();
                MyWebClient1.Credentials = CredentialCache.DefaultCredentials;
                byte[] pageData1 = MyWebClient1.DownloadData("http://115.220.5.81:8081/web/getserver.txt");
                string pageHtml1 = Encoding.UTF8.GetString(pageData1);
                HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(pageHtml1);
                HttpWebResponse myrp;
                myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                Stream st = myrp.GetResponseStream();
                FileStream so = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher/serverlist.json", FileMode.Create);
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
                    DispatcherHelper.DoEvents();
                }
                so.Close();
                st.Close();
                //分类服务端
                StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher/serverlist.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                string a01 = jsonObject["Spigot"].ToString();
                string a02 = jsonObject["CraftBukkit"].ToString();
                string a03 = jsonObject["Paper"].ToString();
                string a04 = jsonObject["MojangServer"].ToString();
                string a05 = jsonObject["ModServer"].ToString();
                reader.Close();
                //获取版本及下载地址
                //以下是spigot端
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    string a001 = a01;
                    while (a001.IndexOf("\",") != -1)
                    {
                        string strtempa = "\"";
                        int IndexofA = a001.IndexOf(strtempa);
                        string Ru = a001.Substring(IndexofA + 1);
                        serverlist.Items.Add(Ru.Substring(0, Ru.IndexOf("\":")));

                        int IndexofA3 = a001.IndexOf("\",");
                        string Ru3 = a001.Substring(IndexofA3 + 2);
                        a001 = Ru3;
                    }
                    string strtempa1 = "\"";
                    int IndexofA4 = a001.IndexOf(strtempa1);
                    string Ru4 = a001.Substring(IndexofA4 + 1);
                    serverlist.Items.Add(Ru4.Substring(0, Ru4.IndexOf("\":")));

                    string a0001 = a01;
                    while (a0001.IndexOf("\",") != -1)
                    {
                        string strtempa = "\":";
                        int IndexofA = a0001.IndexOf(strtempa);
                        string Ru = a0001.Substring(IndexofA + 4);
                        serverdownlist.Items.Add(Ru.Substring(0, Ru.IndexOf("\",")));

                        int IndexofA30 = a0001.IndexOf("\",");
                        string Ru30 = a0001.Substring(IndexofA30 + 2);
                        a0001 = Ru30;
                    }
                    string strtempa111 = "\":";
                    int IndexofA411 = a0001.IndexOf(strtempa111);
                    string Ru411 = a0001.Substring(IndexofA411 + 4);
                    serverdownlist.Items.Add(Ru411.Substring(0, Ru411.IndexOf("\"")));

                    //以下是CraftBukkit端
                    string a002 = a02;
                    while (a002.IndexOf("\",") != -1)
                    {
                        string strtempa = "\"";
                        int IndexofA = a002.IndexOf(strtempa);
                        string Ru = a002.Substring(IndexofA + 1);
                        serverlist2.Items.Add(Ru.Substring(0, Ru.IndexOf("\":")));

                        int IndexofA31 = a002.IndexOf("\",");
                        string Ru31 = a002.Substring(IndexofA31 + 2);
                        a002 = Ru31;
                    }
                    string strtempa1111 = "\"";
                    int IndexofA4111 = a002.IndexOf(strtempa1111);
                    string Ru4111 = a002.Substring(IndexofA4111 + 1);
                    serverlist2.Items.Add(Ru4111.Substring(0, Ru4111.IndexOf("\":")));

                    string a0002 = a02;
                    while (a0002.IndexOf("\",") != -1)
                    {
                        string strtempa = "\":";
                        int IndexofA = a0002.IndexOf(strtempa);
                        string Ru = a0002.Substring(IndexofA + 4);
                        serverdownlist2.Items.Add(Ru.Substring(0, Ru.IndexOf("\",")));

                        int IndexofA310 = a0002.IndexOf("\",");
                        string Ru310 = a0002.Substring(IndexofA310 + 2);
                        a0002 = Ru310;
                    }
                    string strtempa1011 = "\":";
                    int IndexofA4011 = a0002.IndexOf(strtempa1011);
                    string Ru4011 = a0002.Substring(IndexofA4011 + 4);
                    serverdownlist2.Items.Add(Ru4011.Substring(0, Ru4011.IndexOf("\"")));

                    //以下是Paper端
                    string a003 = a03;
                    while (a003.IndexOf("\",") != -1)
                    {
                        string strtempa = "\"";
                        int IndexofA = a003.IndexOf(strtempa);
                        string Ru = a003.Substring(IndexofA + 1);
                        serverlist3.Items.Add(Ru.Substring(0, Ru.IndexOf("\":")));

                        int IndexofA311 = a003.IndexOf("\",");
                        string Ru311 = a003.Substring(IndexofA311 + 2);
                        a003 = Ru311;
                    }
                    string strtempa11101 = "\"";
                    int IndexofA41101 = a003.IndexOf(strtempa11101);
                    string Ru41101 = a003.Substring(IndexofA41101 + 1);
                    serverlist3.Items.Add(Ru41101.Substring(0, Ru41101.IndexOf("\":")));

                    string a0003 = a03;
                    while (a0003.IndexOf("\",") != -1)
                    {
                        string strtempa = "\":";
                        int IndexofA = a0003.IndexOf(strtempa);
                        string Ru = a0003.Substring(IndexofA + 4);
                        serverdownlist3.Items.Add(Ru.Substring(0, Ru.IndexOf("\",")));

                        int IndexofA3110 = a0003.IndexOf("\",");
                        string Ru3110 = a0003.Substring(IndexofA3110 + 2);
                        a0003 = Ru3110;
                    }
                    string strtempa10011 = "\":";
                    int IndexofA40011 = a0003.IndexOf(strtempa10011);
                    string Ru40011 = a0003.Substring(IndexofA40011 + 4);
                    serverdownlist3.Items.Add(Ru40011.Substring(0, Ru40011.IndexOf("\"")));

                    //以下是MojangServer端
                    string a004 = a04;
                    while (a004.IndexOf("\",") != -1)
                    {
                        string strtempa = "\"";
                        int IndexofA = a004.IndexOf(strtempa);
                        string Ru = a004.Substring(IndexofA + 1);
                        serverlist4.Items.Add(Ru.Substring(0, Ru.IndexOf("\":")));

                        int IndexofA00054 = a004.IndexOf("\",");
                        string Ru00054 = a004.Substring(IndexofA00054 + 2);
                        a004 = Ru00054;
                    }
                    string strtempa000054 = "\"";
                    int IndexofA000054 = a004.IndexOf(strtempa000054);
                    string Ru000054 = a004.Substring(IndexofA000054 + 1);
                    serverlist4.Items.Add(Ru000054.Substring(0, Ru000054.IndexOf("\":")));

                    string a0004 = a04;
                    while (a0004.IndexOf("\",") != -1)
                    {
                        string strtempa = "\":";
                        int IndexofA = a0004.IndexOf(strtempa);
                        string Ru = a0004.Substring(IndexofA + 4);
                        serverdownlist4.Items.Add(Ru.Substring(0, Ru.IndexOf("\",")));

                        int IndexofA4110 = a0004.IndexOf("\",");
                        string Ru4110 = a0004.Substring(IndexofA4110 + 2);
                        a0004 = Ru4110;
                    }
                    string strtempa100011 = "\":";
                    int IndexofA400011 = a0004.IndexOf(strtempa100011);
                    string Ru400011 = a0004.Substring(IndexofA400011 + 4);
                    serverdownlist4.Items.Add(Ru400011.Substring(0, Ru400011.IndexOf("\"")));

                    //以下是ModServer端
                    string a005 = a05;
                    while (a005.IndexOf("\",") != -1)
                    {
                        string strtempa = "\"";
                        int IndexofA = a005.IndexOf(strtempa);
                        string Ru = a005.Substring(IndexofA + 1);
                        serverlist5.Items.Add(Ru.Substring(0, Ru.IndexOf("\":")));

                        int IndexofA511 = a005.IndexOf("\",");
                        string Ru511 = a005.Substring(IndexofA511 + 2);
                        a005 = Ru511;
                    }
                    string strtempa12001 = "\"";
                    int IndexofA42001 = a005.IndexOf(strtempa12001);
                    string Ru42001 = a005.Substring(IndexofA42001 + 1);
                    serverlist5.Items.Add(Ru42001.Substring(0, Ru42001.IndexOf("\":")));

                    string a0005 = a05;
                    while (a0005.IndexOf("\",") != -1)
                    {
                        string strtempa = "\":";
                        int IndexofA = a0005.IndexOf(strtempa);
                        string Ru = a0005.Substring(IndexofA + 4);
                        serverdownlist5.Items.Add(Ru.Substring(0, Ru.IndexOf("\",")));
                        int IndexofA5110 = a0005.IndexOf("\",");
                        string Ru5110 = a0005.Substring(IndexofA5110 + 2);
                        a0005 = Ru5110;
                    }
                    string strtempa12011 = "\":";
                    int IndexofA42011 = a0005.IndexOf(strtempa12011);
                    string Ru42011 = a0005.Substring(IndexofA42011 + 4);
                    serverdownlist5.Items.Add(Ru42011.Substring(0, Ru42011.IndexOf("\"")));
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher/serverlist.json");
                    getservermsg.Visibility = Visibility.Hidden;
                    lodingIco.Visibility = Visibility.Hidden;
                });
            }
            catch
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    lodingIco.Visibility = Visibility.Hidden;
                    getservermsg.Text = "获取服务端失败！请重试" + "w4x2";
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher/serverlist.json");
                });
                //timer7.Stop();
            }
        }

        private void updatehistory_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://msdoc.nstarmc.cn/docs/Download_source/history.html");
        }
    }
}
