using MySql.Data.MySqlClient;
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

namespace EasyuiGenerate
{
    /// <summary>
    /// GenerateCodeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GenerateCodeWindow : Window
    {
        private string connStr = "";
        public GenerateCodeWindow(string connStr)
        {
            this.connStr = connStr;

            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            //加载本数据库的表格
            GetTables();
        }

        private void GetTables()
        {
            try
            {
                List<String> listTables = new List<string>();
                using (MySqlDataReader reader = SqlHelper.ExecuteDataReader("show tables", System.Data.CommandType.Text, connStr))
                {
                    while (reader.Read())
                    {
                        listTables.Add(reader.GetString(0));
                    }
                }

                comTables.ItemsSource = listTables;

            }
            catch (Exception)
            {
                MessageBox.Show("连接出错！请检查您的用户名和密码是否正确");
            }
        }
    }
}
