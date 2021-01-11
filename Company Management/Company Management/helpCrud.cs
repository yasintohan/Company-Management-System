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
    class helpCrud
    {


        DBHelper db = new DBHelper();

        public DataTable getHelps()
        {
            db.Open();
            DataTable dt = new DataTable();
            string sql = "SELECT `help_id`, `title`, `text`," +
                " ((SELECT name FROM `employee` WHERE `emp_id` = help_msg.emp_id)) as name," +
                 " ((SELECT surname FROM `employee` WHERE `emp_id` = help_msg.emp_id)) as surname," +
                " ((SELECT title FROM `task` WHERE `id` = help_msg.task_id)) as task FROM `help_msg`";
            dt = db.ExecuteDataTable(sql);

            return dt;
        }

        public void addMSG(string title, string text, int emp_id, int task_id)
        {
            db.Open();
            

            string query = "INSERT INTO `help_msg`(`emp_id`, `title`, `text`, `task_id`) VALUES ('"+ emp_id + "','" + title + "','" + text + "','" + task_id + "')";
            db.ExecuteNonQuery(query);

        }

        public void delMSG(int id)
        {
            db.Open();


            string query = "DELETE FROM `help_msg` WHERE `help_id` = " + id;
            db.ExecuteNonQuery(query);

        }
    }
}
