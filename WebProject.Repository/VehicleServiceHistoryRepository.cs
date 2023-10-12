using System;
using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using WebProject.Model;
using System.Threading.Tasks;
using WebProject.Repository.Common;
using WebProject.Model.Common;
using System.Data.Common;
using System.Data;
using System.Text;

namespace WebProject.Data
{
    public class DataAccessVehicleServiceHistory : IVehicleServiceHistoryRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ABCD"].ToString();

        public DataAccessVehicleServiceHistory() {
        }
        public async Task InitializeDatabaseAsync()
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

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

        public async Task<List<IVehicleServiceHistory>> GetVehicleHistoryServices()
        {
            List<IVehicleServiceHistory> vehicleServiceHistories = new List<IVehicleServiceHistory>();

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

        public async Task<List<IVehicleServiceHistory>> GetVehicleServiceHistoryById(Guid id)
        {
            List<IVehicleServiceHistory> vehicleServiceHistories = new List<IVehicleServiceHistory>();
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

        public async Task AddVehicleServiceHistory(IVehicleServiceHistory vehicleServiceHistory)
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

        public async Task UpdateVehicleServiceHistory(Guid id, IVehicleServiceHistory vehicleServiceHistory)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    var sqlBuilder = new StringBuilder("UPDATE \"VehicleServiceHistory\" SET ");


                    if (vehicleServiceHistory.ServiceDate != null)
                    {
                        sqlBuilder.Append("\"ServiceDate\" = @serviceDate, ");
                        cmd.Parameters.AddWithValue("@serviceDate", vehicleServiceHistory.ServiceDate);
                    }

                    if (!string.IsNullOrEmpty(vehicleServiceHistory.ServiceDescription))
                    {
                        sqlBuilder.Append("\"ServiceDescription\" = @serviceDescription, ");
                        cmd.Parameters.AddWithValue("@serviceDescription", vehicleServiceHistory.ServiceDescription);
                    }

                    if (vehicleServiceHistory.ServiceCost > 0)
                    {
                        sqlBuilder.Append("\"ServiceCost\" = @serviceCost, ");
                        cmd.Parameters.AddWithValue("@serviceCost", vehicleServiceHistory.ServiceCost);
                    }

                    sqlBuilder.Length -= 2;

                    sqlBuilder.Append(" WHERE \"Id\" = @id");
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.CommandText = sqlBuilder.ToString();

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

        private List<IVehicleServiceHistory> MapVehicleServiceHistoryFromReader(NpgsqlDataReader reader)
        {
            if (reader == null || !reader.HasRows)
            {
                return new List<IVehicleServiceHistory>();
            }

            List<IVehicleServiceHistory> serviceHistoryList = new List<IVehicleServiceHistory>();

            while (reader.Read())
            {
                IVehicleServiceHistory vehicleServiceHistory = new VehicleServiceHistory
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



        private void AddVehicleServiceHistoryParameters(NpgsqlCommand cmd, IVehicleServiceHistory vehicleServiceHistory)
        {
            cmd.Parameters.AddWithValue("@vehicleId", vehicleServiceHistory.VehicleId);
            cmd.Parameters.AddWithValue("@serviceDate", vehicleServiceHistory.ServiceDate);
            cmd.Parameters.AddWithValue("@serviceDescription", vehicleServiceHistory.ServiceDescription);
            cmd.Parameters.AddWithValue("@serviceCost", vehicleServiceHistory.ServiceCost);
        }
    }
}
