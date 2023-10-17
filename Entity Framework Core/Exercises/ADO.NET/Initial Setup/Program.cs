using Microsoft.Data.SqlClient;
using Minion_Names;
using System;

namespace Initial_Setup
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();

                string query = "SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount " +
                               "FROM Villains AS v " +
                               "JOIN MinionsVillains AS mv ON v.Id = mv.VillainId " +
                               "GROUP BY v.Id, v.Name " +
                               "HAVING COUNT(mv.VillainId) > 3" +
                               "ORDER BY COUNT(mv.VillainId)";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                }
                
            }
        }
    }
}
