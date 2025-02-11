using ExampleStore.Data;
using ExampleStore.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ExampleStore.Services
{
    public class ProductService : IProductService
    {
        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public ProductService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }

        public void CreateProduct(Product domain)
        {
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
            {
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"INSERT INTO Products 
                                                (Name
                                                ,Price
                                                ,Description
                                                ,Stock)
                                                VALUES
                                                (@Name
                                                ,@Price
                                                ,@Description
                                                ,@Stock)";
                        command.Parameters.AddWithValue("@Name", domain.Name);
                        command.Parameters.AddWithValue("@Price", domain.Price);
                        command.Parameters.AddWithValue("@Description", domain.Description);
                        command.Parameters.AddWithValue("@Stock", true);

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

        public void DeleteProduct(int ProductId)
        {
            string strQuery = @"DELETE FROM Products
                                WHERE ProductId = {0}";
            strQuery = string.Format(strQuery, ProductId);
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

        public List<Product> GetAllProducts()
        {
            List<Product> modelList = new List<Product>();
            string strQuery = @"SELECT
                                *
                                FROM Products";
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
                            Product domain = new Product();
                            domain.ProductId = int.Parse(reader["ProductId"].ToString());
                            domain.Name = reader["Name"].ToString();
                            domain.Description = reader["Description"].ToString();
                            domain.Price = Convert.ToDecimal( reader["Price"].ToString());
                            domain.Stock = Convert.ToBoolean(reader["Stock"].ToString());
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

        public Product GetProductById(int ProductId)
        {
            List<Product> modelList = new List<Product>();
            string strQuery = @"SELECT
                                *
                                FROM Products
                               WHERE ProductId = {0}";
            strQuery = string.Format(strQuery, ProductId);
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
                            Product domain = new Product();
                            domain.ProductId = int.Parse(reader["ProductId"].ToString());
                            domain.Name = reader["Name"].ToString();
                            domain.Description = reader["Description"].ToString();
                            domain.Price = Convert.ToDecimal(reader["Price"].ToString());
                            domain.Stock = Convert.ToBoolean(reader["Stock"].ToString());
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
            return modelList.Count() > 0 ? modelList.FirstOrDefault() : new Product();
        }
        public Product GetProductByFilter(string filter)
        {
            List<Product> modelList = new List<Product>();
            string strQuery = @"SELECT * FROM Products
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
                            Product domain = new Product();
                            domain.ProductId = int.Parse(reader["ProductId"].ToString());
                            domain.Name = reader["Name"].ToString();
                            domain.Description = reader["Description"].ToString();
                            domain.Price = Convert.ToDecimal(reader["Price"].ToString());
                            domain.Stock = Convert.ToBoolean(reader["Stock"].ToString());
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
            return modelList.Count() > 0 ? modelList.FirstOrDefault() : new Product();
        }

        public void UpdateProduct(Product domain)
        {
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
            {
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"UPDATE Products
                                        SET 
                                        Name = COALESCE(@Name, Name),
                                        Price = COALESCE(@Price, Price),
                                        Description = COALESCE(@Description, Description),
                                        Stock = COALESCE(@Stock, Stock)
                                        WHERE ProductId = @ProductId";



                        command.Parameters.AddWithValue("@Name", domain.Name);
                        command.Parameters.AddWithValue("@Price", domain.Price);
                        command.Parameters.AddWithValue("@Description", domain.Description);
                        command.Parameters.AddWithValue("@Stock", domain.Stock);
                        command.Parameters.AddWithValue("@ProductId", domain.ProductId);

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
