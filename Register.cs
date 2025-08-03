using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FitnessTrackerSchool.Classes;

namespace FitnessTrackerSchool
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        UserClass user = new UserClass();

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void rdoMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

      

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

      

        private void txtboxPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxHeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxWeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void rdoFemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoOthers_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pickerDateOfBirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtboxConfirmPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtboxUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtboxUserName.Text.Trim();
            string pwd = txtboxPwd.Text.Trim();
            string confirmPwd = txtboxConfirmPwd.Text.Trim();
            string height = txtboxHeight.Text.Trim();
            string weight = txtboxWeight.Text.Trim();
            DateTime birthDate = pickerDateOfBirth.Value.Date;
            string phone = txtboxPhone.Text.Trim();
            string address = txtboxAddress.Text.Trim();
            string age = txtboxAge.Text.Trim();
            string gender = rdoMale.Checked ? "Male"
                                     : rdoFemale.Checked ? "Female"
                                     : rdoOthers.Checked ? "Others"
                                     : null;
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("UsernamePlease enter a username.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxUserName.Focus();
                return;
            }

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Username can only contain letters and numbers.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxUserName.Focus();
                return;
            }

            if (pwd.Length != 12 || confirmPwd.Length != 12)
            {
                MessageBox.Show("Password must be exactly 12 characters long.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxPwd.Focus();
                return;
            }


            if (!Regex.IsMatch(pwd, @"^(?=.*[a-z])(?=.*[A-Z]).{12}$"))
            {
                MessageBox.Show("Password must contain at least one lowercase letter.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxPwd.Focus();
                return;
            }

            if (pwd != confirmPwd)
            {
                MessageBox.Show("Passwords do not match. Please re‑enter.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxPwd.Clear();
                txtboxConfirmPwd.Clear();
                txtboxPwd.Focus();
                return;
            }

            if (string.IsNullOrEmpty(age))
            {
                MessageBox.Show("Please enter a age.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxAge.Focus();
                return;
            }

            if (string.IsNullOrEmpty(height))
            {
                MessageBox.Show("Please enter a height.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pickerDateOfBirth.Focus();
                return;
            }

            if (string.IsNullOrEmpty(weight))
            {
                MessageBox.Show("Please enter a weight.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pickerDateOfBirth.Focus();
                return;
            }

            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please enter a phone number.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxPhone.Focus();
                return;
            }
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please enter an address.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxAddress.Focus();
                return;
            }


            if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Please select a gender.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            user.UserName = username;
            user.Password = pwd;
            user.Height = Convert.ToInt32(height);
            user.Weight = Convert.ToInt32(weight);
            user.Birthdate = birthDate.ToString();
            user.Phone = phone;
            user.Address = address;
            user.Gender = gender;
            user.Age = Convert.ToInt32(age);

            try
            {
                bool success = user.Insert(user);
                if (success)
                {
                    MessageBox.Show("Registration successful! Redirecting to login...", "Welcome",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login login = new Login();
                    login.ShowDialog();
                    this.Close();
                  
                }
                else
                {
                    MessageBox.Show("Registration failed. Please try again.", "Error",
                                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred:\n" + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
