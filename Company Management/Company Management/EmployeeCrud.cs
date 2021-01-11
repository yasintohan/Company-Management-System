using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Management
{
    class EmployeeCrud
    {
        DBHelper db = new DBHelper();


        public MySqlDataReader getDepInfo(int id)
        {
            db.Open();
            string query = "select dep_id from employee WHERE emp_id ='" + id + "'";
            MySqlDataReader row;
            row = db.ExecuteReader(query);

            return row;
        }


        public DataTable getActiveTasks(int dep_id)
        {
            db.Open();
            DataTable dt = new DataTable();
            string sql = "SELECT `id`, `title`, `text`, `creation_date`, `duedate`, " +
                " ((SELECT username FROM `admin` WHERE `admin_id` = task.admin_id)) as admin," +
                " ((SELECT name FROM `department` WHERE `dep_id` = task.dep_id)) as department FROM `task` WHERE status = '0' and dep_id = '"+dep_id+"'";
            dt = db.ExecuteDataTable(sql);

            return dt;
        }

        public DataTable getTakenTasks(int emp_id)
        {
            db.Open();
            DataTable dt = new DataTable();
            string sql = "SELECT `id`, (SELECT title from task where id = take_task.task_id) as title, (SELECT text from task where id = take_task.task_id) as text, `taken_date`, (SELECT creation_date from task where id = take_task.task_id) as creation, (SELECT duedate from task where id = take_task.task_id) as duedate, `delivered_date` FROM `take_task` WHERE emp_id = " + emp_id + " and `delivered_date` > 0";
            dt = db.ExecuteDataTable(sql);

            return dt;
        }


        public void takeTask(int emp_id, int task_id)
        {

            db.Open();
            DateTime creation = DateTime.Now;
            string creationdate = creation.Date.Year + "/" + creation.Date.Month + "/" + creation.Date.Day;
            

            string query = "INSERT INTO `take_task`(`emp_id`, `task_id`, `taken_date`) VALUES('"+ emp_id + "','" + task_id + "','" + creationdate + "')";
            db.ExecuteNonQuery(query);

            string updquery = "UPDATE `task` SET `status`='1' WHERE `id` = '"+ task_id + "'";
            db.ExecuteNonQuery(updquery);



        }


        public void completeTask(int emp_id, int task_id)
        {

            db.Open();
            DateTime creation = DateTime.Now;
            string creationdate = creation.Date.Year + "/" + creation.Date.Month + "/" + creation.Date.Day;


            string updquery = "UPDATE `take_task` SET `delivered_date`='"+ creationdate + "' WHERE `task_id` = '" + task_id + "' and `emp_id` = '" + emp_id + "'";
            db.ExecuteNonQuery(updquery);



        }




    }
}
