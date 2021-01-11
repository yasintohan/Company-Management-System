using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Management
{
    class Department
    {
        DBHelper db = new DBHelper();

        public DataTable getDepartments()
        {
            db.Open();
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM `department`";
            dt = db.ExecuteDataTable(sql);
            
            return dt;
        }

        public void addDepartment(string namee, string descriptionn)
        {
            db.Open();

                   
            string query = "insert into department(`dep_id`, `name`, `description`) values(NULL, '"+ namee + "', '"+ descriptionn + "')";
            db.ExecuteNonQuery(query);

        }

        public void updDepartment(int id, string namee, string descriptionn)
        {
            db.Open();


            string query = "UPDATE `department` SET `name`='"+namee+"',`description`='"+ descriptionn + "' WHERE `dep_id` = " + id;
            db.ExecuteNonQuery(query);

        }


        public void delDepartment(int id)
        {
            db.Open();


            string query = "DELETE FROM `department` WHERE `dep_id` = " + id;
            db.ExecuteNonQuery(query);

        }






    }
}
