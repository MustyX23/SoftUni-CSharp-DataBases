using P02_FootballBetting.Data;

namespace P02_FootballBetting 
{
    public class StartUp 
    {
        static void Main()
        {
            var context = new FootballBettingContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
