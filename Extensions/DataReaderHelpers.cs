using System;
using Microsoft.Data.SqlClient;

namespace learn_ado.Extensions
{
    public static class DataReaderHelpers
    {
        public static DataType GetFieldValue<DataType>(this SqlDataReader dr, string name)
        {
            DataType ret = default;

            if (!dr[name].Equals(DBNull.Value))
            {
                ret = (DataType)dr[name];
            }

            return ret;
        }
    }
}