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
    public partial class Goal : Form
    {
        public Goal()
        {
            InitializeComponent();
        }

        int userId = Session.CurrentUserId;
        GoalClass goal = new GoalClass ();

        private void Goal_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Activity activity = new Activity();
            activity.ShowDialog();
            this.Close();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.CurrentUserId = 0;
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveGoal_Click(object sender, EventArgs e)
        {
            DateTime dateString = dtpGoalDate.Value.Date;
            int target = (int)numeTargetGoal.Value;
            const int minimumCalories = 50;


            DateTime selectedDate = dtpGoalDate.Value.Date; 
            DateTime today = DateTime.Now.Date;
            bool isEqual = selectedDate < today;
            if (isEqual)
            {
                MessageBox.Show("Goal can only be set for today and upcoming days.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpGoalDate.Focus();
                return;
            }

            if (target < minimumCalories)
            {
                MessageBox.Show($"Please enter at least {minimumCalories} calories for your goal.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numeTargetGoal.Focus();
                return;
            }
            
           

            bool exists = GoalClass.HasGoalForDate(Session.CurrentUserId, dateString);
            if (exists)
            {
                MessageBox.Show("You already have a goal set for that date.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            goal.UserId = userId;
            goal.Date = dateString;
            goal.TargetCalories = target; 

            bool isSuccess = goal.Insert(goal);
            if (isSuccess)
            {
                MessageBox.Show("Goal saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Activity activity = new Activity();
                activity.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Could not save goal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelGoal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

