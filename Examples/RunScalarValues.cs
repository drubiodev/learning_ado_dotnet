using System.Data;
using Microsoft.Data.SqlClient;

namespace learn_ado.Examples
{
    public static class RunScalarValues
    {
        public static void Get(string cnnString)
        {
            string sql = "SELECT COUNT(*) FROM Employees";
            int RowsAffected;
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cnn.Open();
                    // Execute command
                    RowsAffected = (int)cmd.ExecuteScalar();
                }
            }

            System.Console.WriteLine($"Rows Affected: {RowsAffected.ToString()}");

        }
        public static void GetUsingParameters(string cnnString)
        {
            string sql = "SELECT COUNT(*) FROM dbo.Employees WHERE LastName = @LastName";
            int RowsAffected = 0;

            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@LastName", "Rubio"));

                        cnn.Open();

                        RowsAffected = (int)cmd.ExecuteScalar();
                    }
                }

                System.Console.WriteLine($"Rows Affected: {RowsAffected.ToString()}");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
        public static void Insert(string cnnString)
        {
            string sql = "INSERT INTO dbo.Employees (FirstName, LastName, EmailAddress, PayRate, BillRate) VALUES ('Alvaro', 'Rubio', 'ar@dr.com', '120', DEFAULT)";
            int RowsAffected = 0;

            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cnn.Open();
                        // Execute command
                        RowsAffected = cmd.ExecuteNonQuery();

                        System.Console.WriteLine($"Rows Affected: {RowsAffected.ToString()}");
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
        public static void InsertUsingParameters(string cnnString)
        {
            string sql = "INSERT INTO dbo.Employees (FirstName, LastName, EmailAddress, PayRate, BillRate) VALUES (@FirstName, @LastName, @Email, '120', DEFAULT)";
            int RowsAffected = 0;

            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@FirstName", "Jessica"));
                        cmd.Parameters.Add(new SqlParameter("@LastName", "Rubio"));
                        cmd.Parameters.Add(new SqlParameter("@Email", "jr@jr.com"));

                        cmd.CommandType = CommandType.Text;
                        cnn.Open();

                        RowsAffected = cmd.ExecuteNonQuery();
                        System.Console.WriteLine($"Rows Affected: {RowsAffected.ToString()}");
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
        public static void InsertUsingProductOutputParameters(string cnnString)
        {
            string sql = "Employee_Insert";
            int RowsAffected = 0;
            int employeeId = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@FirstName", "Lilly"));
                        cmd.Parameters.Add(new SqlParameter("@LastName", "Rubio"));
                        cmd.Parameters.Add(new SqlParameter("@Email", "lr@lr.com"));

                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@EmployeeId", Value = employeeId, IsNullable = false, DbType = DbType.Int32, Direction = ParameterDirection.Output });
                        cmd.CommandType = CommandType.StoredProcedure;
                        cnn.Open();

                        RowsAffected = cmd.ExecuteNonQuery();
                        employeeId = (int)cmd.Parameters["@EmployeeId"].Value;

                        System.Console.WriteLine($"Rows Affected: {RowsAffected.ToString()}");
                        System.Console.WriteLine($"ID: {employeeId.ToString()}");
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}