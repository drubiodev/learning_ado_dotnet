using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using learn_ado.Entities;
using Microsoft.Data.SqlClient;

namespace learn_ado.Examples
{
    public static class DataReaderExample
    {
        public static void Run(string cnnString)
        {
            StringBuilder sb = new StringBuilder(1024);

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Employees]", cnn))
                {
                    cnn.Open();

                    // CommandBehavior.CloseConnection (close reader also close connection)
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            sb.AppendLine($"Employee: {dr["FirstName"].ToString()}");
                        }
                    }
                }
            }

            System.Console.WriteLine(sb.ToString());
        }

        public static void RunList(string cnnString)
        {
            var emp = new List<Employee>();

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Employees]", cnn))
                {
                    cnn.Open();

                    // CommandBehavior.CloseConnection (close reader also close connection)
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            emp.Add(new Employee
                            {
                                FirstName = dr.GetString(dr.GetOrdinal("FirstName")),
                                LastName = dr.GetString(dr.GetOrdinal("LastName")),
                                CreatedDate = dr.IsDBNull(dr.GetOrdinal("CreatedDate")) ? (DateTime?)null : Convert.ToDateTime(dr["CreatedDate"])
                            });
                        }
                    }
                }
            }

            foreach (var item in emp)
            {
                System.Console.WriteLine($"{item.FirstName} {item.LastName} ({item.CreatedDate})");
            }
        }
    }
}