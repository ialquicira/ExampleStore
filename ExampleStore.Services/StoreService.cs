using ExampleStore.Data;
using ExampleStore.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ExampleStore.Services
{
    public class StoreService : IStoreService
    {
        private readonly ISqlClientConnectionBD _sqlStoreConnectionBD;
        public StoreService(ISqlClientConnectionBD sqlStoreConnectionBD)
        {
            _sqlStoreConnectionBD = sqlStoreConnectionBD;
        }

        public void CreateStore(Store domain)
        {
            using (SqlConnection connection = new SqlConnection(_sqlStoreConnectionBD.GetConnection()))
            {
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"INSERT INTO Stores 
                                                (StoreName
                                                ,Address)
                                                VALUES
                                                (@StoreName
                                                ,@Address)";
                        command.Parameters.AddWithValue("@Name", domain.StoreName);
                        command.Parameters.AddWithValue("@Address", domain.Address);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (SqlException ex)
                {
                    //Guardar la excepcion en algun log de errores
                    //ex
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeleteStore(int StoreId)
        {
            string strQuery = @"DELETE FROM Stores
                                WHERE StoreId = {0}";
            strQuery = string.Format(strQuery, StoreId);
            using (SqlConnection connection = new SqlConnection(_sqlStoreConnectionBD.GetConnection()))
            {
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = strQuery;


                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (SqlException ex)
                {
                    //Guardar la excepcion en algun log de errores
                    //ex
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Store> GetAllStores()
        {
            List<Store> modelList = new List<Store>();
            string strQuery = @"SELECT
                                *
                                FROM Stores";
            using (SqlConnection connection = new SqlConnection(_sqlStoreConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Store domain = new Store();
                            domain.StoreId = int.Parse(reader["StoreId"].ToString());
                            domain.StoreName = reader["StoreName"].ToString();
                            domain.Address = reader["Address"].ToString();
                            modelList.Add(domain);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    //Guardar la excepcion en algun log de errores
                    //ex
                }
                finally
                {
                    connection.Close();
                }
            return modelList;
        }

        public Store GetStoreById(int StoreId)
        {
            List<Store> modelList = new List<Store>();
            string strQuery = @"SELECT
                                *
                                FROM Stores
                               WHERE StoreId = {0}";
            strQuery = string.Format(strQuery, StoreId);
            using (SqlConnection connection = new SqlConnection(_sqlStoreConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Store domain = new Store();
                            domain.StoreId = int.Parse(reader["StoreId"].ToString());
                            domain.StoreName = reader["StoreName"].ToString();
                            domain.Address = reader["Address"].ToString();
                            modelList.Add(domain);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    //Guardar la excepcion en algun log de errores
                    //ex
                }
                finally
                {
                    connection.Close();
                }
            return modelList.Count() > 0 ? modelList.FirstOrDefault() : new Store();
        }
        public Store GetStoreByFilter(string filter)
        {
            List<Store> modelList = new List<Store>();
            string strQuery = @"SELECT * FROM Stores
                                WHERE StoreName LIKE '%{0}%'";
            strQuery = string.Format(strQuery, filter, filter);
            using (SqlConnection connection = new SqlConnection(_sqlStoreConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Store domain = new Store();
                            domain.StoreId = int.Parse(reader["StoreId"].ToString());
                            domain.StoreName = reader["StoreName"].ToString();
                            domain.Address = reader["Address"].ToString();
                            modelList.Add(domain);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    //Guardar la excepcion en algun log de errores
                    //ex
                }
                finally
                {
                    connection.Close();
                }
            return modelList.Count() > 0 ? modelList.FirstOrDefault() : new Store();
        }

        public void UpdateStore(Store domain)
        {
            using (SqlConnection connection = new SqlConnection(_sqlStoreConnectionBD.GetConnection()))
            {
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"UPDATE Stores
                                        SET 
                                        Name = COALESCE(@StoreName, Name),
                                        Description = COALESCE(@Address, Address)
                                        WHERE StoreId = @StoreId";



                        command.Parameters.AddWithValue("@Name", domain.StoreName);
                        command.Parameters.AddWithValue("@Address", domain.Address);
                        command.Parameters.AddWithValue("@StoreId", domain.StoreId);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (SqlException ex)
                {
                    //Guardar la excepcion en algun log de errores
                    //ex
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}

