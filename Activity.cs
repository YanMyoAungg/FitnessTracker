using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FitnessTrackerSchool.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FitnessTrackerSchool
{
    public partial class Activity : Form
    {
        private int currentUserId;
        private ActivityClass activity;

        public Activity()
        {
            InitializeComponent();

            currentUserId = Session.CurrentUserId;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string activity = comboActivity.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(activity))
            {
                MessageBox.Show("Please select an activity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (activity)
            {
                case "Running":
                    lblMetric1.Text = "Duration (min):";
                    lblMetric2.Text = "Distance (km):";
                    lblMetric3.Text = "Avg Pace (min/km):";
                    break;

                case "Swimming":
                    lblMetric1.Text = "Time Taken (min):";
                    lblMetric2.Text = "Laps:";
                    lblMetric3.Text = "Avg Heart Rate (bpm):";
                    break;

                case "Cycling":
                    lblMetric1.Text = "Duration (min):";
                    lblMetric2.Text = "Distance (km):";
                    lblMetric3.Text = "Resistance Level:";
                    break;

                case "Walking":
                    lblMetric1.Text = "Time Taken (min):";
                    lblMetric3.Text = "Distance (km):";
                    lblMetric2.Text = "Step Count:";
                    break;

                case "Jump Rope":
                    lblMetric1.Text = "Duration (min):";
                    lblMetric2.Text = "Jump Count:";
                    lblMetric3.Text = "Intensity (1–10):";
                    break;

                case "Rowing":
                    lblMetric1.Text = "Duration (min):";
                    lblMetric2.Text = "Stroke Count:";
                    lblMetric3.Text = "Distance (km):";
                    break;
            }

            numeMetric1.Value = 0;
            numeMetric2.Value = 0;
            numeMetric3.Value = 0;
        }

        private void Activity_Load(object sender, EventArgs e)
        {
            dgvActivities.DataSource = ActivityClass.LoadActivitiesForAll(currentUserId);
            dgvActivities.Columns["Id"].Visible = false;
            dgvActivities.CellContentClick += dgvActivitiesDelete;
            //lblDailyTotal.Text = $"Today's Total: 0.00 cal";

            dateTimePicker1.ValueChanged += (s, ev) =>
            {
                LoadActivities(dateTimePicker1.Value.Date);
            };
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            DateTime dateString = dateTimePicker1.Value.Date;
            dateString.ToString("yyyy-MM-dd");
            bool exists = GoalClass.HasGoalForDate(Session.CurrentUserId, dateString);
            if (!exists)
            {
                MessageBox.Show("Please set the goal first.", "No Goal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Goal goal = new Goal();
                goal.ShowDialog();
                return;
            }

            string activityType = comboActivity.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(activityType))
            {
                MessageBox.Show("Please enter all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime date = dateTimePicker1.Value;
            double m1 = (double)numeMetric1.Value; // Duration
            double m2 = (double)numeMetric2.Value; // Metric2
            double m3 = (double)numeMetric3.Value; // Metric3

            switch (activityType)
            {
                case "Swimming":
                    activity = new ActivityClass.SwimmingActivity
                    {
                        UserId = currentUserId,
                        Date = date,
                        Duration = m1,
                        Metric2 = m2, //lap count
                        Metric3 = m3  // avg heart rate
                    };
                    break;
                case "Walking":
                    activity = new ActivityClass.WalkingActivity
                    {
                        UserId = currentUserId,
                        Date = date,
                        Duration = m1,
                        Metric2 = m2, // Distance(km)
                        Metric3 = m3, // Step Count
                    };
                    break;
                case "Running":
                    activity = new ActivityClass.RunningActivity
                    {
                        UserId = currentUserId,
                        Date = date,
                        Duration = m1,
                        Metric2 = m2, // Distance (km)
                        Metric3 = m3 // pace (min/km) 
                    };
                    break;
                case "Cycling":
                    activity = new ActivityClass.CyclingActivity
                    {
                        UserId = currentUserId,
                        Date = date,
                        Duration = m1,
                        Metric2 = m2, // Distance (km)
                        Metric3 = m3 // Resistance Level
                    };
                    break;
                case "Jump Rope":
                    activity = new ActivityClass.JumpRopeActivity
                    {
                        UserId = currentUserId,
                        Date = date,
                        Duration = m1,
                        Metric2 = m2, // Jump Count
                        Metric3 = m3 // Intensity Level
                    };
                    break;
                case "Rowing":
                    activity = new ActivityClass.RowingActivity
                    {
                        UserId = currentUserId,
                        Date = date,
                        Duration = m1,
                        Metric2 = m2, // Stroke Count
                        Metric3 = m3 // Distance (km)
                    };
                    break;
                default:
                    MessageBox.Show("Unknown activity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            double calories = activity.CalculateCalories();
            numeCaloriesBurned.Text = calories.ToString("0.00");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.CurrentUserId = 0;
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }

        private void txtboxCaloriesTextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Goal goal = new Goal();
            goal.ShowDialog();
            this.Close();
        }

        private void numeCaloriesBurned_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime dateString = dateTimePicker1.Value.Date;
            dateString.ToString("yyyy-MM-dd");
            bool exists = GoalClass.HasGoalForDate(Session.CurrentUserId, dateString);
            if (!exists)
            {
                MessageBox.Show("Please set the goal first.", "No Goal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Goal goal = new Goal();
                goal.ShowDialog();
                return;
            }

            if (activity == null)
            {
                MessageBox.Show("Please calculate the activity before saving.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
            bool saved = false;
            switch (activity)
            {
                case ActivityClass.SwimmingActivity swim:
                    saved = swim.Insert(); break;
                case ActivityClass.WalkingActivity walk:
                    saved = walk.Insert(); break;
                case ActivityClass.RunningActivity run:
                    saved = run.Insert(); break;
                case ActivityClass.CyclingActivity cycle:
                    saved = cycle.Insert(); break;
                case ActivityClass.JumpRopeActivity jump:
                    saved = jump.Insert(); break;
                case ActivityClass.RowingActivity row:
                    saved = row.Insert(); break;
            }

            if (saved)
            {
                MessageBox.Show("Activity saved successfully.");
                LoadActivities(dateTimePicker1.Value.Date);
            }
            else
            {
                MessageBox.Show("Failed to save activity.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvActivities.Columns[e.ColumnIndex].Name != "Delete" || e.RowIndex < 0)
                return;

            // Get the activity ID from the hidden column
            int activityId = (int)dgvActivities.Rows[e.RowIndex].Cells["Id"].Value;
            //MessageBox.Show(activityId.ToString());

            // Confirm deletion
            var result = MessageBox.Show(
                "Delete this activity?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            SQLiteConnection conn = new SQLiteConnection(DatabaseHandler.connString);
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM tbl_activity WHERE activity_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", activityId);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();

            if (rows > 0)
            {
                MessageBox.Show("Activity deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Refresh current grid
                LoadActivities(dateTimePicker1.Value.Date);
            }
            else
            {
                MessageBox.Show("Failed to delete activity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void LoadActivities(DateTime date)
        {
            DataTable table = ActivityClass.LoadActivitiesForDate(date);
            dgvActivities.DataSource = table;

            UpdateDailyTotal(date);


        }
        private void dgvActivitiesDelete(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvActivities.Rows[e.RowIndex];

            // ignore header and out‑of‑bounds clicks
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // check is Delete button column
            if (dgvActivities.Columns[e.ColumnIndex].Name != "deleteBtn") return;

            if (row.IsNewRow) return;

            // get the activity_id from Id column
            object val = row.Cells["Id"].Value;
            if (val == DBNull.Value || val == null)
            {
                MessageBox.Show("Invalid activity ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int activityId = Convert.ToInt32(val);

            // Confirm
            var result = MessageBox.Show("Delete this activity?", "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            bool isDeleted = ActivityClass.DeleteActivity(activityId);


            if (isDeleted)
            {
                MessageBox.Show("Activity deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadActivities(dateTimePicker1.Value.Date);
            }
            else
            {
                MessageBox.Show("Unable to delete activity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            dgvActivities.DataSource = ActivityClass.LoadActivitiesForAll(currentUserId);
            lblDailyTotal.Text = $"";
            lblGoalTarget.Text = $"";
            lblGoalStatus.Text = $"";

        }

        private void UpdateDailyTotal(DateTime date)
        {
            var table = (DataTable)dgvActivities.DataSource;
            double total = table.AsEnumerable()
                                .Sum(r => r.Field<double>("Calories"));
            lblDailyTotal.Text = $"Total Burned: {total:0.00} cal";
            int goalTarget = GoalClass.GetGoalForDate(currentUserId, date);
            lblGoalTarget.Text = $"Today's Goal: {goalTarget:0.00} cal";
            DateTime dateString = dateTimePicker1.Value;
            bool isAchieved = GoalClass.IsGoalAchieved(currentUserId, date);

            if (isAchieved)
            {
                lblGoalStatus.Text = "Goal achieved.";
                lblGoalStatus.ForeColor = Color.Purple;
                lblGoalStatus.Font = new Font(lblGoalStatus.Font, FontStyle.Bold);
                return;
            }
            if (goalTarget > 0)
            {

                if (total >= goalTarget)
                {
                    GoalClass goalClass = new GoalClass();
                    goalClass.MarkAsAchieved(dateString);
                    lblGoalStatus.Text = "Goal achieved!";
                    lblGoalStatus.ForeColor = Color.Green;
                    lblGoalStatus.Font = new Font(lblGoalStatus.Font, FontStyle.Bold);

                }
                else
                {
                    lblGoalStatus.Text = "";
                }
            } else{
                lblGoalStatus.Text = "No goal set for this date.";
                lblGoalStatus.ForeColor = Color.Blue;
                lblGoalStatus.Font = new Font(lblGoalStatus.Font, FontStyle.Bold);
            }
        }
        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
