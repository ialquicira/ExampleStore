using ExampleStore.Data;
using ExampleStore.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ExampleStore.Services
{
    public class ClientService : IClientService
    {
        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public ClientService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }

        public void CreateClient(Client domain)
        {
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
            {
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"INSERT INTO Clients 
                                                (Name
                                                ,LastName
                                                ,Address)
                                                VALUES
                                                (@Name
                                                ,@LastName
                                                ,@Address)";
                        command.Parameters.AddWithValue("@Name", domain.Name);
                        command.Parameters.AddWithValue("@LastName", domain.LastName);
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

        public void DeleteClient(int ClientId)
        {
            string strQuery = @"DELETE FROM Clients
                                WHERE ClientId = {0}";
            strQuery = string.Format(strQuery, ClientId);
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
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

        public List<Client> GetAllClients()
        {
            List<Client> modelList = new List<Client>();
            string strQuery = @"SELECT
                                *
                                FROM Clients";
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Client domain = new Client();
                            domain.ClientId = int.Parse(reader["ClientId"].ToString());
                            domain.Name = reader["Name"].ToString();
                            domain.LastName = reader["LastName"].ToString();
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

        public Client GetClientById(int ClientId)
        {
            List<Client> modelList = new List<Client>();
            string strQuery = @"SELECT
                                *
                                FROM Clients
                               WHERE ClientId = {0}";
            strQuery = string.Format(strQuery, ClientId);
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Client domain = new Client();
                            domain.ClientId = int.Parse(reader["ClientId"].ToString());
                            domain.Name = reader["Name"].ToString();
                            domain.LastName = reader["LastName"].ToString();
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
            return modelList.Count() > 0 ? modelList.FirstOrDefault() : new Client();
        }
        public Client GetClientByFilter(string filter)
        {
            List<Client> modelList = new List<Client>();
            string strQuery = @"SELECT * FROM Clients
                                WHERE Name LIKE '%{0}%'";
            strQuery = string.Format(strQuery, filter, filter);
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Client domain = new Client();
                            domain.ClientId = int.Parse(reader["ClientId"].ToString());
                            domain.Name = reader["Name"].ToString();
                            domain.LastName = reader["LastName"].ToString();
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
            return modelList.Count() > 0 ? modelList.FirstOrDefault() : new Client();
        }

        public void UpdateClient(Client domain)
        {
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
            {
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"UPDATE Clients
                                        SET 
                                        Name = COALESCE(@Name, Name),
                                        Price = COALESCE(@LastName, LastName),
                                        Description = COALESCE(@Address, Address)
                                        WHERE ClientId = @ClientId";



                        command.Parameters.AddWithValue("@Name", domain.Name);
                        command.Parameters.AddWithValue("@LastName", domain.LastName);
                        command.Parameters.AddWithValue("@Address", domain.Address);
                        command.Parameters.AddWithValue("@ClientId", domain.ClientId);

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
