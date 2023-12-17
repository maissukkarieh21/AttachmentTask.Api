using AttachmentTask.Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace AttachmentTask.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("attachments");

                entity.HasOne(d => d.Employee).WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.EmpolyeeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("employees");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
            });
        }
    }
}
