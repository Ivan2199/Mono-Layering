using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using WebProject.Model;
using WebProject.Repository.RepositoryCommon;

namespace WebProject.Data
{
    public class DataAccess : IVehicleRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ABCD"].ToString();
        private NpgsqlConnection _connection;

        public DataAccess()
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
            InitializeDatabase();
        }

        public async Task InitializeDatabase()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = _connection;

                cmd.CommandText = "CREATE TABLE IF NOT EXISTS \"Vehicle\" (" +
                    "\"Id\" UUID PRIMARY KEY," +
                    "\"VehicleType\" VARCHAR(255)," +
                    "\"VehicleBrand\" VARCHAR(255)," +
                    "\"YearOfProduction\" INT," +
                    "\"TopSpeed\" INT," +
                    "\"VehicleMileage\" INT," +
                    "\"VehicleOwner\" VARCHAR(255))";
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<Vehicle>> GetVehicles(string vehicleType = null)
        {
            var vehicles = new List<Vehicle>();
            Vehicle currentVehicle = null;
            List<VehicleServiceHistory> currentServiceHistories = null;

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = _connection;

                var select = $"SELECT v.*, h.* FROM \"Vehicle\" v LEFT JOIN \"VehicleServiceHistory\" h ON v.\"Id\" = h.\"VehicleId\"";

                if (!string.IsNullOrWhiteSpace(vehicleType))
                {
                    cmd.Parameters.AddWithValue("@type", vehicleType);
                    cmd.CommandText = $"{select} WHERE v.\"VehicleType\" = @type ORDER BY v.\"Id\", h.\"ServiceDate\"";
                }
                else
                {
                    cmd.CommandText = $"{select} ORDER BY v.\"Id\", h.\"ServiceDate\"";
                }


                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Guid currentVehicleId = reader["Id"] != DBNull.Value ? (Guid)reader["Id"] : Guid.Empty;

                        if (currentVehicle == null || currentVehicle.Id != currentVehicleId)
                        {
                            if (currentVehicle != null)
                            {
                                currentVehicle.VehicleServiceHistory = currentServiceHistories;
                                vehicles.Add(currentVehicle);
                            }

                            currentVehicle = MapVehicleFromReader(reader);
                            currentServiceHistories = new List<VehicleServiceHistory>();
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("ServiceDate")))
                        {
                            var serviceHistory = MapVehicleServiceHistoryFromReader(reader);
                            currentServiceHistories.Add(serviceHistory);
                        }
                    }

                    if (currentVehicle != null)
                    {
                        currentVehicle.VehicleServiceHistory = currentServiceHistories;
                        vehicles.Add(currentVehicle);
                    }
                }
            }

            return vehicles;
        }

        public async Task<Vehicle> GetVehicleById(Guid id)
        {
            Vehicle vehicle = null;
            List<VehicleServiceHistory> serviceHistories = new List<VehicleServiceHistory>();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = _connection;

                cmd.CommandText = $"SELECT v.*, h.* FROM \"Vehicle\" v " +
                                  $"LEFT JOIN \"VehicleServiceHistory\" h ON v.\"Id\" = h.\"VehicleId\" " +
                                  $"WHERE v.\"Id\" = @id";
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (vehicle == null)
                        {
                            vehicle = MapVehicleFromReader(reader);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("ServiceDate")))
                        {
                            var serviceHistory = MapVehicleServiceHistoryFromReader(reader);
                            serviceHistories.Add(serviceHistory);
                        }
                    }
                }
            }

            if (vehicle != null)
            {
                vehicle.VehicleServiceHistory = serviceHistories;
            }

            return vehicle;
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = _connection;

                cmd.CommandText = "INSERT INTO \"Vehicle\" (\"Id\", \"VehicleType\", \"VehicleBrand\", \"YearOfProduction\", \"TopSpeed\", \"VehicleMileage\", \"VehicleOwner\")" +
                                  "VALUES (@id, @type, @brand, @year, @speed, @mileage, @owner)";

                Guid randid = System.Guid.NewGuid();
                cmd.Parameters.AddWithValue("@id", randid);
                AddVehicleParameters(cmd, vehicle);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateVehicle(Guid id, Vehicle updatedVehicle)
        {
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = _connection;

                cmd.CommandText = "UPDATE \"Vehicle\" SET \"VehicleType\" = @type, \"VehicleBrand\" = @brand, " +
                                  "\"YearOfProduction\" = @year, \"TopSpeed\" = @speed, \"VehicleMileage\" = @mileage, " +
                                  "\"VehicleOwner\" = @owner WHERE \"Id\" = @id";
                cmd.Parameters.AddWithValue("@id", id);
                AddVehicleParameters(cmd, updatedVehicle);

               await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteVehicle(Guid id)
        {
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = _connection;

                cmd.CommandText = "DELETE FROM \"Vehicle\" WHERE \"Id\" = @id";
                cmd.Parameters.AddWithValue("@id", id);

                await cmd.ExecuteNonQueryAsync();
            }
        }



        private void AddVehicleParameters(NpgsqlCommand cmd, Vehicle vehicle)
        {
            cmd.Parameters.AddWithValue("@type", vehicle.VehicleType);
            cmd.Parameters.AddWithValue("@brand", vehicle.VehicleBrand);
            cmd.Parameters.AddWithValue("@year", vehicle.YearOfProduction);
            cmd.Parameters.AddWithValue("@speed", vehicle.TopSpeed);
            cmd.Parameters.AddWithValue("@mileage", vehicle.VehicleMileage);
            cmd.Parameters.AddWithValue("@owner", vehicle.VehicleOwner);
        }

        private Vehicle MapVehicleFromReader(NpgsqlDataReader reader)
        {
            var vehicle = new Vehicle()
            {
                Id = reader["Id"] != DBNull.Value ? (Guid)reader["Id"] : Guid.Empty,
                VehicleType = Convert.ToString(reader["VehicleType"]),
                VehicleBrand = Convert.ToString(reader["VehicleBrand"]),
                YearOfProduction = Convert.ToInt32(reader["YearOfProduction"]),
                TopSpeed = Convert.ToInt32(reader["TopSpeed"]),
                VehicleMileage = Convert.ToInt32(reader["VehicleMileage"]),
                VehicleOwner = Convert.ToString(reader["VehicleOwner"]),
                VehicleServiceHistory = reader["ServiceDate"] != DBNull.Value ? new List<VehicleServiceHistory>
                {
                  new VehicleServiceHistory()
                  {
                      Id = (Guid)reader[8]
                  }
                } : null,
            };

            return vehicle;
        }

        private VehicleServiceHistory MapVehicleServiceHistoryFromReader(NpgsqlDataReader reader)
        {
            return new VehicleServiceHistory
            {
                Id = (Guid)reader[7],
                VehicleId = (Guid)reader["VehicleId"],
                ServiceDate = Convert.ToDateTime(reader["ServiceDate"]).Date,
                ServiceDescription = Convert.ToString(reader["ServiceDescription"]),
                ServiceCost = Convert.ToDecimal(reader["ServiceCost"]),
            };
        }
    }
}