using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharprestClient
{
    class SqlStatements
    {

        SqlConnection connection;

        public void createTableSqlCommand(string nameOfTable, string primaryKey, string connectionString)
        {
            string createTableCommand = String.Format("CREATE TABLE [{0}] ({1} bigint IDENTITY(100001,1) Primary Key, [Date] date, [Time] time, [Close] decimal(9, 4), [High] decimal(9, 4), [Low] decimal(9, 4), [Open] decimal(9, 4), [Volume] int, Stock_ID int Foreign Key References Stock_List(Stock_ID));", nameOfTable, primaryKey);
            //string createTableCommand = "CREATE TABLE ALEX(age int);";

            connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(createTableCommand, connection);

            command.CommandTimeout = 0;

            Console.WriteLine("Attempting to create table for: " + nameOfTable);
            // Execeute the command to create the table:
            Console.WriteLine("Are we opening?");

            Console.WriteLine("We opened!");
            command.ExecuteNonQuery();

            connection.Dispose();
        }
    }
}
