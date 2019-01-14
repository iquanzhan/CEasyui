using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyuiGenerate
{
    public static class SqlHelper
    {

        public static int ExecuteNonQuery(string sql, CommandType type, string connStr, params MySqlParameter[] pms)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string sql, CommandType type, string connStr, params MySqlParameter[] pms)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        public static DataTable ExecuteDataTable(string sql, CommandType type, string connStr, params MySqlParameter[] pms)
        {
            DataTable table = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn))
                {
                    adapter.SelectCommand.CommandType = type;
                    if (pms != null)
                    {
                        adapter.SelectCommand.Parameters.AddRange(pms);
                    }
                    conn.Open();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        public static MySqlDataReader ExecuteDataReader(string sql, CommandType type, string connStr, params MySqlParameter[] pms)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.CommandType = type;
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }
                try
                {
                    conn.Open();

                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception)
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }

            }
        }
    }
}
