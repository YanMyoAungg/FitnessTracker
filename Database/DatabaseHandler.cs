using System;
using System.Data.SQLite;
using System.IO;

namespace FitnessTrackerSchool.Database
{
    public class DatabaseHandler
    {
        private static readonly string dbFilePath = @"..\..\Database\FitnessTracker.db";
        public static readonly string connectionString = $"Data Source={dbFilePath};Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string createUserTable = @"
                    CREATE TABLE IF NOT EXISTS tbl_user (
                        user_id    INTEGER PRIMARY KEY AUTOINCREMENT,
                        user_name  TEXT    NOT NULL,
                        password   TEXT    NOT NULL,
                        height     INTEGER,
                        weight     INTEGER,
                        birthdate  TEXT,
                        phone      TEXT,
                        address    TEXT,
                        gender     TEXT,
                        age        INTEGER
                    );";

                string createGoalTable = @"
                    CREATE TABLE IF NOT EXISTS tbl_goal (
                        goal_id         INTEGER PRIMARY KEY AUTOINCREMENT,
                        user_id         INTEGER NOT NULL,
                        target_calories REAL,
                        date       TEXT    NOT NULL,
                        is_achieved     INTEGER  NOT NULL DEFAULT 0,
                        FOREIGN KEY(user_id) REFERENCES tbl_user(user_id)
                    );";
                
                string createActivityTable = @"
                    CREATE TABLE IF NOT EXISTS tbl_activity (
                        activity_id    INTEGER PRIMARY KEY AUTOINCREMENT,
                        user_id        INTEGER NOT NULL,
                        activity_type  TEXT,
                        date           TEXT,
                        duration       INTEGER,
                        calories_burned REAL,
                        metric1        REAL,
                        metric2        REAL,
                        metric3        REAL,
                        FOREIGN KEY(user_id) REFERENCES tbl_user(user_id)
                    );";

                using (var cmd = new SQLiteCommand(createUserTable, conn)) cmd.ExecuteNonQuery();
                using (var cmd = new SQLiteCommand(createGoalTable, conn)) cmd.ExecuteNonQuery();
                using (var cmd = new SQLiteCommand(createActivityTable, conn)) cmd.ExecuteNonQuery();
            }
        }
    }
}
