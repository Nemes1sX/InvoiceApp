using InvoiceApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.DataContext
{
    public class InvoiceDataContext : DbContext 
    {
        public InvoiceDataContext(DbContextOptions options) : base(options) 
        { 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=InvoiceApp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Invoice> Invoices { get; set; }    
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<LegalPerson> LegalPersons { get; set;}
    }
}
