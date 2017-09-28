using System;
using System.Data.SqlClient;

namespace CandyShop.Repository.Database
{
    public static class SQLExtension
    {        
        public static string ReadAsString(this SqlDataReader r, string campo)
        {
            return r.GetString(r.GetOrdinal(campo));
        }

        public static int ReadAsInt(this SqlDataReader r, string campo)
        {
            return r.GetInt32(r.GetOrdinal(campo));
        }

        public static decimal ReadAsDecimal(this SqlDataReader r, string campo)
        {
            return r.GetDecimal(r.GetOrdinal(campo));
        }


        public static decimal? ReadAsDecimalNull(this SqlDataReader r, string campo)
        {
            var ordinal = r.GetOrdinal(campo);
            return r.IsDBNull(ordinal) ? (decimal?)null : r.GetDecimal(r.GetOrdinal(campo)) ;
        }

        public static DateTime ReadAsDateTime(this SqlDataReader r, string campo)
        {
            return r.GetDateTime(r.GetOrdinal(campo));
        }                       
    }
}
