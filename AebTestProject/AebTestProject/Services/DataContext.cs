using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace AebTestProject.Services
{
    public class DataContext : DbContext
    {
        public DbSet<Models.Task> Tasks { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
