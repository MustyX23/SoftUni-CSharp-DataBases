using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {
        }

        //Add DbSets

        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientMedicament> Prescriptions { get; set;}

        public DbSet<Visitation> Visitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Configure the local server
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Hospital;" +
                    "Trusted_Connection=true;TrustServerCertificate=true");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Set primary keys to the mappingtable

            modelBuilder.Entity<PatientMedicament>(entity => 
            {
                entity.HasKey(pm => new {pm.PatientId, pm.MedicamentId });
            });
        }        
    }
}
