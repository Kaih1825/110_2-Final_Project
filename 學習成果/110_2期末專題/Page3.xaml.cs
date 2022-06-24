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
using System.Data;
using System.Data.SqlClient;

namespace ver2._1
{
    /// <summary>
    /// Page3.xaml 的互動邏輯
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }
        string date;
        String cnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DB.mdf;Integrated Security=True;Connect Timeout=30";
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            txb.Document.Blocks.Clear();
            Cal.SelectedDate = DateTime.Today;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dateTime = Cal.SelectedDate.Value;
                date = dateTime.ToString("yyyy/MM/dd");
                string ivent = new TextRange(txb.Document.ContentStart, txb.Document.ContentEnd).Text;
                if (ivent.Length == 2 || ivent.Length == 0)
                {
                    try
                    {
                        SqlConnection cn = new SqlConnection(cnStr);
                        cn.Open();
                        SqlCommand cmd = new SqlCommand();  //建立SqlCommand物件cmd
                        cmd.CommandText = $"DELETE FROM 活動 WHERE 時間=N'{date}'";
                        cmd.Connection = cn;
                        cmd.ExecuteNonQuery();         //cmd物件執行SQL的新增命令
                        txb.Document.Blocks.Clear();
                        MessageBox.Show("已刪除本日活動");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    SaveEvent(date, ivent);
                }
                //MessageBox.Show(ivent.Length+"");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Cal_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dateTime=Cal.SelectedDate.Value;
            date=dateTime.ToString("yyyy/MM/dd");
            string textbox=SearchEvent(date);
            if(textbox=="Error")
            {
                txb.Document.Blocks.Clear();
            }
            else
            {
                txb.Document.Blocks.Clear();
                txb.Document.Blocks.Add(new Paragraph(new Run(textbox)));
            }
        }
////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SaveEvent(string date,string ivent)
        {
            try
            {
                SqlConnection cn = new SqlConnection(cnStr);
                cn.Open();
                SqlCommand cmd = new SqlCommand();  //建立SqlCommand物件cmd
                try
                {
                    cmd.CommandText = $"INSERT INTO 活動(時間,事件)VALUES(N'{date}',N'{ivent}')";
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();         //cmd物件執行SQL的新增命令
                }
                catch
                {
                    cmd.CommandText = $"UPDATE 活動 SET 事件=N'{ivent}' WHERE 時間=N'{date}'";
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();         //cmd物件執行SQL的新增命令
                }
                MessageBox.Show("新增/更新成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private string SearchEvent(string date)
        {
            SqlConnection cn = new SqlConnection(cnStr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT 時間,事件 FROM 活動", cn);
            //建立DataSet物件ds
            DataSet ds = new DataSet();
            //使用da物件的Fill方法取得查詢的客戶記錄並填入ds物件內的DataTable
            da.Fill(ds);
            //將ds內的第一個DataTable物件指定給DataTable物件dt
            DataTable dt = ds.Tables[0];
            string textbox="";
            int num=-1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i]["時間"]+""==date)
                {
                    num=i;
                }
                    
            }
            if(num==-1)
            {
                return "Error";
            }
            else
            {
                textbox+=dt.Rows[num]["事件"];
                return textbox;
            }
            //txb.Document.Blocks.Clear();
            //txb.Document.Blocks.Add(new Paragraph(new Run(textbox)));
        }
    }
}
