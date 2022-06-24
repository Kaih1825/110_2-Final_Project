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
using System.IO;
using System.Windows.Threading;

namespace ver2._1
{
    /// <summary>
    /// Page4.xaml 的互動邏輯
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
        }
        private void RichTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            txb.Focus();
            StreamReader rd = new StreamReader("NotePad.txt");
            string word="",line;
            while((line=rd.ReadLine())!=null){
                word+=line+"\n";
            }
            TextRange textRange = new TextRange(txb.Document.ContentStart, txb.Document.ContentEnd);
            textRange.Text=word;
            rd.Close();
            //宣告Timer
            DispatcherTimer _timer = new DispatcherTimer();

            //設定呼叫間隔時間為600ms
            _timer.Interval = TimeSpan.FromMilliseconds(300);

            //加入callback function
            _timer.Tick += _timer_Tick;
            //開始
            //_timer.Start();
        }

        private void txb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        
        void _timer_Tick(object sender, EventArgs e)
        {
            TextRange t = new TextRange(txb.Document.ContentStart, txb.Document.ContentEnd);
            FileStream file = new FileStream("NotePad.txt", FileMode.Create);
            t.Save(file, System.Windows.DataFormats.Text);
            file.Close();
        }

        private void txb_KeyUp(object sender, KeyEventArgs e)
        {
            TextRange t = new TextRange(txb.Document.ContentStart, txb.Document.ContentEnd);
            FileStream file = new FileStream("NotePad.txt", FileMode.Create);
            t.Save(file, System.Windows.DataFormats.Text);
            file.Close();
        }
    }
}
