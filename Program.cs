using System;
using learn_ado.Examples;

namespace learn_ado
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TimeSheets;Integrated Security=True";

            MultipleResultSets.Run(connectionString);
        }
    }
}
