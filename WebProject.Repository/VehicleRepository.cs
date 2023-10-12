using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using WebProject.Common;
using WebProject.Model;
using WebProject.Model.Common;
using WebProject.Repository.Common;

namespace WebProject.Data
{
    public class DataAccessVehicleRepository : IVehicleRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ABCD"].ToString();
        private NpgsqlConnection _connection;

        public DataAccessVehicleRepository() { }

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
        }

        public async Task<List<IVehicle>> GetVehiclesAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            var vehiclesDict = new Dictionary<Guid, IVehicle>();

            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;
                    

                    cmd.CommandText = Query(cmd, filtering, paging, sorting);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            IVehicle currentVehicle;

                            Guid currentVehicleId = reader.GetGuid(reader.GetOrdinal("Id"));

                            if (!vehiclesDict.TryGetValue(currentVehicleId, out currentVehicle))
                            {
                                currentVehicle = MapVehicleFromReader(reader);
                                vehiclesDict.Add(currentVehicleId, currentVehicle);
                            }

                            if (!reader.IsDBNull(7))
                            {
                                var serviceHistory = MapVehicleServiceHistoryFromReader(reader);
                                currentVehicle.VehicleServiceHistory.Add(serviceHistory);
                            }
                        }
                    }
                }
            }
            return vehiclesDict.Values.ToList();
        }


        public async Task<IVehicle> GetVehicleByIdAsync(Guid id)
        {
            IVehicle vehicle = null;
            List<IVehicleServiceHistory> serviceHistories = new List<IVehicleServiceHistory>();
            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

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

                            if (!reader.IsDBNull(7))
                            {
                                var serviceHistory = MapVehicleServiceHistoryFromReader(reader);
                                serviceHistories.Add(serviceHistory);
                            }
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

        public async Task AddVehicleAsync(IVehicle vehicle)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "INSERT INTO \"Vehicle\" (\"Id\", \"VehicleType\", \"VehicleBrand\", \"YearOfProduction\", \"TopSpeed\", \"VehicleMileage\", \"VehicleOwner\")" +
                                      "VALUES (@id, @type, @brand, @year, @speed, @mileage, @owner)";

                    Guid randid = System.Guid.NewGuid();
                    cmd.Parameters.AddWithValue("@id", randid);
                    AddVehicleParameters(cmd, vehicle);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateVehicleAsync(Guid id, IVehicle updatedVehicle)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    var sqlBuilder = new StringBuilder("UPDATE \"Vehicle\" SET ");


                    if (updatedVehicle.VehicleType != null)
                    {
                        sqlBuilder.Append("\"VehicleType\" = @serviceType, ");
                        cmd.Parameters.AddWithValue("@serviceType", updatedVehicle.VehicleType);
                    }

                    if (!string.IsNullOrEmpty(updatedVehicle.VehicleBrand))
                    {
                        sqlBuilder.Append("\"VehicleBrand\" = @vehicleBrand, ");
                        cmd.Parameters.AddWithValue("@vehicleBrand", updatedVehicle.VehicleBrand);
                    }
                    if (updatedVehicle.VehicleMileage > 0)
                    {
                        sqlBuilder.Append("\"VehicleMileage\" = @vehicleMileage, ");
                        cmd.Parameters.AddWithValue("@vehicleMileage", updatedVehicle.VehicleMileage);
                    }
                    if (updatedVehicle.YearOfProduction > 0)
                    {
                        sqlBuilder.Append("\"YearOfProduction\" = @yearOfProduction, ");
                        cmd.Parameters.AddWithValue("@yearOfProduction", updatedVehicle.YearOfProduction);
                    }
                    if (updatedVehicle.VehicleOwner != null)
                    {
                        sqlBuilder.Append("\"VehicleOwner\" = @vehicleOwner, ");
                        cmd.Parameters.AddWithValue("@vehicleOwner", updatedVehicle.YearOfProduction);
                    }

                    sqlBuilder.Length -= 2;

                    sqlBuilder.Append(" WHERE \"Id\" = @id");
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.CommandText = sqlBuilder.ToString();

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteVehicleAsync(Guid id)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    cmd.CommandText = "DELETE FROM \"VehicleServiceHistory\" WHERE \"VehicleId\" = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.ExecuteNonQueryAsync();

                    cmd.CommandText = "DELETE FROM \"Vehicle\" WHERE \"Id\" = @id";
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }



        private void AddWithParametersPagingSortingFiltering(NpgsqlCommand cmd, Filtering filtering, Paging paging, Sorting sorting)
        {
            cmd.Parameters.AddWithValue("@vehicleType", filtering.VehicleType);
            cmd.Parameters.AddWithValue("@vehicleBrand", filtering.VehicleBrand);
            cmd.Parameters.AddWithValue("@mileageMin", filtering.MileageMin);
            cmd.Parameters.AddWithValue("@mileageMax", filtering.MileageMax);

            cmd.Parameters.AddWithValue("@pageNumber", paging.PageNumber);
            cmd.Parameters.AddWithValue("@pageSize", paging.PageSize);

            cmd.Parameters.AddWithValue("@orderBy", sorting.OrderBy);
            cmd.Parameters.AddWithValue("@sortOrder", sorting.SortOrder);
        }
        private void AddVehicleParameters(NpgsqlCommand cmd, IVehicle vehicle)
        {
            cmd.Parameters.AddWithValue("@type", vehicle.VehicleType);
            cmd.Parameters.AddWithValue("@brand", vehicle.VehicleBrand);
            cmd.Parameters.AddWithValue("@year", vehicle.YearOfProduction);
            cmd.Parameters.AddWithValue("@speed", vehicle.TopSpeed);
            cmd.Parameters.AddWithValue("@mileage", vehicle.VehicleMileage);
            cmd.Parameters.AddWithValue("@owner", vehicle.VehicleOwner);
        }

        private IVehicle MapVehicleFromReader(NpgsqlDataReader reader)
        {
            return new Vehicle()
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                VehicleType = Convert.ToString(reader["VehicleType"]),
                VehicleBrand = Convert.ToString(reader["VehicleBrand"]),
                YearOfProduction = Convert.ToInt32(reader["YearOfProduction"]),
                TopSpeed = Convert.ToInt32(reader["TopSpeed"]),
                VehicleMileage = Convert.ToInt32(reader["VehicleMileage"]),
                VehicleOwner = Convert.ToString(reader["VehicleOwner"]),
                VehicleServiceHistory = new List<IVehicleServiceHistory>(),
            };
        }

        private IVehicleServiceHistory MapVehicleServiceHistoryFromReader(NpgsqlDataReader reader)
        {
            return new VehicleServiceHistory
            {
                Id = reader.GetGuid(7),
                VehicleId = reader.GetGuid(reader.GetOrdinal("VehicleId")),
                ServiceDate = Convert.ToDateTime(reader["ServiceDate"]).Date,
                ServiceDescription = Convert.ToString(reader["ServiceDescription"]),
                ServiceCost = Convert.ToDecimal(reader["ServiceCost"]),
            };
        }

        private string Query(NpgsqlCommand cmd, Filtering filtering, Paging paging, Sorting sorting)
        {
            AddWithParametersPagingSortingFiltering(cmd, filtering, paging, sorting);

            var sqlQuery = new StringBuilder("SELECT v.*, h.* FROM \"Vehicle\" v LEFT JOIN \"VehicleServiceHistory\" h ON v.\"Id\" = h.\"VehicleId\" WHERE 1 = 1");

            if (!string.IsNullOrEmpty(filtering.VehicleType))
            {
                sqlQuery.Append(" AND v.\"VehicleType\" = @vehicleType");
            }

            if (!string.IsNullOrEmpty(filtering.VehicleBrand))
            {
                sqlQuery.Append(" AND v.\"VehicleBrand\" = @vehicleBrand");
            }

            if (filtering.MileageMin > 0 && filtering.MileageMax > 0)
            {
                sqlQuery.Append(" AND v.\"VehicleMileage\" BETWEEN @mileageMin AND @mileageMax");
            }
            else if (filtering.MileageMin > 0 || filtering.MileageMax > 0)
            {
                if (filtering.MileageMin > 0 && filtering.MileageMax == 0)
                {
                    sqlQuery.Append(" AND v.\"VehicleMileage\" >= @mileageMin");
                }
                else if (filtering.MileageMin == 0 && filtering.MileageMax > 0)
                {
                    sqlQuery.Append(" AND v.\"VehicleMileage\" <= @mileageMax");
                }
            }

            sqlQuery.Append($" ORDER BY v.\"{sorting.OrderBy}\" {(sorting.SortOrder.ToUpper() == "DESC" ? "DESC" : "ASC")}");

            sqlQuery.Append($" OFFSET {(paging.PageNumber - 1) * paging.PageSize} LIMIT {paging.PageSize}");

            return sqlQuery.ToString();
        }

    }
}