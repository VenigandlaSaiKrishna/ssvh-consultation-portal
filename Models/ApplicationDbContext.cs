using Microsoft.EntityFrameworkCore;

namespace SSVH_Consultation_Poratl.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>().ToTable("class");
            modelBuilder.Entity<ClassNames>().ToTable("classnames");
            modelBuilder.Entity<Fees>().ToTable("fees");
            modelBuilder.Entity<Stationary>().ToTable("stationary");
            modelBuilder.Entity<Transport>().ToTable("transport");
            modelBuilder.Entity<Configs>().ToTable("configs");
            modelBuilder.Entity<Consultation>().ToTable("consultation");
            modelBuilder.Entity<Roles>().ToTable("roles");
            modelBuilder.Entity<Users>().ToTable("users");
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassNames> ClassNames { get; set; }
        public DbSet<Fees> Fees { get; set; }
        public DbSet<Stationary> Stationary { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public DbSet<Configs> Configs { get; set; }
        public DbSet<Consultation> Consultation { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
