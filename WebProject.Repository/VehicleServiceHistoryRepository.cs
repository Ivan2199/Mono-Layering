﻿using System;
using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using WebProject.Model;
using System.Threading.Tasks;
using WebProject.Repository.RepositoryCommon;

namespace WebProject.Data
{
    public class DataAccessVehicleServiceHistory : IVehicleServiceHistoryRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ABCD"].ToString();

        public DataAccessVehicleServiceHistory() { }
        public async Task InitializeDatabase()
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS \"VehicleServiceHistory\" (" +
                        "\"Id\" UUID PRIMARY KEY," +
                        "\"VehicleId\" UUID," +
                        "\"ServiceDate\" DATE," +
                        "\"ServiceDescription\" VARCHAR(255)," +
                        "\"ServiceCost\" DECIMAL," +
                        "FOREIGN KEY (\"VehicleId\") REFERENCES \"Vehicle\"(\"Id\"))";
                   await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<VehicleServiceHistory>> GetVehicleHistoryServices()
        {
            List<VehicleServiceHistory> vehicleServiceHistories = new List<VehicleServiceHistory>();

            using (var con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "SELECT * FROM \"VehicleServiceHistory\"";

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                       vehicleServiceHistories = MapVehicleServiceHistoryFromReader(reader); 
                    }
                }
            }

            return vehicleServiceHistories;
        }

        public async Task<List<VehicleServiceHistory>> GetVehicleServiceHistoryById(Guid id)
        {
            List<VehicleServiceHistory> vehicleServiceHistories = new List<VehicleServiceHistory>();
            using (var con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "SELECT * FROM \"VehicleServiceHistory\" WHERE \"Id\" = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        vehicleServiceHistories = MapVehicleServiceHistoryFromReader(reader);
                    }
                }
            }

            return vehicleServiceHistories;
        }

        public async Task AddVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "INSERT INTO \"VehicleServiceHistory\" (\"Id\", \"VehicleId\", \"ServiceDate\", \"ServiceDescription\", \"ServiceCost\")" +
                                      "VALUES (@id, @vehicleId, @serviceDate, @serviceDescription, @serviceCost)";

                    Guid randId = System.Guid.NewGuid();
                    cmd.Parameters.AddWithValue("@id", randId);
                    AddVehicleServiceHistoryParameters(cmd, vehicleServiceHistory);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateVehicleServiceHistory(Guid id, VehicleServiceHistory vehicleServiceHistory)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandText = "UPDATE \"VehicleServiceHistory\" SET \"ServiceDate\" = @serviceDate, " +
                                      "\"ServiceDescription\" = @serviceDescription, \"ServiceCost\" = @serviceCost " +
                                      "WHERE \"Id\" = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    AddVehicleServiceHistoryParameters(cmd, vehicleServiceHistory);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteVehicleServiceHistory(Guid id)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "DELETE FROM \"VehicleServiceHistory\" WHERE \"Id\" = @id";
                    cmd.Parameters.AddWithValue("@Id", id);

                    await cmd.ExecuteNonQueryAsync();
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
                    Id = (Guid)reader["Id"],
                    VehicleId = (Guid)reader["VehicleId"],
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
