using EasyuiGenerate.utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyuiGenerate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public string connStr = "server=127.0.0.1;userid=root;password=123456";

        public MainWindow()
        {
            InitializeComponent();
        }

        //连接数据库服务器显示，数据库列表
        private void btnTestConnect_Click(object sender, RoutedEventArgs e)
        {

            string server = textDatabaseIp.Text;
            string uid = textUserName.Text;
            string password = textPassword.Text;

            if (String.IsNullOrEmpty(server))
            {
                System.Windows.MessageBox.Show("请输入服务器IP");
                return;
            }
            if (String.IsNullOrEmpty(uid))
            {
                System.Windows.MessageBox.Show("请输入用户名");
                return;
            }
            if (String.IsNullOrEmpty(password))
            {
                System.Windows.MessageBox.Show("请输入密码");
                return;
            }

            connStr = "server=" + server + ";userid=" + uid + ";password=" + password;

            try
            {
                List<String> listDataBase = new List<string>();
                using (MySqlDataReader reader = SqlHelper.ExecuteDataReader("show DATABASES", System.Data.CommandType.Text, connStr))
                {
                    while (reader.Read())
                    {
                        listDataBase.Add(reader.GetString(0));
                    }
                }
                comDatabase.ItemsSource = listDataBase;
                comDatabase.SelectedIndex = 0;

                //保存连接信息到app.config
                ConfigurationHelper.UpdateAppConfig("ServerIP", server);
                ConfigurationHelper.UpdateAppConfig("user", uid);
                ConfigurationHelper.UpdateAppConfig("password", password);

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("连接出错！请检查您的用户名和密码是否正确");
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            string selectDataBase = (String)comDatabase.SelectedItem;

            if (String.IsNullOrEmpty(selectDataBase))
            {

                System.Windows.MessageBox.Show("请先连接选择数据库");
                return;
            }

            connStr += ";database=" + selectDataBase;

            this.Visibility = Visibility.Hidden;
            GenerateCodeWindow gcWindwow = new GenerateCodeWindow(connStr);
            gcWindwow.ShowDialog();

            this.Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            //加载字符串
            textDatabaseIp.Text = ConfigurationHelper.GetValueConfig("ServerIP");
            textUserName.Text = ConfigurationHelper.GetValueConfig("user");
            textPassword.Text = ConfigurationHelper.GetValueConfig("password");
        }
    }
}
