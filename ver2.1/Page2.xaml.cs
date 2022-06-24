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

namespace ver2._1
{
    /// <summary>
    /// Page2.xaml 的互動邏輯
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }
        bool status ;
        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetIpInfo("My ip");
                status = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetIpInfo(string SearchedIp)
        {
            using (WebClient web = new WebClient())
            {
                string url;
                if(SearchedIp=="My ip")
                {
                    url = @"http://ip-api.com/json/";
                }
                else
                {
                    SearchIp.Foreground=Brushes.Black;
                    url = $@"http://ip-api.com/json/{SearchedIp}";
                }
                var json = web.DownloadString(url);
                GetIp.root Info = JsonConvert.DeserializeObject<GetIp.root>(json);
                var image = new Image();
                var urlimg = $@"https://countryflagsapi.com/png/{Info.countryCode}";
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(urlimg, UriKind.Absolute);
                bitmap.EndInit();
                IpLabel.Content = Info.query;
                ContryLabel.Content=Info.country;
                StatusLabel.Content=Info.status;
                CityLabel.Content=Info.city;
                LonLabel.Content=Info.lon+"";
                LatLabel.Content=Info.lat+"";
                if(SearchedIp=="My ip")
                {
                    SearchIp.Foreground=Brushes.Gray;
                    SearchIp.Text=Info.query;
                }
            }
        }

        private void GetIp()
        {
            using (WebClient web = new WebClient())
            {
                string url = @"http://ip-api.com/json/";
                var json = web.DownloadString(url);
                GetIp.root Info = JsonConvert.DeserializeObject<GetIp.root>(json);
                var image = new Image();
                var urlimg = $@"https://countryflagsapi.com/png/{Info.countryCode}";
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(urlimg, UriKind.Absolute);
                bitmap.EndInit();
                IpLabel.Content = Info.query;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string ip=SearchIp.Text;
            GetIpInfo(ip);
        }

        private void SearchIp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (status == false)
            {
                SearchIp.Foreground = Brushes.Black;
                SearchIp.Text = "";
                status= true;
            }
        }
    }
}
