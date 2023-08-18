using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Permissions;

namespace ProductInventoryApp
{
    internal class Program
    {
        static SqlDataReader reader;
        static SqlConnection con = new SqlConnection("Server=DESKTOP-2D9MD1Q;Database=ProductInventoryDB;Trusted_Connection=true");
        static SqlCommand cmd;
        public static void ViewProducts()
        {
            cmd = new SqlCommand("Select * From ProductInventory", con);
            con.Open();
            reader = cmd.ExecuteReader();
            Console.WriteLine("ProductId\tProductName\tPrice\t   Quantity\t\tMfDate\t\t\t\tExpDate\t");
            while (reader.Read())
            {
                Console.WriteLine("    " + reader["ProductId"] + "           " + reader["ProductName"] + "      " + reader["Price"] + "           " + reader["Quantity"] + "          " + reader["MfDate"] + "         " + reader["ExpDate"]);
            }
        }
        public static void AddProduct()
        {
            cmd = new SqlCommand("Insert Into ProductInventory(ProductId,ProductName,Price,Quantity,MfDate,ExpDate) Values (@id,@name,@price,@quantity,@MFD,@ED)", con);
            con.Open();
            Console.WriteLine("Enter New Product Id");
            cmd.Parameters.AddWithValue("@id", int.Parse(Console.ReadLine()));
            Console.WriteLine("Enter New Product Name");
            cmd.Parameters.AddWithValue("@name", Console.ReadLine());
            Console.WriteLine("Enter New Price");
            cmd.Parameters.AddWithValue("@price", decimal.Parse(Console.ReadLine()));
            Console.WriteLine("Enter New Quantity");
            cmd.Parameters.AddWithValue("@quantity", int.Parse(Console.ReadLine()));
            Console.WriteLine("Enter New Manufacture Date");
            cmd.Parameters.AddWithValue("@MFD", DateTime.Parse(Console.ReadLine()));
            Console.WriteLine("Enter New Expiration Date");
            cmd.Parameters.AddWithValue("@ED", DateTime.Parse(Console.ReadLine()));
            int nor = cmd.ExecuteNonQuery();
            if (nor >= 1)
                Console.WriteLine("1 Rows Affected");
        }
        public static void UpdateProduct()
        {
            cmd = new SqlCommand("Update ProductInventory Set Quantity=@quantity where ProductId=@id", con);
            con.Open();
            Console.WriteLine("Enter Id to Update");
            cmd.Parameters.AddWithValue("@id", int.Parse(Console.ReadLine()));
            Console.WriteLine("Enter New Quantity");
            cmd.Parameters.AddWithValue("@quantity", int.Parse(Console.ReadLine()));
            int nor = cmd.ExecuteNonQuery();
            if (nor >= 1)
                Console.WriteLine("1 Rows has been Updated");
            else
                Console.WriteLine("No Such ID in the Table");
        }
        public static void RemoveProduct()
        {
            cmd = new SqlCommand("Delete From ProductInventory where ProductId=@id", con);
            con.Open();
            Console.WriteLine("Enter Product Id to Delete");
            cmd.Parameters.AddWithValue("@id", int.Parse(Console.ReadLine()));
            int nor = cmd.ExecuteNonQuery();
            if (nor >= 1)
                Console.WriteLine("1 Rows has been Deleted");
            else
                Console.WriteLine("No Such ID in the Table");
        }
        static void Main(string[] args)
        {
            try
            {
                
           
                Console.WriteLine("1.View ProductInventory");
                Console.WriteLine("2.Add New Product");
                Console.WriteLine("3.Update Product Quantity");
                Console.WriteLine("4.Remove Product");
                Console.WriteLine("Enter your Choice");
                Console.WriteLine("-----------------------------");
                int op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        {
                            ViewProducts();
                            break;
                        }
                    case 2:
                        {
                            AddProduct();
                            break;
                        }
                    case 3:
                        {
                            UpdateProduct();
                            break;
                        }
                    case 4:
                        {
                            RemoveProduct();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Choice");
                            return;
                        }
                }
              
                 



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
