using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company_Management
{
    public partial class EmployeePanel : Form
    {

        public int employeeId { get; set; }
        public string empUsername { get; set; }

        public int departmentId { get; set; }

        EmployeeCrud crud = new EmployeeCrud();
        helpCrud helpcrud = new helpCrud();
        public EmployeePanel()
        {
            InitializeComponent();
        }

        private void EmployeePanel_Load(object sender, EventArgs e)
        {

            MySqlDataReader row = crud.getDepInfo(employeeId);
            if (row.HasRows)
            {

                while (row.Read())
                {
                    departmentId = row.GetInt32(0);
                }
                

            }
            else
            {
                MessageBox.Show("Failed");
            }
            refreshTasksList();
            welcomeLabel.Text = "Employee: " + empUsername;


        }

      

       

    private void refreshTasksList()
        {
            DataTable dt = crud.getActiveTasks(departmentId);
            activeTaskList.DataSource = dt;
            activeTaskList.DisplayMember = "title";
            activeTaskList.ValueMember = "id";

            DataTable takentask = crud.getTakenTasks(employeeId);
            takenTasksList.DataSource = takentask;
            takenTasksList.DisplayMember = "title";
            takenTasksList.ValueMember = "id";

            foreach (DataRow dtRow in takentask.Rows)
            {
                taskCombo.Items.Add(dtRow["id"].ToString());
               
            }

        }

        private void activeTaskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = crud.getActiveTasks(departmentId);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = crud.getActiveTasks(departmentId).Rows[activeTaskList.SelectedIndex];
                taskIdLabel.Text = dr.ItemArray[0].ToString();
                titleLabel.Text = dr.ItemArray[1].ToString();
                textLabel.Text = dr.ItemArray[2].ToString();
                creationLabel.Text = dr.ItemArray[3].ToString();
                duedateLabel.Text = dr.ItemArray[4].ToString();
                taskCount.Text = "Active Tasks count: " + crud.getActiveTasks(departmentId).Rows.Count;
            }
        }

        private void takeTaskBtn_Click(object sender, EventArgs e)
        {
            crud.takeTask(employeeId, Int32.Parse(taskIdLabel.Text));
            refreshTasksList();
        }

        private void takenTasksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = crud.getTakenTasks(employeeId);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = crud.getTakenTasks(employeeId).Rows[takenTasksList.SelectedIndex];
                takenTaskId.Text = dr.ItemArray[0].ToString();
                takenTaskTitle.Text = dr.ItemArray[1].ToString();
                takenTaskText.Text = dr.ItemArray[2].ToString();
                takenTaskDate.Text = dr.ItemArray[3].ToString();
                takenTaskCreation.Text = dr.ItemArray[4].ToString();
                takenTaskDuedate.Text = dr.ItemArray[5].ToString();


                takenTaskscount.Text = "Active Tasks count: " + crud.getTakenTasks(employeeId).Rows.Count;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            crud.completeTask(employeeId, Int32.Parse(takenTaskId.Text));
            refreshTasksList();
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            string title = msgTitle.Text;
            string message = msgText.Text;
            int task_id = Int32.Parse(taskCombo.Text);

            helpcrud.addMSG(title, message, employeeId, task_id);

            msgTitle.Text = null;
            msgText.Text = null;
            taskCombo.Text = null;

        }
    }
}
