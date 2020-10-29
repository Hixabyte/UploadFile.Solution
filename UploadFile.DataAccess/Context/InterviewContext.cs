namespace UploadFile.DataAccess.Context
{
    using Microsoft.EntityFrameworkCore;

    using UploadFile.Models;

    /// <summary>
    /// EF Core DbContext
    /// </summary>
    public partial class InterviewContext : DbContext
    {
        public InterviewContext()
        {
        }

        public InterviewContext(DbContextOptions<InterviewContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// Connections string. Locating the connection string here in the DBContext is sub-optimal.
        /// Storing secrets in Azure KeyVault would be a superior solution.
        /// As this DbContext is used by multiple projects and with KeyVault unavailable
        /// for this project, specifying the connection string here is preferred to
        /// storing in multiple appConfig files.
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(LocalDb)\\Interview;Initial Catalog=Interview;Trusted_Connection=True;");
            }
        }

        public virtual DbSet<EmployeeDto> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDto>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Forename).IsRequired();

                entity.Property(e => e.Gender).IsRequired();

                entity.Property(e => e.Role).IsRequired();

                entity.Property(e => e.Salary).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Surname).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
