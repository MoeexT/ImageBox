using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
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

using OpenCvSharp;

namespace ImageBox
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
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
                LoadImage(openFileDialog.FileName);
            }
            else
            {
                //File_Name.Text = "giraffe.jpg";
                //CurrentImage.Source = new BitmapImage(new Uri("Resources\\giraffe.jpg", UriKind.Relative));
                LoadDefaultImage();
            }
        }

        private void LoadImage(string fileName)
        {
            try
            {
                this.bitmapImage = new BitmapImage(new Uri(fileName, UriKind.RelativeOrAbsolute));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "不支持的文件类型", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            string[] path = fileName.Split('\\');
            File_Name.Text = path[path.Length-1];
            CurrentImage.Source = this.bitmapImage;
            Image_Width.Content = this.bitmapImage.Width;
            Image_Height.Content = this.bitmapImage.Height;
            // ShowDefaultImage.IsChecked = false;
        }

        private void LoadDefaultImage()
        {
            File_Name.Text = "giraffe.jpg";
            BitmapImage bitmap = new BitmapImage(new Uri("Resources\\giraffe.jpg", UriKind.Relative));
            CurrentImage.Source = bitmap;
            Image_Width.Content = bitmap.Width;
            Image_Height.Content = bitmap.Height;
        }

        private void ClearImage()
        {
            CurrentImage.Source = null;
            File_Name.Text = null;
            Image_Width.Content = 0;
            Image_Height.Content = 0;
            ShowDefaultImage.IsChecked = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) { LoadDefaultImage(); }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e) { ClearImage(); }

        private void Clear_Image(object sender, RoutedEventArgs e) { ClearImage(); }

        private void OnMouseMove (object sender, MouseEventArgs e)
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
            // TODO
            Value_X.Content = (int)(e.GetPosition(CurrentImage).X * width / CurrentImage.Width);
            Value_Y.Content = (int)(e.GetPosition(CurrentImage).Y * height / CurrentImage.Height);
        }

        private void OnDragEnter (object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //MessageBox.Show();
                LoadImage(((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString());
            }
        }

        private void Debug_Button_Click (object sender, RoutedEventArgs e)
        {
            Debug_Info.Content = ((int)CurrentImage.ActualWidth).ToString() + ", " + ((int)CurrentImage.ActualHeight).ToString();
        }

        private void Flip_Image(object sender, RoutedEventArgs e)
        {
            // Cv2.Transpose(InputArray【Mat】 src, OutputArray dst);
            // Cv2.Flip(InputArray src, OutputArray dst, FlipMode flipCode);
            
        }

        /*public static Bitmap rotateImage(Bitmap b, float angle)
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //move rotation point to center of image
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            //rotate
            g.RotateTransform(angle);
            //move image back
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            //draw passed in image onto graphics object
            g.DrawImage(b, new System.Windows.Point(0, 0));
            return returnBitmap;
        }*/

        // TODO 已经解决了拖拽问题；1. 没有显示图片也可以拖拽（Image的尺寸）；2. 拖拽选框、计算长宽及原点

    }
}
