using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageBox
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }


        private void Open_Image(object sender, RoutedEventArgs e)
        {
            // 创建一个打开文件的对话框
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置对话框的起始打开路径
            openFileDialog.InitialDirectory = @"D:\Users\Teemo Nicolas\Downloads";

            // 设置打开文件的类型，注意过滤器语法
            openFileDialog.Filter = "图片|*.jpg|全部|*";

            // 调用ShowDialog()方法显示改对话框，该方法的返回值代表用户是否点击了确定按钮
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
                catch (NotSupportedException exception)
                {
                    MessageBox.Show(exception.Message, "打开文件失败", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }

            }
            else
            {
                image.Source = new BitmapImage(new Uri(@"Resources/giraffe.jpg", UriKind.Relative));
            }

        }

        private void Generate_Value(object sender, RoutedEventArgs e)
        {
            int a = 1;

            value_x.Content = a;
            value_y.Content = 2;

            value_length.Content = "3";
            value_height.Content = "4";
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            image.Source = new BitmapImage(new Uri(@"Resources/giraffe.jpg", UriKind.Relative));
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            image.Source = null;
        }

    }
}
