using P01_HospitalDatabase.Data;

namespace P01_HospitalDatabase
{
    public class StartUp
    {
        public static void Main() 
        {
            var context = new HospitalContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}