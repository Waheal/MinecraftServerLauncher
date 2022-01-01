using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Windows.Threading;

namespace 我的世界开服器Metro
{
    /// <summary>
    /// Window7.xaml 的交互逻辑
    /// </summary>
    public partial class Window7 : Window
    {
        public static bool nosafeClose = false;
        public Window7()
        {
            InitializeComponent();
        }
            private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                item001.Text = File.ReadAllText(MainWindow.serverbase + @"\server.properties");
                nosafeClose = false;
            }
            catch
            {
                MessageBox.Show("出现错误，请打开一次服务器后再次尝试！"+"w7x1", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                nosafeClose = true;
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (nosafeClose == false)
            {
                File.WriteAllText(MainWindow.serverbase + @"\server.properties", item001.Text);
                MessageBox.Show("配置已成功保存，请重启服务器以使设置生效！");
            }
        }
    }
}
