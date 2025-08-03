using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FitnessTrackerSchool.Database;

namespace FitnessTrackerSchool.Classes
{
    internal class GoalClass
    {
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int TargetCalories { get; set; }
        public bool IsAchieved { get; set; }
        
        private static readonly string connString = DatabaseHandler.connectionString;
        public DataTable SelectByUser(int userId)
        {
            SQLiteConnection conn = new SQLiteConnection(connString);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_goal WHERE user_id = @user_id";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public bool Insert(GoalClass goal)
        {
            bool isSuccess = false;
            SQLiteConnection conn = new SQLiteConnection(connString);
            try
            {
                string sql = "INSERT INTO tbl_goal (user_id, date, target_calories) VALUES (@user_id, @date, @targetCalories)";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", UserId);
                cmd.Parameters.AddWithValue("@date", Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@targetCalories", TargetCalories);
                conn.Open();
                int rows = cmd.ExecuteNonQuery(); 
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
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
            return isSuccess;
        }

        public bool MarkAsAchieved(DateTime date)
        {
            bool isSuccess = false;
            int userId = Session.CurrentUserId;
            string dateString = date.ToString("yyyy-MM-dd");
            
            SQLiteConnection conn = new SQLiteConnection(connString);
            try
            {
                string sql = "UPDATE tbl_goal SET is_achieved = 1 WHERE user_id = @userId AND date = @date";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@date", dateString);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        public static bool HasActiveGoal(int userId)
        {
            string today = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SQLiteConnection conn = new SQLiteConnection(DatabaseHandler.connectionString);
            const string sql = @"SELECT COUNT(*) FROM tbl_goal WHERE user_id = @userId AND is_achieved = 0 AND date = @today";

            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@today", today);
            conn.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            return count > 0;
        }

        public static bool HasGoalForDate(int userId, DateTime date)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            const string sql = @"SELECT COUNT(*) FROM tbl_goal WHERE user_id = @userId AND date = @date";
            SQLiteConnection conn = new SQLiteConnection(connString);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@date", dateString);
            conn.Open();
            
            int count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            return count > 0;
        }

        public static int GetGoalForDate(int userId, DateTime date)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            const string sql = "SELECT target_calories FROM tbl_goal WHERE user_id=@uid AND date=@date AND is_achieved=0 LIMIT 1";
            SQLiteConnection conn = new SQLiteConnection(connString);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@date", dateString);
            conn.Open();
            object result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToInt32(result);
        }
    }
}
