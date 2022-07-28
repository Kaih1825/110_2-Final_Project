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
using Newtonsoft.Json;
using System.Net;

namespace ver2._1
{
    /// <summary>
    /// Page1.xaml 的互動邏輯
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        string APIKey = "25d0e865fec8eae50e57165edb127700";
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string city;
            city =ip_city.Text;
            getweather(city);
            getip();
        }

        void getweather(string city)
        {
            using (WebClient web = new WebClient())
            {
                string url = $@"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={APIKey}";
                
                try
                {
                    var json = web.DownloadString(url);
                    WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
                    SearchCity.Content = Info.name;
                    des.Content = Info.weather[0].description.ToString();
                    tmp.Content = Math.Round((Info.main.temp-273.15),2)+"℃";
                    fel_tmp.Content = Math.Round((Info.main.feels_like - 273.15), 2)+ "℃";
                    h_tmp.Content=Math.Round((Info.main.temp_max - 273.15), 2) + "℃";
                    lo_tmp.Content=Math.Round((Info.main.temp_min-273.15),2) + "℃";
                    dry.Content=Info.main.humidity.ToString()+"%";
                    var image=new Image();
                    var urlimg = $@"https://openweathermap.org/img/w/{Info.weather[0].icon}.png";
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(urlimg, UriKind.Absolute);
                    bitmap.EndInit();
                    img.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+"\n\n請輸入正確的城市名稱(英文)");
                }
            }
        }
        bool status ;
        private string getip()
        {
            using(WebClient web = new WebClient())
            {

                string url = @"http://ip-api.com/json/";
                var json = web.DownloadString(url);
                GetIp.root Info= JsonConvert.DeserializeObject<GetIp.root>(json);
                return Info.city.ToString();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchCity.Content = getip();
                getweather(getip());
                status = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            ip_city.Foreground = Brushes.Gray;
            ip_city.Text = getip();
        }

        private void ip_city_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (status == false)
            {
                ip_city.Foreground = Brushes.Black;
                ip_city.Text = "";
                status= true;
            }
            
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
