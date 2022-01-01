using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 我的世界开服器Metro
{
    /// <summary>
    /// Window3.xaml 的交互逻辑
    /// </summary>
    public partial class Window3 : Window
    {
        string pageHtml;
        public Window3()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                byte[] pageData = MyWebClient.DownloadData("http://115.220.5.81:8081/web/frpcserver.txt");
                pageHtml = Encoding.UTF8.GetString(pageData);
            }
            catch
            {
                MessageBox.Show("连接服务器失败！\n错误代码：" + "w3x1", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                pageHtml = "";
                //Close();
            }
            while (pageHtml.IndexOf("#") != -1)
                {
                    string strtempa = "#";
                    int IndexofA = pageHtml.IndexOf(strtempa);
                    string Ru = pageHtml.Substring(IndexofA + 1);
                    string a100 = Ru.Substring(0, Ru.IndexOf("\n"));
                    listBox1.Items.Add(a100);

                    int IndexofA3 = pageHtml.IndexOf("#");
                    string Ru3 = pageHtml.Substring(IndexofA3 + 1);
                    pageHtml = Ru3;

                    string strtempa1 = "server_addr = ";
                    int IndexofA1 = pageHtml.IndexOf(strtempa1);
                    string Ru1 = pageHtml.Substring(IndexofA1 + 14);
                    string a101 = Ru1.Substring(0, Ru1.IndexOf("\n"));
                    listBox2.Items.Add(a101);

                    string strtempa2 = "server_port = ";
                    int IndexofA2 = pageHtml.IndexOf(strtempa2);
                    string Ru2 = pageHtml.Substring(IndexofA2 + 14);
                    string a102 = Ru2.Substring(0, Ru2.IndexOf("\n"));
                    listBox3.Items.Add(a102);
                }
                try
                {
                    WebClient MyWebClient1 = new WebClient();
                    MyWebClient1.Credentials = CredentialCache.DefaultCredentials;
                    Byte[] pageData1 = MyWebClient1.DownloadData("http://115.220.5.81:8081/web/frpcgg.txt");
                    gonggao.Content = Encoding.UTF8.GetString(pageData1);
                }
                catch
                {
                    gonggao.Content = "none";
                }
            
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox3.Text == "")
            {
                try
                {
                    if (textBox1.Text == "" || textBox2.Text == "")
                    {
                        MessageBox.Show("出现错误，请确保内网端口和QQ号不为空后再试：" + "w3x2", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    int a = listBox1.SelectedIndex;
                    Random ran = new Random();
                    int n = ran.Next(20000, 65535);
                    string frpc = "[common]\nserver_port = " + listBox3.Items[a].ToString() + "\nserver_addr = " + listBox2.Items[a].ToString() + "\n" + "token=000\n" + "\n[" + textBox2.Text + "]\ntype = tcp\nlocal_ip = 127.0.0.1\nlocal_port = " + textBox1.Text + "\nremote_port = " + n;
                    FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc", FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(frpc);
                    sw.Flush();
                    sw.Dispose();
                    sw.Close();
                    fs.Close();
                    string frpc1 = listBox2.Items[a].ToString() + ":" + n;
                    MainWindow.frpc = frpc1.Replace("\r", "");
                    StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json");
                    JsonTextReader jsonTextReader = new JsonTextReader(reader);
                    JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                    jsonObject["frpc"] = MainWindow.frpc;
                    reader.Close();
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", output);
                    MessageBox.Show("映射配置成功，请您点击“启动内网映射”以启动映射！\n连接IP为：\n" + MainWindow.frpc, "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("出现错误，请确保选择节点后再试：" + "w3x2", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    if (textBox1.Text == "" || textBox2.Text == "")
                    {
                        MessageBox.Show("出现错误，请确保内网端口和QQ号不为空后再试：" + "w3x2", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    int a = listBox1.SelectedIndex;
                    Random ran = new Random();
                    int n = ran.Next(20000, 65535);
                    string frpc = "[common]\nserver_port = " + listBox3.Items[a].ToString() + "\nserver_addr = " + listBox2.Items[a].ToString() + "\n" + "user = "+textBox2.Text+"\n" + "meta_token = "+textBox3.Text+"\n" + "\n[" + textBox2.Text + "]\ntype = tcp\nlocal_ip = 127.0.0.1\nlocal_port = " + textBox1.Text + "\nremote_port = " + n;
                    FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\frpc", FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(frpc);
                    sw.Flush();
                    sw.Dispose();
                    sw.Close();
                    fs.Close();
                    string frpc1 = listBox2.Items[a].ToString() + ":" + n;
                    MainWindow.frpc = frpc1.Replace("\r", "");
                    StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json");
                    JsonTextReader jsonTextReader = new JsonTextReader(reader);
                    JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                    jsonObject["frpc"] = MainWindow.frpc;
                    reader.Close();
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"ServerLauncher\config.json", output);
                    MessageBox.Show("映射配置成功，请您点击“启动内网映射”以启动映射！\n连接IP为：\n" + MainWindow.frpc, "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("出现错误，请确保选择节点后再试：" + "w3x2", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
