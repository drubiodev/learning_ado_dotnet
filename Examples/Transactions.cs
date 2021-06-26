using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace learn_ado.Examples
{
    public static class Transactions
    {
        public static void Run(string cnnString)
        {
            string sql = "INSERT INTO dbo.Employees (FirstName, LastName, EmailAddress, PayRate, BillRate) VALUES (@FirstName, @LastName, @Email, '120', DEFAULT)";
            int RowsAffected = 0;

            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    // start transaction
                    cnn.Open();
                    using (SqlTransaction trn = cnn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, cnn))
                            {
                                // make command object part of the transaction
                                cmd.Transaction = trn;

                                cmd.Parameters.Add(new SqlParameter("@FirstName", "Not Jessica"));
                                cmd.Parameters.Add(new SqlParameter("@LastName", "Rubio"));
                                cmd.Parameters.Add(new SqlParameter("@Email", "jr@jr.com"));

                                cmd.CommandType = CommandType.Text;

                                RowsAffected = cmd.ExecuteNonQuery();
                                System.Console.WriteLine($"Rows Affected: {RowsAffected.ToString()}");

                                // Second statment to execute
                                sql = "INSERT INTO dbo.Customersd (CompanyName, PhoneNumber, EmailAddress) VALUES(@CustomerName, '555-555-5555', 'google@gmail.com')";

                                cmd.CommandText = sql;
                                cmd.Parameters.Clear();

                                cmd.Parameters.Add(new SqlParameter("@CustomerName", "Google"));

                                RowsAffected = cmd.ExecuteNonQuery();

                                System.Console.WriteLine($"Rows Affected: {RowsAffected.ToString()}");
                                trn.Commit();
                            }
                        }
                        catch (System.Exception ex)
                        {

                            trn.Rollback();
                            System.Console.WriteLine($"Transaction rolled back: {Environment.NewLine} {ex.ToString()}");
                        }
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