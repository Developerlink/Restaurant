using RestaurantLibrary.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.DataAccess
{
    public class SqlConnector
    {
        private const string db = "RestaurantSqlLocal";

        public int CreateDinnerTable(DinnerTable table)
        {
            // Name of stored procedure
            string sql = "sp_dinner_tables_insert";
            int tblId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Create INPUT parameters
                        cmd.Parameters.Add(new SqlParameter("@AreaId", table.AreaId));
                        cmd.Parameters.Add(new SqlParameter("@Name", table.Name));
                        cmd.Parameters.Add(new SqlParameter("@Seats", table.Seats));

                        // Create OUTPUT parameter
                        cmd.Parameters.Add(new SqlParameter("@Id", table.Id));
                        cmd.Parameters["@Id"].Direction = ParameterDirection.Output;


                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        tblId = (int)cmd.Parameters["@Id"].Value;

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return tblId;
        }


        public List<ArrivalStatus> GetArrivalStatuses()
        {
            string sql = "SELECT * FROM arrival_statuses ORDER BY id";
            List<ArrivalStatus> arrivalStatuses = new List<ArrivalStatus>();
            ArrivalStatus arrivalStatus = new ArrivalStatus();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return arrivalStatuses;
        }

        protected virtual string GetConnectionInformation(SqlConnection cnn)
        {
            StringBuilder sb = new StringBuilder(1024);

            sb.AppendLine("Connection String: " + cnn.ConnectionString);
            sb.AppendLine("State: " + cnn.State.ToString());
            sb.AppendLine("Connection Timeout: " + cnn.ConnectionTimeout.ToString());
            sb.AppendLine("Database: " + cnn.Database);
            sb.AppendLine("Data Source: " + cnn.DataSource);
            sb.AppendLine("Server Version: " + cnn.ServerVersion);
            sb.AppendLine("Workstation ID: " + cnn.WorkstationId);

            return sb.ToString();
        }


    }
}
