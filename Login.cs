using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FitnessTrackerSchool.Classes;

namespace FitnessTrackerSchool
{
    public partial class Login : Form
    {
        UserClass user = new UserClass();
        int userId = Session.CurrentUserId;

        private const int MAX_ATTEMPTS = 5;
        private int failedAttempts = 0;
        private DateTime lockoutEnd = DateTime.MinValue;
        private Timer lockoutTimer;

        public Login()
        {
            InitializeComponent();
            lockoutTimer = new Timer();
            lockoutTimer.Interval = 1000;
            lockoutTimer.Tick += LockoutTimer_Tick;
        }

        
        private void lblConfirmPwd_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            failedAttempts = 0;
            lockoutTimer.Stop();
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtboxUserName.Text.Trim();
            string password = txtboxPwd.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter username and password.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxPwd.Focus();
                return;
            }

            bool isValid = user.Login(username, password);
            if (isValid) 
            {
                failedAttempts = 0;
                lockoutTimer.Stop();
                btnLogin.Enabled = true;
                
                int userId = Session.CurrentUserId;

                bool hasGoal = GoalClass.HasActiveGoal(userId);

                if (!hasGoal)
                {
                    MessageBox.Show("Login successful! Please set the goal first", "Welcome",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Goal goalForm = new Goal();
                    goalForm.ShowDialog();
                    return;
                }
                MessageBox.Show("Login successful! Redirecting to Activity Page...", "Welcome",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                Activity activityForm = new Activity();
                activityForm.ShowDialog();
                this.Close();
            }
            else
            {
                failedAttempts++;
                if (failedAttempts >= MAX_ATTEMPTS)
                {
                    // start a 30‑second lockout
                    lockoutEnd = DateTime.Now.AddSeconds(30);
                    btnLogin.Enabled = false;
                    lockoutTimer.Start();
                    MessageBox.Show(
                        $"Too many failed attempts. Locked out for 30 seconds.",
                        "Login Locked",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        $"Invalid credentials. You have {MAX_ATTEMPTS - failedAttempts} attempts left.",
                        "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LockoutTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= lockoutEnd)
            {
                lockoutTimer.Stop();
                btnLogin.Enabled = true;
                failedAttempts = 0;
                btnLogin.Text = "Login";
                MessageBox.Show("You may try logging in again.", "Lockout Ended",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var secLeft = (lockoutEnd - DateTime.Now).Seconds;
                btnLogin.TextAlign = ContentAlignment.MiddleCenter;
                btnLogin.Text = $"({secLeft}s)";
            }
        }
    }
}
