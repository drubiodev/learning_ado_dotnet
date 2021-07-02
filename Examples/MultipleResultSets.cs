using System.Data;
using System.Collections.Generic;
using learn_ado.Entities;
using Microsoft.Data.SqlClient;
using System;

namespace learn_ado.Examples
{
    public static class MultipleResultSets
    {
        public static void Run(string cnnString)
        {
            var emp = new List<Employee>();
            var cust = new List<Customer>();

            string sql = "SELECT [FirstName],[LastName],[CreatedDate]FROM [TimeSheets].[dbo].[Employees];SELECT [CompanyName],[EmailAddress]FROM [TimeSheets].[dbo].[Customers]";

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

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
                        // move to next result set
                        dr.NextResult();

                        while (dr.Read())
                        {
                            cust.Add(new Customer
                            {
                                CompanyName = dr.GetFieldValue<string>("CompanyName"),
                                EmailAddress = dr.GetFieldValue<string>("EmailAddress")
                            });
                        }
                    }
                }
            }

            foreach (var item in emp)
            {
                System.Console.WriteLine($"{item.FirstName} {item.LastName}");
            }

            foreach (var item in cust)
            {
                System.Console.WriteLine($"{item.CompanyName} {item.EmailAddress}");
            }

        }
    }
}