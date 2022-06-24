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
using System.Windows.Threading;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace ver2._1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            //this.WindowState = (this.WindowState != WindowState.Maximized) ? WindowState.Maximized : WindowState.Normal;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            /*FileStream fs = File.OpenWrite("NotePad.txt");
            fs.SetLength(0);
            fs.Close();*/
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Weather_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page1();
            img.Visibility = Visibility.Hidden;
        }

        private string getip()
        {
            using (WebClient web = new WebClient())
            {
                string url = @"http://ip-api.com/json/";
                var json = web.DownloadString(url);
                GetIp.root Ipfo = JsonConvert.DeserializeObject<GetIp.root>(json);
                return Ipfo.query.ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            time.Content = DateTime.Now.ToString("hh:mm:ss");
            date.Content = DateTime.Now.ToString("yyy/MM/dd dddd");
            //宣告Timer
            DispatcherTimer _timer = new DispatcherTimer();
            //設定呼叫間隔時間為1000ms
            _timer.Interval = TimeSpan.FromMilliseconds(1000);

            //加入callback function
            _timer.Tick += _timer_Tick;

            //開始
            _timer.Start();
            img.Visibility = Visibility.Visible;
        }
            void _timer_Tick(object sender, EventArgs e)
        {
            time.Content = DateTime.Now.ToString("hh:mm:ss");
            date.Content = DateTime.Now.ToString("yyy/MM/dd dddd");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page2();
            img.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page3();
            img.Visibility = Visibility.Hidden;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page4();
            img.Visibility = Visibility.Hidden;
        }

        private void btnMinimize_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            btnMinimize.Background = Brushes.Red;
            img.Visibility = Visibility.Hidden;
        }

    }
}
