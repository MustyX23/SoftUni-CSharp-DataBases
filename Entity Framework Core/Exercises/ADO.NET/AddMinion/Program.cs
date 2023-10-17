using Microsoft.Data.SqlClient;
using Minion_Names;
using System;

namespace AddMinion
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString))
            {
                sqlConnection.Open();
                string[] firstInput = Console.ReadLine().Split(' ');
                string minionNameInput = firstInput[1];
                int age = int.Parse(firstInput[2]);
                string town = firstInput[3];

                string[] secondInput = Console.ReadLine().Split(' ');
                string villainNameInput = secondInput[1];



                SqlCommand getTownIdCmd = new SqlCommand(Queries.GetTownId, sqlConnection);
                getTownIdCmd.Parameters.AddWithValue("@townName", town);
                int? townId = (int?)getTownIdCmd.ExecuteScalar();
                if (!townId.HasValue)
                {
                    SqlCommand addNewTownCmd = new SqlCommand(Queries.AddNewTown, sqlConnection);
                    addNewTownCmd.Parameters.AddWithValue("@townName", town);
                    addNewTownCmd.ExecuteNonQuery();
                    Console.WriteLine($"Town {town} was added to the database.");
                    townId = (int?)getTownIdCmd.ExecuteScalar();
                }

                SqlCommand getVillionId = new SqlCommand(Queries.GetVillainId, sqlConnection);
                getVillionId.Parameters.AddWithValue("@Name", villainNameInput);

                int? villainId= (int?)getVillionId.ExecuteScalar();

                if (villainId == null)
                {
                    SqlCommand addNewVillainCmd = new SqlCommand(Queries.AddNewVillain, sqlConnection);
                    addNewVillainCmd.Parameters.AddWithValue("@villainName", villainNameInput);
                    addNewVillainCmd.ExecuteNonQuery();
                    Console.WriteLine($"Villain {villainNameInput} was added to the database.");
                    villainId = (int?)getVillionId.ExecuteScalar();                    
                }
                

                SqlCommand getMinnionIdCmd = new SqlCommand(Queries.GetMinnionId, sqlConnection);
                getMinnionIdCmd.Parameters.AddWithValue("@Name", minionNameInput);

                int? minnionId = (int?)getMinnionIdCmd.ExecuteScalar();

                if (minnionId == null)
                {
                    SqlCommand addNewMinnionCmd = new SqlCommand(Queries.AddNewMinnion, sqlConnection);
                    addNewMinnionCmd.Parameters.AddWithValue("@name", minionNameInput);
                    addNewMinnionCmd.Parameters.AddWithValue("@age", age);
                    addNewMinnionCmd.Parameters.AddWithValue("@townId", townId);

                    addNewMinnionCmd.ExecuteNonQuery();                    
                }

                SqlCommand setMinnionToVillain = new SqlCommand(Queries.AddMinnionToVillain, sqlConnection);
                setMinnionToVillain.Parameters.AddWithValue("@minionId", minnionId);
                setMinnionToVillain.Parameters.AddWithValue("@villainId", villainId);
                setMinnionToVillain.ExecuteNonQuery();
                Console.WriteLine($"Successfully added {minionNameInput} to be minion of {villainNameInput}.");
               
            }
        }
    }
}
