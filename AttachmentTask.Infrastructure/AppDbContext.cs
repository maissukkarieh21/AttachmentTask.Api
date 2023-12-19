using AttachmentTask.Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace AttachmentTask.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AttachmentsGroup> AttachmentsGroup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("attachments");

            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("employees");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();


            });

            modelBuilder.Entity<AttachmentsGroup>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("attachmentsGroup");

                


                //    modelBuilder.Entity<SystemAttachments>()
                //.HasOne(ea => ea.Attachment)
                //.WithMany() // No navigation property in Attachment
                //.HasForeignKey(ea => ea.AttachmentId);

            });

        }
    }
}
