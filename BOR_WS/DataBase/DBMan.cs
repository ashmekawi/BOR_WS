using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BOR_WS.DataBase
{
    public class DBMan
    {

        string connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["BOR"].ConnectionString;

        public SqlConnection databaseConnection;

        public void openDatabaseConnection()
        {
            try
            {
                databaseConnection = new SqlConnection(connetionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in open Connection :" + ex.Message);
                throw ex;
            }
        }


        public void closeDatabaseConnection()
        {
            try
            {
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in close Connection :" + ex.Message);
                throw ex;

            }
        }

        public DataSet executeQuery(string query)
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (SqlCommand sqlCommand = new SqlCommand(query, databaseConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    adapter.Fill(dataSet);
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet executeSqlCommand(SqlCommand sqlCommand)
        {
            try
            {
                DataSet dataSet = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}