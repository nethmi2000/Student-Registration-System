﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Student_registration
{
    public partial class registration_frm : Form
    {
        public SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-NHEOJHS;Initial Catalog=StudentManagementSystem  0.2;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public SqlDataAdapter SqlDa = new SqlDataAdapter();
        public SqlCommand cmd = new SqlCommand();
        public DataSet Dset = new DataSet();
        public registration_frm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {

                string gender = " ";
                bool isCheck = rdbtnMale.Checked;
                if (isCheck)
                {
                    gender = rdbtnMale.Text;
                }
                else
                {
                    gender = rdBtnFemale.Text;
                }
               
                String sql = "INSERT INTO Student VALUES" + "('" + txtFirstName.Text + "','" + txtSirstName.Text + "','" + txtDateTimePicker.Value.Date + "','" + gender + "','" + txtAddress.Text + "','" + txtEmail.Text + "','" + txtMobilePhone.Text + "','" + txtHomePhone.Text + "','" + txtParentName.Text + "','" + txtNIC.Text + "','" + txtContact.Text + "')";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added Succesfully", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadIds();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Faield", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { con.Close(); }
        }

		


		private void btnClear_Click(object sender, EventArgs e)
		{
			
			txtFirstName.Clear();
			txtSirstName.Clear();
			txtAddress.Clear();
			txtEmail.Clear();
			txtMobilePhone.Clear();
			txtHomePhone.Clear();
			txtParentName.Clear();
			txtNIC.Clear();
			txtContact.Clear();

			cmbRegNO.SelectedIndex = -1; 
										
			cmbRegNO.Text = ""; 

			txtDateTimePicker.Value = DateTime.Now; 

			rdbtnMale.Checked = false;
			rdBtnFemale.Checked = false;
		}

		private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure,You want to Delete Student Register No:" + cmbRegNO.Text + "?", "Deleted", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    
                    string sql = "DELETE  FROM Student WHERE regNo ='" + cmbRegNO.Text + "'";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Register No:" + cmbRegNO.Text, "Deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    con.Close();
                    txtFirstName.Clear();
                    txtSirstName.Clear();
                    txtAddress.Clear();
                    txtEmail.Clear();
                    txtMobilePhone.Clear();
					txtHomePhone.Clear();

					txtParentName.Clear();
                    txtNIC.Clear();

                    txtContact.Clear();

                    loadIds();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { con.Close(); }
        }
		private void loadIds()
		{
			try
			{

				string sql = "SELECT regNo FROM Student ORDER BY regNo";
				con.Open();
				SqlDa = new SqlDataAdapter(sql, con);
				DataTable dt = new DataTable();
				SqlDa.Fill(dt);
				con.Close();

				cmbRegNO.Items.Clear();

				cmbRegNO.Items.Add(dt);
				foreach (DataRow row in dt.Rows)
				{
					cmbRegNO.Items.Add(row["regNo"]);


				}
				cmbRegNO.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			finally { con.Close(); }
		}

		private void cmbRegNO_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{

				string sql = "SELECT  firstName , lastName , dateOfBirth , gender , address , email, mobilePhone , homePhone , parentName , nic , contactNo  FROM Student WHERE regNo='" + cmbRegNO.Text + "'";
				con.Open();
				cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("regNo", cmbRegNO.Text);

				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					txtFirstName.Text = dr.GetValue(0).ToString();
					txtSirstName.Text = dr.GetValue(1).ToString();
					txtDateTimePicker.Text = dr.GetValue(2).ToString();
					string gender = dr.GetValue(3).ToString();
					if (gender.Equals("Male"))
					{

					}
					else
					{

					}
					txtAddress.Text = dr.GetValue(4).ToString();
					txtEmail.Text = dr.GetValue(5).ToString();
					txtMobilePhone.Text = dr.GetValue(6).ToString();
					txtHomePhone.Text = dr.GetValue(7).ToString();
					txtParentName.Text = dr.GetValue(8).ToString();
					txtNIC.Text = dr.GetValue(9).ToString();
					txtContact.Text = dr.GetValue(10).ToString();


				}
				con.Close();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			finally { con.Close(); }
		}









		private void linkLabelLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			login_form log = new login_form();
			log.Show();
			this.Hide();

		}

		private void linkLabelExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DialogResult result = MessageBox.Show("Are you sure, Do you really want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				this.Close();
			}

		}

		private void btnUpdate_Click_1(object sender, EventArgs e)
		{
			try
			{
				string gender = rdbtnMale.Checked ? rdbtnMale.Text : rdBtnFemale.Text;

				string sql = "UPDATE Student SET firstName = @FirstName, lastName = @LastName, dateOfBirth = @DateOfBirth, " +
							 "gender = @Gender, address = @Address, email = @Email, mobilePhone = @MobilePhone, " +
							 "homePhone = @HomePhone, parentName = @ParentName, nic = @NIC, contactNo = @ContactNo " +
							 "WHERE regNo = @RegNo";

				con.Open();
				cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
				cmd.Parameters.AddWithValue("@LastName", txtSirstName.Text);
				cmd.Parameters.AddWithValue("@DateOfBirth", txtDateTimePicker.Value.Date);
				cmd.Parameters.AddWithValue("@Gender", gender);
				cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
				cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
				cmd.Parameters.AddWithValue("@MobilePhone", txtMobilePhone.Text);
				cmd.Parameters.AddWithValue("@HomePhone", txtHomePhone.Text);
				cmd.Parameters.AddWithValue("@ParentName", txtParentName.Text);
				cmd.Parameters.AddWithValue("@NIC", txtNIC.Text);
				cmd.Parameters.AddWithValue("@ContactNo", txtContact.Text);
				cmd.Parameters.AddWithValue("@RegNo", cmbRegNO.Text);
				cmd.ExecuteNonQuery();
				MessageBox.Show("Record Updated Successfully", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
				con.Close();
				loadIds();
				
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to update record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				if (con.State == ConnectionState.Open) con.Close();
			}

		}
	}
}
