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

    class taskCrud
    {

        DBHelper db = new DBHelper();

        public DataTable getActiveTasks()
        {
            db.Open();
            DataTable dt = new DataTable();
            string sql = "SELECT `id`, `title`, `text`, `creation_date`, `duedate`, " +
                " ((SELECT username FROM `admin` WHERE `admin_id` = task.admin_id)) as admin," +
                " ((SELECT name FROM `department` WHERE `dep_id` = task.dep_id)) as department FROM `task` WHERE status = '0'";
            dt = db.ExecuteDataTable(sql);

            return dt;
        }

        public DataTable getTakenTasks()
        {
            db.Open();
            DataTable dt = new DataTable();
            string sql = "SELECT `id`, `title`, `text`, `creation_date`, `duedate`, " +
                " ((SELECT username FROM `admin` WHERE `admin_id` = task.admin_id)) as admin," +
                " ((SELECT name FROM `department` WHERE `dep_id` = task.dep_id)) as department,(SELECT username FROM employee WHERE emp_id = (SELECT emp_id FROM take_task WHERE `task_id` = task.id)) as Employee, (SELECT taken_date FROM take_task WHERE `task_id` = task.id) as TakenDate,(SELECT delivered_date FROM take_task WHERE `task_id` = task.id) as DeliveredDate FROM `task` WHERE status = '1'";
            dt = db.ExecuteDataTable(sql);

            return dt;
        }


        public void addTask(string title, string text, DateTime date, string depname, int admin_id)
        {
            db.Open();
            DateTime creation = DateTime.Now;
            string creationdate = creation.Date.Year + "/" + creation.Date.Month + "/" + creation.Date.Day;
            string duedate = date.Date.Year + "/" + date.Date.Month + "/" + date.Date.Day;

            string query = "INSERT INTO `task`(`title`, `text`, `creation_date`, `duedate`," +
                " `status`, `admin_id`, `dep_id`) VALUES ('" + title + "','" + text + "','" + creationdate + "'," +
                "'" + duedate + "','0','"+ admin_id + "',(SELECT dep_id FROM `department` WHERE `name` = '" + depname + "'))";
            db.ExecuteNonQuery(query);

        }




        public void updTask(int id, string title, string text, DateTime date, string depname)
        {
            db.Open();

            string duedate = date.Date.Year + "/" + date.Date.Month + "/" + date.Date.Day;

            string query = "UPDATE `task` SET `title`='" + title +
                "',`text`='" + text + "',`duedate`='" + duedate + "',`dep_id`=(SELECT dep_id FROM `department` WHERE `name` = '" + depname + "') WHERE `id`= '" + id + "'";
            db.ExecuteNonQuery(query);

        }



        public void delTask(int id)
        {
            db.Open();


            string query = "DELETE FROM `task` WHERE `id` = " + id;
            db.ExecuteNonQuery(query);

        }



    }
}
