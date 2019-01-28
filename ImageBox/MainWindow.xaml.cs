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
        // private String fileName;
        private BitmapImage bitmapImage;

        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Open_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\Users\Teemo Nicolas\Downloads",  // 设置对话框的起始打开路径
                Filter = "图片|*.jpg|全部|*"  // 设置打开文件的类型，注意过滤器语法
            };  // 创建一个打开文件的对话框
            if (openFileDialog.ShowDialog() == true)  // 调用ShowDialog()方法显示改对话框，该方法的返回值代表用户是否点击了确定按钮
            {
                try
                {
                    this.LoadImage(openFileDialog.FileName);
                }
                catch (NotSupportedException exception)
                {
                    MessageBox.Show(exception.Message, "打开文件失败", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
            else
            {
                //File_Name.Text = "giraffe.jpg";
                //CurrentImage.Source = new BitmapImage(new Uri("Resources\\giraffe.jpg", UriKind.Relative));
                LoadImage("Resources\\giraffe.jpg");
            }

        }

        private void LoadImage(string fileName)
        {
            this.bitmapImage = new BitmapImage(new Uri(fileName, UriKind.RelativeOrAbsolute));
            string[] path = fileName.Split('\\');
            File_Name.Text = path[path.Length-1];
            CurrentImage.Source = this.bitmapImage;
            Image_Width.Content = this.bitmapImage.Width;
            Image_Height.Content = this.bitmapImage.Height;
        }

        private void ClearImage()
        {
            CurrentImage.Source = null;
            File_Name.Text = null;
            Image_Width.Content = 0;
            Image_Height.Content = 0;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) { LoadImage("Resources\\giraffe.jpg"); }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e) { ClearImage(); }

        private void Clear_Image(object sender, RoutedEventArgs e) { ClearImage(); }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            double width;
            double height;
            try
            {
                width = this.bitmapImage.Width;
                height = this.bitmapImage.Height;
            }
            catch
            {
                width = CurrentImage.Width;
                height = CurrentImage.Height;
            }

            Value_X.Content = (int)(e.GetPosition(CurrentImage).X * width / CurrentImage.Width);
            Value_Y.Content = (int)(e.GetPosition(CurrentImage).Y * height / CurrentImage.Height);
        }

        
    }
}
