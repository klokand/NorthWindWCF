using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayerNorthwindClient.ProductServiceRef;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace LayerNorthwindClient
{
    class Program
    {
        static void Main(string[] args)
        {
            /*test
            var client = new ProductServiceClient();

            var product = client.GetProduct(23);
            Console.WriteLine("product name is " + product.ProductName);
            Console.WriteLine("product price is " + product.UnitPrice.ToString());
             * /
           /*
            product.UnitPrice = 20.0m;
            var message = "";
            var result = client.UpdateProduct(product, ref message);
            Console.WriteLine("Update result is " + result.ToString());
            Console.WriteLine("Update message is " + message);
            * */
            /*Console.ReadLine();*/

            string connectionString = ConfigurationManager.ConnectionStrings["NorthWindMySql"].ConnectionString;
            MySql.Data.MySqlClient.MySqlConnection conn;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select * from Products where ProductID=@id";
                cmd.Parameters.AddWithValue("@id", 12);
                cmd.Connection = conn;
                conn.Open();
                
                MySqlDataReader reader = cmd.ExecuteReader();
                
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Console.WriteLine((string)reader["ProductName"]);
                            Console.WriteLine((string)reader["QuantityPerUnit"]);
                            Console.WriteLine((decimal)reader["UnitPrice"]);
                            Console.WriteLine((short)reader["UnitsInStock"]);
                            Console.WriteLine((short)reader["UnitsOnOrder"]);
                            Console.WriteLine((short)reader["ReorderLevel"]);
                            Console.WriteLine((bool)reader["Discontinued"]);
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
                        Console.ReadLine();
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Good");
                        Console.ReadLine();
                        break;
                        /*
                         * 
                         * 
                         *  using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandTimeout = 60;
                            cmd.CommandText = "select * from Products where ProductID=@id";
                            cmd.Parameters.AddWithValue("@id", 12);
                            cmd.Connection = conn;
                            conn.Open();
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    Console.WriteLine((string)reader["ProductName"]);
                                    Console.WriteLine((string)reader["QuantityPerUnit"]);
                                    Console.WriteLine((decimal)reader["UnitPrice"]);
                                    Console.WriteLine((short)reader["UnitsInStock"]);
                                    Console.WriteLine((short)reader["UnitsOnOrder"]);
                                    Console.WriteLine((short)reader["ReorderLevel"]);
                                    Console.WriteLine((bool)reader["Discontinued"]);
                                }
                            }
                        }
                         */
                }
            }


        }
    }
}
