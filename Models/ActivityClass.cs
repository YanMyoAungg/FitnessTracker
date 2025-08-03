using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FitnessTrackerSchool.Database;

namespace FitnessTrackerSchool.Classes
{
    internal abstract class ActivityClass
    {
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public double Duration { get; set; } // Metric 1 - in minutes
        public double Metric2 { get; set; }
        public double Metric3 { get; set; }
        public double CaloriesBurned { get; set; }

        public abstract double CalculateCalories();

        private static readonly string connString = DatabaseHandler.connectionString;

        public static DataTable LoadActivitiesForDate(DateTime date)
        {
            DataTable table = new DataTable();
            SQLiteConnection conn = new SQLiteConnection(connString);

            try
            {
                string sql = @"SELECT  activity_id AS Id, date AS Date, activity_type AS Activity, calories_burned AS Calories FROM tbl_activity WHERE user_id = @userId AND date = @date";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", Session.CurrentUserId);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                conn.Open();
                adapter.Fill(table);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();

            }
            return table;
        }
        public static DataTable LoadActivitiesForAll(int userId)
        {
            DataTable table = new DataTable();
            SQLiteConnection conn = new SQLiteConnection(connString);

            try
            {
                string sql = @"SELECT  activity_id AS Id, date AS Date, activity_type AS Activity, calories_burned AS Calories FROM tbl_activity WHERE user_id = @userId ORDER BY date DESC";
                
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", Session.CurrentUserId);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                conn.Open();
                adapter.Fill(table);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();

            }
            return table;
        }


        public static bool DeleteActivity(int activityId)
        {
            SQLiteConnection conn = new SQLiteConnection(DatabaseHandler.connectionString);
            bool isDeleted = false;
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(
                "DELETE FROM tbl_activity WHERE activity_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", activityId);
                conn.Open();
                int deleted = cmd.ExecuteNonQuery();
                if (deleted > 0)
                {
                    isDeleted = true;
                }
                else
                {
                    isDeleted = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return isDeleted;
        }

        // --- Walking Activity ---
        internal class WalkingActivity : ActivityClass
        {
            // Metric2: Step Count
            // Metric3: Distance (km)

            public override double CalculateCalories()
            {
                CaloriesBurned = (Duration * 4) + (Metric2 * 0.0008) + (Metric3 * 45); 
                return Convert.ToInt32(CaloriesBurned);
            }

            public bool Insert()
            {
                SQLiteConnection conn = new SQLiteConnection(connString);
                string sql = @"INSERT INTO tbl_activity (user_id, date, duration, metric2, metric3, calories_burned, activity_type) VALUES (@user_id, @date, @duration, @metric2, @metric3, @calories_burned, 'Walking');";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", UserId);
                cmd.Parameters.AddWithValue("@date", Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@duration", Duration);
                cmd.Parameters.AddWithValue("@metric2", Metric2);
                cmd.Parameters.AddWithValue("@metric3", Metric3);
                cmd.Parameters.AddWithValue("@calories_burned", CaloriesBurned);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // --- Swimming Activity ---
        internal class SwimmingActivity : ActivityClass
        {
            // Metric2: Lap Count
            // Metric3: Avg Heart Rate

            public override double CalculateCalories()
            {
                CaloriesBurned = (Duration * 6) + (Metric2 * 5.5) + (Metric3 * 0.16);  
                return Convert.ToInt32(CaloriesBurned);
            }

            public bool Insert()
            {
                SQLiteConnection conn = new SQLiteConnection(connString);
                string sql = @"INSERT INTO tbl_activity (user_id, date, duration, metric2, metric3, calories_burned, activity_type) VALUES (@user_id, @date, @duration, @metric2, @metric3, @calories_burned, 'Swimming');";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", UserId);
                cmd.Parameters.AddWithValue("@date", Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@duration", Duration);
                cmd.Parameters.AddWithValue("@metric2", Metric2);
                cmd.Parameters.AddWithValue("@metric3", Metric3);
                cmd.Parameters.AddWithValue("@calories_burned", CaloriesBurned);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // --- Running Activity ---
        internal class RunningActivity : ActivityClass
        {
            // Metric2: Distance (km)
            // Metric3: Speed (km/h)

            public override double CalculateCalories()
            {
                CaloriesBurned = (Duration * 6.5) + (Metric2 * 40) + (Metric3 * 3);
                return Convert.ToInt32(CaloriesBurned);
            }

            public bool Insert()
            {
                SQLiteConnection conn = new SQLiteConnection(connString);
                string sql = @"INSERT INTO tbl_activity (user_id, date, duration, metric2, metric3, calories_burned, activity_type) VALUES (@user_id, @date, @duration, @metric2, @metric3, @calories_burned, 'Running');";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", UserId);
                cmd.Parameters.AddWithValue("@date", Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@duration", Duration);
                cmd.Parameters.AddWithValue("@metric2", Metric2);
                cmd.Parameters.AddWithValue("@metric3", Metric3);
                cmd.Parameters.AddWithValue("@calories_burned", CaloriesBurned);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // --- Cycling Activity ---
        internal class CyclingActivity : ActivityClass
        {
            // Metric2: Distance (km)
            // Metric3: Resistance Level

            public override double CalculateCalories()
            {
                CaloriesBurned = (Duration * 4.4) + (Metric2 * 27) + (Metric3 * 12);
                return Convert.ToInt32(CaloriesBurned);
            }

            public bool Insert()
            {
                SQLiteConnection conn = new SQLiteConnection(connString);
                string sql = @"INSERT INTO tbl_activity (user_id, date, duration, metric2, metric3, calories_burned, activity_type) VALUES (@user_id, @date, @duration, @metric2, @metric3, @calories_burned, 'Cycling');";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", UserId);
                cmd.Parameters.AddWithValue("@date", Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@duration", Duration);
                cmd.Parameters.AddWithValue("@metric2", Metric2);
                cmd.Parameters.AddWithValue("@metric3", Metric3);
                cmd.Parameters.AddWithValue("@calories_burned", CaloriesBurned);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // --- Jump Rope Activity ---
        internal class JumpRopeActivity : ActivityClass
        {
            // Metric2: Jump Count
            // Metric3: Intensity Level

            public override double CalculateCalories()
            {
                CaloriesBurned = (Duration * 6.5) + (Metric2 * 0.5) + (Metric3 * 7.5);
                return Convert.ToInt32(CaloriesBurned);
            }

            public bool Insert()
            {
                SQLiteConnection conn = new SQLiteConnection(connString);
                string sql = @"INSERT INTO tbl_activity (user_id, date, duration, metric2, metric3, calories_burned, activity_type) VALUES (@user_id, @date, @duration, @metric2, @metric3, @calories_burned, 'Jump Rope');";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", UserId);
                cmd.Parameters.AddWithValue("@date", Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@duration", Duration);
                cmd.Parameters.AddWithValue("@metric2", Metric2);
                cmd.Parameters.AddWithValue("@metric3", Metric3);
                cmd.Parameters.AddWithValue("@calories_burned", CaloriesBurned);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // --- Rowing Activity ---
        internal class RowingActivity : ActivityClass
        {
            // Metric2: Stroke Count
            // Metric3: Distance (km)

            public override double CalculateCalories()
            {
                CaloriesBurned = (Duration * 5.5) + (Metric2 * 0.5) + (Metric3 * 25);
                return Convert.ToInt32(CaloriesBurned);
            }

            public bool Insert()
            {
                SQLiteConnection conn = new SQLiteConnection(connString);
                string sql = @"INSERT INTO tbl_activity(user_id, date, duration, metric2, metric3, calories_burned, activity_type)VALUES(@user_id, @date, @duration, @metric2, @metric3, @calories_burned, 'Rowing');";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", UserId);
                cmd.Parameters.AddWithValue("@date", Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@duration", Duration);
                cmd.Parameters.AddWithValue("@metric2", Metric2);
                cmd.Parameters.AddWithValue("@metric3", Metric3);
                cmd.Parameters.AddWithValue("@calories_burned", CaloriesBurned);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
   }
