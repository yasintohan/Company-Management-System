using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company_Management
{
    public partial class AdminPanel : Form
    {
        public int adminId { get; set; }
        public string adminusername { get; set; }
        public string adminpassword { get; set; }

        Department department = new Department();
        LoginCrud crud = new LoginCrud();
        taskCrud taskcrud = new taskCrud();
        AdminCrud adminCrud = new AdminCrud();
        helpCrud helpcrud = new helpCrud();


        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            welcomeLabel.Text = "Admin: " + adminusername;
            refreshDepList();
            refreshEmpDataGrid();
            refreshTaskDataGrid();
            refreshAdminList();
            refreshHelpDataGrid();




        }

        private void btnAddDep_Click(object sender, EventArgs e)
        {
            
            if (!depIdText.Text.Trim().Equals("") && !txtDepName.Text.Trim().Equals("") && !depDescription.Text.Trim().Equals(""))
            {
                int ids = Int32.Parse(depIdText.Text);
                bool contains = department.getDepartments().AsEnumerable().Any(row => ids == row.Field<int>("dep_id"));
                if(!contains)
                {
                    department.addDepartment(txtDepName.Text, depDescription.Text);
                    refreshDepList();
                }
                


            }
        }

        private void btnEditDep_Click(object sender, EventArgs e)
        {
            
            if (!depIdText.Text.Trim().Equals("") && !txtDepName.Text.Trim().Equals("") && !depDescription.Text.Trim().Equals(""))
            {

                int ids = Int32.Parse(depIdText.Text);
                bool contains = department.getDepartments().AsEnumerable().Any(row => ids == row.Field<int>("dep_id"));
                if (contains)
                {
                    department.updDepartment(ids, txtDepName.Text, depDescription.Text);
                    MessageBox.Show("Updated");
                    refreshDepList();
                }


            }
        }

        private void depList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = department.getDepartments();
            if(dt.Rows.Count > 0)
            {

                DataRow dr = department.getDepartments().Rows[depList.SelectedIndex];
                depIdText.Text = dr.ItemArray[0].ToString();
                txtDepName.Text = dr.ItemArray[1].ToString();
                depDescription.Text = dr.ItemArray[2].ToString();
                depCount.Text = "Deparment count: " + department.getDepartments().Rows.Count; }
        }

        private void btnRemoveDep_Click(object sender, EventArgs e)
        {
            
            if (!depIdText.Text.Trim().Equals(""))
            {

                int ids = Int32.Parse(depIdText.Text);
                bool contains = department.getDepartments().AsEnumerable().Any(row => ids == row.Field<int>("dep_id"));
                if(contains)
                {
                    department.delDepartment(ids);
                    MessageBox.Show("Deleted");
                    refreshDepList();
                }
               


            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!txtEmployeId.Text.Trim().Equals("") && !txtEmployeName.Text.Trim().Equals("") && !txtEmployeSurname.Text.Trim().Equals("") && !txtEmployeUserName.Text.Trim().Equals("") &&
                !txtEmployeEmail.Text.Trim().Equals("") && !txtEmployePsswrd.Text.Trim().Equals("") && !comboBoxDepOfEmploye.Text.Trim().Equals(""))
            {
                int ids = Int32.Parse(txtEmployeId.Text);
                bool contains = crud.getUsers().AsEnumerable().Any(row => ids == row.Field<int>("emp_id"));
                if (!contains)
                {
                    string name = txtEmployeName.Text;
                    string surname = txtEmployeSurname.Text;
                    string username = txtEmployeUserName.Text;
                    string email = txtEmployeEmail.Text;
                    string pass = txtEmployePsswrd.Text;
                    string dep = comboBoxDepOfEmploye.Text;
                    DateTime datet = dateTimeEmp.Value;
                    

                    crud.addUser(name, surname,username,email, datet, pass,dep);
                    refreshEmpDataGrid();
                }



            }
        }

       

       

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {// edit button
                txtEmployeId.Text = dataGrid.CurrentRow.Cells[2].Value.ToString();
                txtEmployeUserName.Text = dataGrid.CurrentRow.Cells[3].Value.ToString();
                txtEmployePsswrd.Text = dataGrid.CurrentRow.Cells[4].Value.ToString();
                txtEmployeName.Text = dataGrid.CurrentRow.Cells[5].Value.ToString();
                txtEmployeSurname.Text = dataGrid.CurrentRow.Cells[6].Value.ToString();
                txtEmployeEmail.Text = dataGrid.CurrentRow.Cells[7].Value.ToString();
                comboBoxDepOfEmploye.Text = dataGrid.CurrentRow.Cells[9].Value.ToString();
                string datestr = dataGrid.CurrentRow.Cells[8].Value.ToString();
                
                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime dateTime = DateTime.ParseExact(datestr, "MM/dd/yyyy", provider);

                
                dateTimeEmp.CustomFormat = "MM/dd/yyyy";
               dateTimeEmp.Value = dateTime;




            }
            if (e.ColumnIndex == 1)
            {// delete button
                string message = "Do you want to delete this employee?";
                string title = "Delete Employee";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    crud.delUser(Convert.ToInt32(dataGrid.CurrentRow.Cells[2].Value));
                    refreshEmpDataGrid();
                }
                else
                {
                    
                }

                
            }
        }


        private void refreshEmpDataGrid()
        {
            DataTable dtemp = crud.getUsers();


            DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();

            editBtn.HeaderText = "Edit";
            editBtn.Text = "Edit";
            editBtn.UseColumnTextForButtonValue = true;
            editBtn.DefaultCellStyle.BackColor = Color.Green;
            editBtn.Width = 50;

            DataGridViewButtonColumn delBtn = new DataGridViewButtonColumn();

            delBtn.HeaderText = "Del";
            delBtn.Text = "Del";
            delBtn.UseColumnTextForButtonValue = true;
            delBtn.DefaultCellStyle.BackColor = Color.Red;
            delBtn.Width = 30;
            dataGrid.Columns.Clear();
            dataGrid.Columns.Add(editBtn);
            dataGrid.Columns.Add(delBtn);
            dataGrid.DataSource = dtemp;
        }

        private void refreshTaskDataGrid()
        {
            DataTable dtemp = taskcrud.getActiveTasks();
            DataTable takentask = taskcrud.getTakenTasks();
            

            DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();

            editBtn.HeaderText = "Edit";
            editBtn.Text = "Edit";
            editBtn.UseColumnTextForButtonValue = true;
            editBtn.DefaultCellStyle.BackColor = Color.Green;
            editBtn.Width = 50;

            DataGridViewButtonColumn delBtn = new DataGridViewButtonColumn();

            delBtn.HeaderText = "Del";
            delBtn.Text = "Del";
            delBtn.UseColumnTextForButtonValue = true;
            delBtn.DefaultCellStyle.BackColor = Color.Red;
            delBtn.Width = 30;

            taskGridView.Columns.Clear();
            taskGridView.Columns.Add(editBtn);
            taskGridView.Columns.Add(delBtn);
            taskGridView.DataSource = dtemp;

            completedTaskGrid.DataSource = takentask;
            
        }


        private void refreshDepList()
        {
            DataTable dt = department.getDepartments();
            depList.DataSource = dt;
            depList.DisplayMember = "name";
            depList.ValueMember = "dep_id";

            foreach (DataRow dtRow in dt.Rows)
            {
                comboBoxDepOfEmploye.Items.Add(dtRow["name"].ToString());
                cmbBoxDepartment.Items.Add(dtRow["name"].ToString());
            }

        }

        private void refreshAdminList()
        {
            DataTable dt = adminCrud.getAdmins();
            AdminlistBox.DataSource = dt;
            AdminlistBox.DisplayMember = "username";
            AdminlistBox.ValueMember = "admin_id";

            

        }


        private void button5_Click(object sender, EventArgs e)
        {
            if (!txtEmployeId.Text.Trim().Equals("") && !txtEmployeName.Text.Trim().Equals("") && !txtEmployeSurname.Text.Trim().Equals("") && !txtEmployeUserName.Text.Trim().Equals("") &&
               !txtEmployeEmail.Text.Trim().Equals("") && !txtEmployePsswrd.Text.Trim().Equals("") && !comboBoxDepOfEmploye.Text.Trim().Equals(""))
            {
                int ids = Int32.Parse(txtEmployeId.Text);
                bool contains = crud.getUsers().AsEnumerable().Any(row => ids == row.Field<int>("emp_id"));
                if (contains)
                {
                    int id = Convert.ToInt32(txtEmployeId.Text);
                    string name = txtEmployeName.Text;
                    string surname = txtEmployeSurname.Text;
                    string username = txtEmployeUserName.Text;
                    string email = txtEmployeEmail.Text;
                    string pass = txtEmployePsswrd.Text;
                    string dep = comboBoxDepOfEmploye.Text;
                    DateTime datet = dateTimeEmp.Value;


                    crud.updUser(id, name, surname, username, email, datet, pass, dep);
                    refreshEmpDataGrid();
                }



            }
        }

        private void addTaskbtn_Click(object sender, EventArgs e)
        {
            if (!taskIdText.Text.Trim().Equals("") && !taskTitleText.Text.Trim().Equals("") && !TaskText.Text.Trim().Equals("") && !cmbBoxDepartment.Text.Trim().Equals(""))
            {
                int ids = Int32.Parse(taskIdText.Text);
                bool contains = taskcrud.getActiveTasks().AsEnumerable().Any(row => ids == row.Field<int>("id"));
                if (!contains)
                {
                    string title = taskTitleText.Text;
                    string text = TaskText.Text;
                    string dep = cmbBoxDepartment.Text;
                    DateTime datet = dueDateTimePicler.Value;


                    taskcrud.addTask(title,text,datet,dep, adminId);
                    refreshTaskDataGrid();
                }



            }
        }

        private void taskGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {// edit button
                taskIdText.Text = taskGridView.CurrentRow.Cells[2].Value.ToString();
                taskTitleText.Text = taskGridView.CurrentRow.Cells[3].Value.ToString();
                TaskText.Text = taskGridView.CurrentRow.Cells[4].Value.ToString();
                cmbBoxDepartment.Text = taskGridView.CurrentRow.Cells[8].Value.ToString();
                

                string datestr = taskGridView.CurrentRow.Cells[6].Value.ToString();
                
                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime dateTime = DateTime.ParseExact(datestr, "MM/dd/yyyy", provider);


                dueDateTimePicler.CustomFormat = "MM/dd/yyyy";
                dueDateTimePicler.Value = dateTime;
                



            }
            if (e.ColumnIndex == 1)
            {// delete button
                string message = "Do you want to delete this task?";
                string title = "Delete Task";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    taskcrud.delTask(Convert.ToInt32(dataGrid.CurrentRow.Cells[2].Value));
                    refreshTaskDataGrid();
                }
                else
                {

                }


            }

        }

        private void editTaskbtn_Click(object sender, EventArgs e)
        {
            if (!taskIdText.Text.Trim().Equals("") && !taskTitleText.Text.Trim().Equals("") && !TaskText.Text.Trim().Equals("") && !cmbBoxDepartment.Text.Trim().Equals(""))
            {
                int ids = Int32.Parse(taskIdText.Text);
                bool contains = taskcrud.getActiveTasks().AsEnumerable().Any(row => ids == row.Field<int>("id"));
                if (contains)
                {
                    
                    string title = taskTitleText.Text;
                    string text = TaskText.Text;
                    string dep = cmbBoxDepartment.Text;
                    DateTime datet = dueDateTimePicler.Value;


                    taskcrud.updTask(ids, title, text, datet, dep);
                    refreshTaskDataGrid();
                }



            }
        }

        private void Adminaddbtn_Click(object sender, EventArgs e)
        {
            if (!adminIdText.Text.Trim().Equals("") && !adminPassText.Text.Trim().Equals("") && !adminUsernameText.Text.Trim().Equals(""))
            {
                int ids = Int32.Parse(adminIdText.Text);
                bool contains = adminCrud.getAdmins().AsEnumerable().Any(row => ids == row.Field<int>("admin_id"));
                if (!contains)
                {
                    adminCrud.addAdmin(adminUsernameText.Text, adminPassText.Text);
                    refreshAdminList();
                }



            }
        }

        private void AdminlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = adminCrud.getAdmins();
            if (dt.Rows.Count > 0)
            {
                DataRow dr = adminCrud.getAdmins().Rows[AdminlistBox.SelectedIndex];
                adminIdText.Text = dr.ItemArray[0].ToString();
                adminUsernameText.Text = dr.ItemArray[1].ToString();
                adminPassText.Text = dr.ItemArray[2].ToString();
            }
        }



        private void refreshHelpDataGrid()
        {
            DataTable dtemp = helpcrud.getHelps();


            
            DataGridViewButtonColumn delBtn = new DataGridViewButtonColumn();

            delBtn.HeaderText = "Del";
            delBtn.Text = "Del";
            delBtn.UseColumnTextForButtonValue = true;
            delBtn.DefaultCellStyle.BackColor = Color.Red;
            delBtn.Width = 30;
            helpGrid.Columns.Clear();


            helpGrid.Columns.Add(delBtn);
            helpGrid.DataSource = dtemp;
        }

        private void helpGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.ColumnIndex == 0)
            {// delete button
                string message = "Do you want to delete this help message?";
                string title = "Delete Message";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    helpcrud.delMSG(Convert.ToInt32(helpGrid.CurrentRow.Cells[1].Value));
                    refreshHelpDataGrid();
                }
                else
                {

                }


            }
        }
    }
}
