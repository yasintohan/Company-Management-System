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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            //this.Hide();
            adminLogin form = new adminLogin();
            form.Show();
        }

        private void empButton_Click(object sender, EventArgs e)
        {
            //this.Hide();
            EmployeeLogin form = new EmployeeLogin();
            form.Show();
        }
    }
}
