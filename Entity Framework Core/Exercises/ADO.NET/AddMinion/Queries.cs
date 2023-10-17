using System;
using System.Collections.Generic;
using System.Text;

namespace AddMinion
{
    internal static class Queries
    {
        public const string GetVillainId = "SELECT Id FROM Villains WHERE Name = @Name";
        public const string GetMinnionId = "SELECT Id FROM Minions WHERE Name = @Name";
        public const string GetTownId = "SELECT Id FROM Towns WHERE Name = @townName";

        public const string AddMinnionToVillain
            = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";
        public const string AddNewTown = "INSERT INTO Towns (Name) VALUES (@townName)";
        public const string AddNewVillain = "INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
        public const string AddNewMinnion = "INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";
    }
}
