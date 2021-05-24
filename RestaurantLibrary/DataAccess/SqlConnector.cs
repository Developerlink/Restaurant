using RestaurantLibrary.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace RestaurantLibrary.DataAccess
{
    public class SqlConnector
    {
        private const string db = "RestaurantSqlHome";

        public int CreateDinnerTable(DinnerTable table)
        {
            // Name of stored procedure
            string sp = "sp_dinner_tables_insert";

            try
            {
                using (SqlConnection conn = new SqlConnection(db))
                {
                    using (SqlCommand cmd = new SqlCommand(sp, conn))
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
                        table.Id = (int)cmd.Parameters["@Id"].Value;

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return table.Id;
        }

        public Restaurant GetRestaurant(int id)
        {
            Restaurant restaurant = new Restaurant();
            string sql = "SELECT * FROM restaurants ";
            sql += "INNER JOIN streets ON streets.id=restaurants.street_id ";
            sql += "INNER JOIN cities ON cities.id=streets.city_id ";
            sql += "WHERE restaurants.Id = @Id ";

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    using (cmd)
                    {
                        try
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                restaurant.Id = id;
                                restaurant.Name = reader.GetString(2);
                                restaurant.CVR = reader.GetInt32(3);
                                restaurant.PhoneNumber = reader.GetInt32(4);
                                restaurant.Email = reader.GetString(5);
                            }
                            reader.Close();

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            restaurant.Areas = GetAreas(restaurant.Id);
            return restaurant;
        }

        private List<Area> GetAreas(int restaurantId)
        {
            List<Area> areas = new List<Area>();
            string sql = "SELECT * FROM areas WHERE restaurant_id = @id";

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = restaurantId;
                    using (cmd)
                    {
                        try
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Area area = new Area();
                                area.Id = reader.GetInt32(0);
                                area.RestaurantId = reader.GetInt32(1);
                                area.Name = reader.GetString(2);
                                areas.Add(area);
                            }
                            reader.Close();

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            foreach (var area in areas)
            {
                area.DinnerTables = GetTables(area.Id);
            }

            return areas;
        }

        private List<DinnerTable> GetTables(int areaId)
        {
            List<DinnerTable> tables = new List<DinnerTable>();
            string sql = $"SELECT * FROM dinner_tables WHERE area_id = @id";

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = areaId;
                    using (cmd)
                    {
                        try
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                DinnerTable table = new DinnerTable();
                                table.Id = reader.GetInt32(0);
                                table.AreaId = reader.GetInt32(1);
                                table.Name = reader.GetString(2);
                                table.Seats = reader.GetInt32(3);
                                tables.Add(table);
                            }
                            reader.Close();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return tables;
        }

        public List<ArrivalStatus> GetArrivalStatuses()
        {
            List<ArrivalStatus> arrivalStatuses = new List<ArrivalStatus>();
            string sql = "SELECT * FROM arrival_statuses ORDER BY id";

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        try
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                ArrivalStatus arrivalStatus = new ArrivalStatus();
                                arrivalStatus.Id = reader.GetInt32(0);
                                arrivalStatus.Status = reader.GetString(1);
                                arrivalStatuses.Add(arrivalStatus);
                            }
                            reader.Close();

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return arrivalStatuses;
        }

        public List<Reservation> GetReservations(DateTime selectedDate, Area selectedArea)
        {
            List<Reservation> reservations = new List<Reservation>();
            // Name of stored procedure
            string sp = "sp_reservations_get_for_date_area";

            try
            {
                using (SqlConnection conn = new SqlConnection(db))
                {
                    SqlCommand cmd = new SqlCommand(sp, conn);
                    cmd.Parameters.Add(new SqlParameter("@Date", selectedDate));
                    cmd.Parameters.Add(new SqlParameter("@AreaId", selectedArea.Id));
                    using (cmd)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        try
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Reservation reservation = new Reservation();

                            }
                            reader.Close();

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return table.Id;

            return reservations;
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
