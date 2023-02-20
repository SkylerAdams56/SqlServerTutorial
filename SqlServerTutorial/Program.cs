using Microsoft.Data.SqlClient;
using Microsoft.SqlServer;
using System.ComponentModel.Design.Serialization;

string connectionString = "server=localhost\\sqlexpress;" +
                            "database=SalesDb;" +
                             "trusted_connection=true;" +
                             "trustServerCertificate=true;";

SqlConnection sqlConn = new SqlConnection(connectionString);

sqlConn.Open();

if(sqlConn.State != System.Data.ConnectionState.Open)
{
    throw new Exception("I screwedup my connection string!");
}
Console.WriteLine("Connection opened successfully!");


string sql = "SELECT * from Customers where sales > 90000 order by sales desc;";

SqlCommand cmd = new SqlCommand(sql, sqlConn);

SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    var id = Convert.ToInt32(reader["Id"]);
    var name = Convert.ToString(reader["Name"]);
    var city = Convert.ToString(reader["City"]);
    var state = Convert.ToString(reader["State"]);
    var sales = Convert.ToDecimal(reader["Sales"]);
    var active = Convert.ToBoolean(reader["Active"]);
    Console.WriteLine($"{id}|{name}|{city}, {state}|{sales}|{(active ? "yes" : "No")}");
}
reader.Close();

string sql2 = "Select * from Orders;";

cmd = new SqlCommand(sql2, sqlConn);

 reader = cmd.ExecuteReader();
while (reader.Read())
{
    var id2 = Convert.ToInt32(reader["Id"]);
    var customerId = (reader["customerId"].Equals(System.DBNull.Value)
                         ? (int?)null
                         : Convert.ToInt32(reader["CustomerId"]));
    var datetime = Convert.ToDateTime(reader["Date"]);
    var description = Convert.ToString(reader["Description"]);
    Console.WriteLine($"{id2}|{customerId}|{datetime}|{description}");
}

reader.Close();


sqlConn.Close();
