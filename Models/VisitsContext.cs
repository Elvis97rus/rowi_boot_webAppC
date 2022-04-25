using Microsoft.EntityFrameworkCore;

namespace WebApp_c.Models
{
    public class VisitsContext : DbContext
    {
        public VisitsContext(DbContextOptions<VisitsContext> options) : base(options)
        {

        }

        //public DbSet<Command> CommandItems { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
    }
}