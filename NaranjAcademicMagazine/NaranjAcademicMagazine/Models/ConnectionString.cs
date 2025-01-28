using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace NaranjAcademicMagazine.Models
{
    public class ConnectionString
    {
        public static string ConString;
        public ConnectionString()
        {
            ConString = "Data Source=DESKTOP-NJBSP6T;Initial Catalog=NaranjKdruDB;Integrated Security=True";
            //ConString = "Data Source=191.96.52.2;Initial Catalog=naranjk9;Persist Security Info=true;User ID=naranjDb;Password=Root@12345@";
        }
        public void Insert(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();
            }
        }
        public DataTable Select(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
            }
            return dt;
        }

        public void Update(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }
    }
}