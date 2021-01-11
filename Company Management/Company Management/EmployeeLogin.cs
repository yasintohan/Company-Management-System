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
    public partial class EmployeeLogin : Form
    {
        LoginCrud crud = new LoginCrud();

        public EmployeeLogin()
        {
            InitializeComponent();
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            String username = txtAdminName.Text;
            String password = txtAdminPass.Text;
            int employeeid = 0;
            EmployeePanel empForm = new EmployeePanel();

            if (checkFields() == true)
            {

                MySqlDataReader row = crud.getLogin("employee", username, password);
                if (row.HasRows)
                {

                    while (row.Read())
                    {
                        employeeid = row.GetInt32(0);
                    }
                    empForm.employeeId = employeeid;
                    empForm.empUsername = username;
                    empForm.Show();

                }
                else
                {
                    MessageBox.Show("Login Failed");
                }




            }
        }


        public Boolean checkFields()
        {
            star1.Visible = false;
            star2.Visible = false;

            if (txtAdminName.Text.Trim().Equals("") && txtAdminPass.Text.Trim().Equals(""))
            {
                star1.Visible = true;
                star2.Visible = true;
                return false;
            }
            else if (txtAdminName.Text.Trim().Equals(""))
            {
                star1.Visible = true;
                return false;
            }
            else if (txtAdminPass.Text.Trim().Equals(""))
            {
                star2.Visible = true;
                return false;
            }
            else
            {
                return true;
            }

        }

    }
}
