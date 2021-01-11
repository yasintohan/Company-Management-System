using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Company_Management
{
    class LoginCrud
    {

        DBHelper db = new DBHelper();




        public MySqlDataReader getLogin(String table, String username, String password)
        {
            db.Open();
            string query = "select * from "+ table + " WHERE username ='" + username + "' AND password ='" + password + "'";
            MySqlDataReader row;
            row = db.ExecuteReader(query);
            
            return row;
        }


        public DataTable getUsers()
        {
            db.Open();
            DataTable dt = new DataTable();
            string sql = "SELECT `emp_id`, `username`, `password`, `name`, `surname`, `email`, `birthdate`, ((SELECT name FROM `department` WHERE `dep_id` = employee.dep_id)) as department FROM `employee`";
            dt = db.ExecuteDataTable(sql);

            return dt;
        }


        public void addUser(string namee, string surname, string username, string email, DateTime birth, string password, string depname)
        {
            db.Open();
            
            string datebirth = birth.Date.Year + "/" + birth.Date.Month + "/" + birth.Date.Day;

            string query = "INSERT INTO `employee`(`username`, `password`, `name`, `surname`," +
                " `email`, `birthdate`, `dep_id`) VALUES ('"+ username + "','" + password + "','" + namee + "'," +
                "'" + surname + "','" + email + "','" + datebirth + "',(SELECT dep_id FROM `department` WHERE `name` = '" + depname + "'))";
            db.ExecuteNonQuery(query);

        }


        public void updUser(int id, string namee, string surname, string username, string email, DateTime birth, string password, string depname)
        {
            db.Open();

            string datebirth = birth.Date.Year + "/" + birth.Date.Month + "/" + birth.Date.Day;

            string query = "UPDATE `employee` SET `username`='"+ username +
                "',`password`='"+ password + "',`name`='" + namee + "',`surname`='" + surname + "'," +
                "`email`='" + email + "',`birthdate`='" + birth + "',`dep_id`='(SELECT dep_id FROM `department` WHERE `name` = '" + depname + "')' WHERE `emp_id`= '" + id+"'";
            db.ExecuteNonQuery(query);

        }


        public void delUser(int id)
        {
            db.Open();


            string query = "DELETE FROM `employee` WHERE `emp_id` = " + id;
            db.ExecuteNonQuery(query);

        }




    }
}
