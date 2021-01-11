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
    public partial class adminLogin : Form
    {

        LoginCrud crud = new LoginCrud();

        public adminLogin()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username = txtAdminName.Text;
            String password = txtAdminPass.Text;
            int adminid = 0;
            AdminPanel adminform = new AdminPanel();

            if (checkFields() == true)
            {

                MySqlDataReader row = crud.getLogin("admin", username, password);
                if (row.HasRows)
                {
                    
                    while (row.Read())
                    {
                        adminid = row.GetInt32(0);
                    }
                    adminform.adminId = adminid;
                    adminform.adminusername = username;
                    adminform.adminpassword = password;
                    adminform.Show();
                
                } else
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
