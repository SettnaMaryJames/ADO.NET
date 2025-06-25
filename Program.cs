using MySql.Data.MySqlClient;
using Mysqlx.Cursor;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET
{
    internal class Program
    {
        static void Main(string[] args)
        {

           
                var ConnString = "server=localhost;port=330" +
                "" +
                "6;username=root;password=root;database=dapper";

            using (MySqlConnection conn = new MySqlConnection(ConnString))
            {

                try

                ///// <summary>     
                {

                    conn.Open();
                    Console.WriteLine("Connection Opened");
                    MySqlCommand insertcmd = new MySqlCommand("Insert into employees(name,Bio)Values('Harry','sales') ",conn);
                    var insertedvalues=insertcmd.ExecuteNonQuery();
                    Console.WriteLine($"{insertedvalues} inserted");

                    Console.WriteLine("MySqDataReader");
                    MySqlCommand selectsmd = new MySqlCommand("Select * from employees",conn);    
                    MySqlDataReader reader=selectsmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name :{reader["Name"]},Bio:{reader["Bio"]}");
                    }

                    reader.Close();

                    Console.WriteLine("Updating record...");
                    MySqlCommand updateCmd = new MySqlCommand("UPDATE employees SET Bio='Updated Bio' WHERE name='Harry'", conn);
                    var updatedValues = updateCmd.ExecuteNonQuery();
                    Console.WriteLine($"{updatedValues} record(s) updated.");

                    Console.WriteLine("Deleting record...");
                    MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM employees WHERE name='Harry'", conn);
                    var deletedValues = deleteCmd.ExecuteNonQuery();
                    Console.WriteLine($"{deletedValues} record(s) deleted.");
                    Console.WriteLine("MySqlDataAdapter");
                    MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from employees",conn);
                    DataSet ds=new DataSet();
                    adapter.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach(DataRow dr in dt.Rows)
                    {
                        Console.WriteLine($"Name:{dr["Name"]},Bio:{dr["Bio"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error", ex.Message);
                }

                finally
                {

                    conn.Close();
                    Console.WriteLine("Connection Closed");

                }

            }
        }
    }
}
