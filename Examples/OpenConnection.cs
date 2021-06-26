using System.Text;
using Microsoft.Data.SqlClient;

namespace learn_ado.Examples
{
    public static class OpenConnection
    {
        public static void Run(string cnnString)
        {
            // Create SQL connection object
            SqlConnection cnn = new SqlConnection(cnnString);

            // Open connection
            cnn.Open();

            // gather conection information
            var resultText = GetConnectionInfo(cnn);

            System.Console.WriteLine(resultText);

            cnn.Close();
            cnn.Dispose();
        }

        public static void RunWithUsing(string cnnString)
        {
            // use inside using for auto closing and disposing
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();
                var resultText = GetConnectionInfo(cnn);

                System.Console.WriteLine(resultText);
            }
        }

        private static string GetConnectionInfo(SqlConnection cnn)
        {
            StringBuilder sb = new StringBuilder(1024);

            sb.AppendLine($"Connection String: {cnn.ConnectionString}");
            sb.AppendLine($"State: {cnn.State.ToString()}");
            sb.AppendLine($"Connection Timeout: {cnn.ConnectionTimeout} seconds");
            sb.AppendLine($"Database: {cnn.Database}");
            sb.AppendLine($"Data Source: {cnn.DataSource}");
            sb.AppendLine($"Server Version: {cnn.ServerVersion}");
            sb.AppendLine($"Workstation ID: {cnn.WorkstationId}");

            return sb.ToString();
        }
    }
}