using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
            //获取模版
            GetTemplate();

            //加载数据库名
            textDataBase.Text = connStr.Substring(connStr.LastIndexOf("=") + 1);
        }

        /// <summary>
        /// 获取模版
        /// </summary>
        private void GetTemplate()
        {
            string path = "template";
            DirectoryInfo root = new DirectoryInfo(path);
            DirectoryInfo[] directoryInfos = root.GetDirectories();

            List<String> listTemplate = new List<string>();
            foreach (var item in directoryInfos)
            {
                listTemplate.Add(item.Name);
            }

            comTemplate.ItemsSource = listTemplate;

        }

        /// <summary>
        /// 获取当前数据库的表格信息
        /// </summary>
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
                System.Windows.MessageBox.Show("连接出错！请检查您的用户名和密码是否正确");
            }
        }

        /// <summary>
        /// 选择路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbWnd = new FolderBrowserDialog();
            fbWnd.Description = "请选择代码生成目录";
            fbWnd.SelectedPath = "C:";
            fbWnd.ShowNewFolderButton = true;
            string strbtn = fbWnd.ShowDialog().ToString();            
            if (strbtn.CompareTo("OK") == 0)
            {
                textCodePath.Text = fbWnd.SelectedPath;
            }
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateCode_Click(object sender, RoutedEventArgs e)
        {
            #region 校验输入项目
            string template = (string)comTemplate.SelectedItem;
            if (String.IsNullOrEmpty(template))
            {
                System.Windows.MessageBox.Show("请选择模版");
                return;
            }

            string codePath = textCodePath.Text;
            if (String.IsNullOrEmpty(codePath))
            {
                System.Windows.MessageBox.Show("请先选择生成代码路径");
                return;
            }

            string project = textProject.Text;
            if (String.IsNullOrEmpty(project))
            {
                System.Windows.MessageBox.Show("请先输入项目名");
                return;
            }

            string table = comTables.Text;
            if (String.IsNullOrEmpty(table))
            {
                System.Windows.MessageBox.Show("请先输入项目名");
                return;
            }
            #endregion

            //读取模版生成到代码路径



        }
    }
}
