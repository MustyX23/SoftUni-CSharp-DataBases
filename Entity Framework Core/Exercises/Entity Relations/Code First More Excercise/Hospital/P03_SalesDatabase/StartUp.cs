using P03_SalesDatabase.Data;

namespace P01_HospitalDatabase
{
    public class StartUp
    {
        public static void Main() 
        {
            var context = new SalesContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}