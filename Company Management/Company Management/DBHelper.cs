using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Company_Management
{


    class DBHelper
    {

        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;


        static string host = "localhost";
        static string database = "company_todo";
        static string userDB = "root";
        static string password = "";
        
     
        public MySqlConnection getConnection()
        {
           return conn;
        }
        
        public bool Open()
        {
            try
            {
                MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();

                csb.Server = host;
                csb.UserID = userDB;
                csb.Password = password;
                csb.Database = database;
                
                
                conn = new MySqlConnection(csb.ToString());
                
                conn.Open();
                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show("Connection Error ! " + er.Message, "Information");
            }
            return false;
        }
        public void Close()
        {
            conn.Close();
            conn.Dispose();
        }
        public DataSet ExecuteDataSet(string sql)
        {
            
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                da.Fill(ds, "result");
                return ds;
            
        }

        public DataTable ExecuteDataTable(string sql)
        {
              
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;
            
        }
        public MySqlDataReader ExecuteReader(string sql)
        {
            
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                return reader;
            
        }
        public int ExecuteNonQuery(string sql)
        {
            
                int affected;
                MySqlTransaction mytransaction = conn.BeginTransaction();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                affected = cmd.ExecuteNonQuery();
                mytransaction.Commit();
                return affected;
            
        }
    }



}
