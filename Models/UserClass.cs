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
    internal class UserClass
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string  Password{ get; set; }

        public int Height { get; set; }
        public int Weight { get; set; }
        public string Birthdate { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        private static readonly string connString = DatabaseHandler.connectionString;

        public DataTable Select()
        {
            SQLiteConnection conn = new SQLiteConnection(connString);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT user_id FROM tbl_user WHERE user_name = @username AND password = @password";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
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

        public bool Insert(UserClass user)
        {
            bool isSuccess = false;
            SQLiteConnection conn = new SQLiteConnection(connString);
            try
            {
                string sql = "INSERT INTO tbl_user (user_name, password, height, weight, birthdate, phone, address, gender, age) VALUES (@user_name, @password, @height, @weight, @birthdate, @phone, @address, @gender, @age)";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_name", user.UserName);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@height", user.Height);
                cmd.Parameters.AddWithValue("@weight", user.Weight);
                cmd.Parameters.AddWithValue("@birthdate", user.Birthdate);
                cmd.Parameters.AddWithValue("@phone", user.Phone);
                cmd.Parameters.AddWithValue("@address", user.Address);
                cmd.Parameters.AddWithValue("@gender", user.Gender);
                cmd.Parameters.AddWithValue("@age", user.Age);

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
        
        public bool Login(string username, string password)
        {
            bool isSuccess = false;
            SQLiteConnection conn = new SQLiteConnection(connString);

            try
            {
                string sql = "SELECT user_id FROM tbl_user WHERE user_name = @username AND password = @password";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    Session.CurrentUserId = Convert.ToInt32(result);
                    isSuccess = true;
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
      
        public static int getWeight(int userId)
        {
            const string sql = "SELECT weight FROM tbl_user WHERE user_id = @userId";
            SQLiteConnection conn = new SQLiteConnection(connString);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", userId);
            conn.Open();
            object result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToInt32(result);
        }

    }
}
