using System.Configuration;

namespace FitnessTrackerSchool.Classes
{
    internal static class DatabaseHandler
    {
        public static readonly string connString = ConfigurationManager.ConnectionStrings["mssqlConn"].ConnectionString;
    }
}