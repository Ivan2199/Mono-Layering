using System;
using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using WebProject.Model;

namespace WebProject.Data
{
    public class DataAccessVehicleServiceHistory
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ABCD"].ToString();

        public DataAccessVehicleServiceHistory() { }
        public void InitializeDatabase()
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS VehicleServiceHistory (" +
                        "Id SERIAL PRIMARY KEY," +
                        "VehicleId INT," +
                        "ServiceDate DATE," +
                        "ServiceDescription VARCHAR(255)," +
                        "ServiceCost DECIMAL," +
                        "FOREIGN KEY (vehicleid) REFERENCES vehicle(id))";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<VehicleServiceHistory> GetVehicleHistoryServices()
        {
            List<VehicleServiceHistory> vehicleServiceHistories = new List<VehicleServiceHistory>();

            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "SELECT * FROM VehicleServiceHistory";

                    using (var reader = cmd.ExecuteReader())
                    {
                       vehicleServiceHistories = MapVehicleServiceHistoryFromReader(reader); 
                    }
                }
            }

            return vehicleServiceHistories;
        }

        public List<VehicleServiceHistory> GetVehicleServiceHistoryById(int id)
        {
            List<VehicleServiceHistory> vehicleServiceHistories = new List<VehicleServiceHistory>();
            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "SELECT * FROM VehicleServiceHistory WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        vehicleServiceHistories = MapVehicleServiceHistoryFromReader(reader);
                    }
                }
            }

            return vehicleServiceHistories;
        }

        public void AddVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "INSERT INTO VehicleServiceHistory (VehicleId, ServiceDate, ServiceDescription, ServiceCost)" +
                                      "VALUES (@vehicleId, @serviceDate, @serviceDescription, @serviceCost)";
                    AddVehicleServiceHistoryParameters(cmd, vehicleServiceHistory);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "UPDATE VehicleServiceHistory SET ServiceDate = @serviceDate, " +
                                      "ServiceDescription = @serviceDescription, ServiceCost = @serviceCost " +
                                      "WHERE Id = @id";
                    AddVehicleServiceHistoryParameters(cmd, vehicleServiceHistory);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteVehicleServiceHistory(int id)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "DELETE FROM VehicleServiceHistory WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private List<VehicleServiceHistory> MapVehicleServiceHistoryFromReader(NpgsqlDataReader reader)
        {
            if (reader == null || !reader.HasRows)
            {
                return new List<VehicleServiceHistory>();
            }

            List<VehicleServiceHistory> serviceHistoryList = new List<VehicleServiceHistory>();

            while (reader.Read())
            {
                var vehicleServiceHistory = new VehicleServiceHistory
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    VehicleId = Convert.ToInt32(reader["VehicleId"]),
                    ServiceDate = Convert.ToDateTime(reader["ServiceDate"]).Date,
                    ServiceDescription = Convert.ToString(reader["ServiceDescription"]),
                    ServiceCost = Convert.ToDecimal(reader["ServiceCost"]),
                };

                serviceHistoryList.Add(vehicleServiceHistory);
            }

            return serviceHistoryList;
        }


        private void AddVehicleServiceHistoryParameters(NpgsqlCommand cmd, VehicleServiceHistory vehicleServiceHistory)
        {
            cmd.Parameters.AddWithValue("@vehicleId", vehicleServiceHistory.VehicleId);
            cmd.Parameters.AddWithValue("@serviceDate", vehicleServiceHistory.ServiceDate);
            cmd.Parameters.AddWithValue("@serviceDescription", vehicleServiceHistory.ServiceDescription);
            cmd.Parameters.AddWithValue("@serviceCost", vehicleServiceHistory.ServiceCost);
        }
    }
}
