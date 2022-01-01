using System;
using System.Collections.Generic;
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

namespace 我的世界开服器Metro
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.aa == true)
            {
                TitileText.Text = "更新";
                CancelBtn.Visibility = Visibility.Visible;
            }
            else
            {
                CancelBtn.Visibility = Visibility.Hidden;
            }
            ContentText.Text = MainWindow.aa2;
            this.Height = ContentText.ActualHeight;
        }

        private void SureBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.aa = false;
            Close();
        }
    }
}
