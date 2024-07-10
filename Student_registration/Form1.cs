using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_registration
{
    public partial class login_form : Form
    {
        private static string userName = "Admin";
        private static string password = "esoft@123";
        public login_form()
        {
            InitializeComponent();
        }

        private void login_form_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string getId = txtUserName.Text;
            string getPassword = txtPassword.Text;

            if (getId.Equals(userName) && getPassword.Equals(password))
            {
                registration_frm registration_Frm = new registration_frm();
                registration_Frm.Show();
                this.Hide();


            }
            else
            {
                MessageBox.Show("Invalid Login Credentials,please check Username and Password and try again", "Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassword.Text = " ";
            txtUserName.Text = " ";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure,Do You really want to Exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

		private void label3_Click(object sender, EventArgs e)
		{

		}
	}
}
