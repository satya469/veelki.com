// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Veelki.Data.Entities;

namespace Veelki.Data
{
    public partial class RB444Context : IdentityDbContext
    {
        public RB444Context()
        {
        }

        public RB444Context(DbContextOptions<RB444Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Forms> Forms { get; set; }
        public virtual DbSet<PermissonSet> PermissonSets { get; set; }
        public virtual DbSet<PermissonSetMapping> PermissonSetMappings { get; set; }
        public virtual DbSet<Workspace> Workspaces { get; set; }
        public virtual DbSet<WorkspacePermissonMapping> WorkspacePermissionMappings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=181.214.133.181;Database=fAvjet;;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=explorate;Password=2sTwe7MrL9;Integrated Security=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_tblUsers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleId");

                entity.Property(e => e.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .HasColumnName("FullName")
                    .HasMaxLength(200);

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("PasswordHash")
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasColumnName("PhoneNumber");

                //entity.HasOne(d => d.WorkSpace)
                //    .WithMany(p => p.Users)
                //    .HasForeignKey(d => d.RoleId)
                //    .HasConstraintName("FK_tblUsers_tblUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
