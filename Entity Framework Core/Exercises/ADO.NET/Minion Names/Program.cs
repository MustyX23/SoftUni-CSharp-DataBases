using Microsoft.Data.SqlClient;
using System;

namespace Minion_Names
{
    public class StartUp
    {
        static void Main(string[] args)
        {            
            using (SqlConnection connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();
                string firstQuery = "SELECT Name FROM Villains WHERE Id = @Id";
                SqlCommand firstCommand = new SqlCommand(firstQuery, connection);

                int villainId = int.Parse(Console.ReadLine());

                firstCommand.Parameters.AddWithValue("@Id", villainId);
                string villainName = (string)firstCommand.ExecuteScalar();

                if (villainName == null)
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                    return;
                }

                Console.WriteLine($"Villain: {villainName}");

                string secondQuery = "SELECT ROW_NUMBER() OVER (ORDER BY m.Name) AS RowNum, " +
                    "m.Name, m.Age FROM MinionsVillains AS mv " +
                    "JOIN Minions As m ON mv.MinionId = m.Id WHERE mv.VillainId = @Id " +
                    "ORDER BY m.Name";

                SqlCommand secondCommand = new SqlCommand(secondQuery, connection);

                secondCommand.Parameters.AddWithValue("@Id", villainId);
                SqlDataReader sqlDataReader = secondCommand.ExecuteReader();

                if (sqlDataReader.HasRows == false)
                {
                    Console.WriteLine("(no minions)");
                    return;
                }

                while (sqlDataReader.Read())
                {
                    int rowNum = (int)Convert.ToInt64((sqlDataReader[0]));
                    string minionName = (string)sqlDataReader[1]; 
                    int age = (int)sqlDataReader[2];

                    Console.WriteLine($"{rowNum}. {minionName} {age}");
                }

            }
        }
    }
}
