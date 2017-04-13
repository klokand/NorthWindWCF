using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayerNorthwindBDO;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace LayerNorthwindDAL
{
    public class ProductDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["NorthWindMySql"].ConnectionString;
        MySql.Data.MySqlClient.MySqlConnection conn;

        public ProductBDO GetProduct(int id)
        {   
            // TODO: connect to DB to retrieve product
            ProductBDO p = null;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select * from Products where ProductID=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    p = new ProductBDO();
                    p.ProductID = id;
                    p.ProductName = (string)reader["ProductName"];
                    p.UnitPrice = (decimal)reader["UnitPrice"];
                    p.UnitsInStock = (short)reader["UnitsInStock"];
                    p.ReorderLevel = (short)reader["ReorderLevel"];
                    p.Discontinued = Convert.ToBoolean((ulong)reader["Discontinued"]);
                    p.UnitsOnOrder = (short)reader["UnitsOnOrder"];
                    p.QuantityPerUnit = (string)reader["QuantityPerUnit"];
                }
                Console.WriteLine("Good connected");
                Console.ReadLine();
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                    default:
                        Console.WriteLine("Good");
                        Console.ReadLine();
                        break;
                }
            }
            /*
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from Products where ProductID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            p = new ProductBDO();
                            p.ProductID = id;
                            p.ProductName = (string)reader["ProductName"];
                            p.QuantityPerUnit = (string)reader["QuantityPerUnit"];
                            p.UnitPrice = (decimal)reader["UnitPrice"];
                            p.UnitsInStock = (short)reader["UnitsInStock"];
                            p.UnitsOnOrder = (short)reader["UnitsOnOrder"];
                            p.ReorderLevel = (short)reader["ReorderLevel"];
                            p.Discontinued = (bool)reader["Discontinued"];
                        }
                    }
                }
            }
             * */
            return p;
        }
        public bool UpdateProduct(ProductBDO product, ref string message)
        {
            // TODO: connect to DB to update product
            message = "product updated successfully";
            var ret = true;
            using (conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                var cmdStr = @"UPDATE products SET ProductName=@name, QuantityPerUnit=@unit, UnitPrice=@price, Discontinued=@discontinued WHERE ProductID=@id";
                using (MySqlCommand cmd = new MySqlCommand(cmdStr, conn))
                {
                    cmd.Parameters.AddWithValue("@name",product.ProductName);
                    cmd.Parameters.AddWithValue("@unit",product.QuantityPerUnit);
                    cmd.Parameters.AddWithValue("@price",product.UnitPrice);
                    cmd.Parameters.AddWithValue("@discontinued",product.Discontinued);
                    cmd.Parameters.AddWithValue("@id",product.ProductID);
                    conn.Open();
                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        message = "no product was updated";
                        ret = false;
                    }
                }
            }

            return ret;
        }
    }
}
