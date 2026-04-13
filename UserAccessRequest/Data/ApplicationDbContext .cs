using Microsoft.EntityFrameworkCore;
using UserAccessRequest.Model.Database;

namespace UserAccessRequest.Data
{
    public class ApplicationDbContext : DbContext  
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Form> Forms { get; set; }
        public DbSet<Fields> Fields { get; set; }
        public DbSet<FormFields> formFields { get; set; }
        public DbSet<RequestForms> RequestForms { get; set; }
        public DbSet<RequestFormsResult> RequestFormsResult { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure if not already configured (useful for parameterless constructor)
            if (!optionsBuilder.IsConfigured)
            {
                // Example: Configure SQL Server
                // optionsBuilder.UseSqlServer("YourConnectionString");
                optionsBuilder.UseNpgsql("DefaultConnection");

                // Or configure SQLite
                // optionsBuilder.UseSqlite("Data Source=useraccess.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships, constraints, etc.
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<FormFields>(
                entity =>
                    {
                        entity.HasOne(form => form.Form)
                        .WithMany(field => field.FormFields)
                        .HasForeignKey(form => form.FormId)
                        .OnDelete(DeleteBehavior.Cascade);

                        entity.HasOne(field => field.Field)
                        .WithMany(form => form.FieldForms)
                        .HasForeignKey(field => field.FieldId)
                        .OnDelete(DeleteBehavior.Cascade);
                    }
                );


            modelBuilder.Entity<RequestForms>(
                entity =>
                    {
                        entity.HasOne(f => f.Form)
                        .WithMany(r => r.RequestForms)
                        .HasForeignKey(f => f.FormId)
                        .OnDelete(DeleteBehavior.NoAction);
                    }
                );

            modelBuilder.Entity<RequestFormsResult>(
                entity =>
                    {
                        entity.HasOne(f => f.RequestForms)
                        .WithMany(r => r.RequestFormsResult)
                        .HasForeignKey(f => f.RequestFormId)
                        .OnDelete(DeleteBehavior.NoAction);

                        entity.HasOne(f => f.Fields)
                        .WithMany(r => r.ResultFields)
                        .HasForeignKey(f => f.FieldId)
                        .OnDelete(DeleteBehavior.NoAction);

                    }
                );
        }
    }
}