using System;
using System.Collections.Generic;
using System.Data;
using learn_ado.Entities;
using Microsoft.Data.SqlClient;

namespace learn_ado.Examples
{
    public static class GetFieldValueExample
    {
        public static void Run(string cnnString)
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
                                FirstName = dr.GetFieldValue<string>(dr.GetOrdinal("FirstName")),
                                LastName = dr.GetFieldValue<string>(dr.GetOrdinal("LastName")),
                                // GetFieldValue does not work on nullable fields
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

        public static void RunWithExtension(string cnnString)
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
                                FirstName = dr.GetFieldValue<string>("FirstName"),
                                LastName = dr.GetFieldValue<string>("LastName"),
                                CreatedDate = dr.GetFieldValue<DateTime?>("CreatedDate")
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