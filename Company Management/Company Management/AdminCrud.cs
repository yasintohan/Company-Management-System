using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Management
{
    class AdminCrud
    {

        DBHelper db = new DBHelper();

        public DataTable getAdmins()
        {
            db.Open();
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM `admin`";
            dt = db.ExecuteDataTable(sql);

            return dt;
        }

        public void addAdmin(string username, string pass)
        {
            db.Open();


            string query = "insert into admin(`admin_id`, `username`, `password`) values(NULL, '" + username + "', '" + pass + "')";
            db.ExecuteNonQuery(query);

        }

        public void updAdmin(int id, string username, string pass)
        {
            db.Open();


            string query = "UPDATE `admin` SET `username`='" + username + "',`password`='" + pass + "' WHERE `admin_id` = " + id;
            db.ExecuteNonQuery(query);

        }


        public void delAdmin(int id)
        {
            db.Open();


            string query = "DELETE FROM `admin` WHERE `admin_id` = " + id;
            db.ExecuteNonQuery(query);

        }




    }
}
