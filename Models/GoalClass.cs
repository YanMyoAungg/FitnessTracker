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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace FitnessTrackerSchool.Classes
{
    internal class GoalClass
    {
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int TargetCalories { get; set; }
        public bool IsAchieved { get; set; }


        private static readonly string connString = DatabaseHandler.connString;
        public DataTable SelectByUser(int userId)
        {
            SqlConnection conn = new SqlConnection(connString);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_goal WHERE user_id = @user_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                string sql = "INSERT INTO tbl_goal (user_id, date, target_calories) VALUES (@user_id, @date, @targetCalories)";
                SqlCommand cmd = new SqlCommand(sql, conn);
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
            
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                string sql = "UPDATE tbl_goal SET is_achieved = 1 WHERE user_id = @userId AND date = @date";
                SqlCommand cmd = new SqlCommand(sql, conn);
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
            SqlConnection conn = new SqlConnection(DatabaseHandler.connString);
            const string sql = @"SELECT COUNT(*) FROM tbl_goal WHERE user_id = @userId AND date = @today";

            SqlCommand cmd = new SqlCommand(sql, conn);
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
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@date", dateString);
            conn.Open();
            
            int count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            return count > 0;
        }

        public static int GetGoalForDate(int userId, DateTime date)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            const string sql = "SELECT target_calories FROM tbl_goal WHERE user_id=@uid AND date=@date AND is_achieved=0";
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@date", dateString);
            conn.Open();
            object result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToInt32(result);
        }

        public static bool IsGoalAchieved(int userId, DateTime date)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            const string sql = @"SELECT COUNT(*) FROM tbl_goal WHERE user_id = @userId AND date = @date AND is_achieved=1";
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@date", dateString);
            conn.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            return count > 0;
        }
    }
}
