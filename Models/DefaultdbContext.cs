using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace dotnet.Models
{
    public partial class DefaultdbContext : DbContext
    {

        public virtual DbSet<HibernateSequence> HibernateSequences { get; set; }

        public DefaultdbContext()
        {
        }

        public DefaultdbContext(DbContextOptions<DefaultdbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Place>(entity =>
            {

                entity.Navigation(x => x.Owner).AutoInclude();
                entity.ToTable("place");

                entity.HasIndex(e => e.OwnerId, "FK5rljo0tn7t3tjjkhpw25aur2m");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK5rljo0tn7t3tjjkhpw25aur2m");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleType)
                    .HasMaxLength(20)
                    .HasColumnName("role_type");
            });

            modelBuilder.Entity<HibernateSequence>(entity =>
            {
                // entity.HasNoKey();

                entity.ToTable("hibernate_sequence");

                entity.Property(e => e.NextVal).HasColumnName("next_val");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Navigation(x => x.Role).AutoInclude();

                entity.ToTable("user");

                entity.HasIndex(e => e.RoleId, "FKn82ha3ccdebhokx3a8fgdqeyy");

                entity.HasOne(d => d.Role)
                   .WithMany(p => p.Users)
                   .HasForeignKey(d => d.RoleId)
                   .HasConstraintName("FKn82ha3ccdebhokx3a8fgdqeyy");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.Navigation(x => x.Guests).AutoInclude();
                entity.Navigation(x => x.Place).AutoInclude();
                entity.ToTable("visit");

                entity.HasIndex(e => e.PlaceId, "FKeykr09xn08xkwsq51ivwyhpov");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FinishDate)
                    .HasMaxLength(6)
                    .HasColumnName("finish_date");

                entity.Property(e => e.PlaceId).HasColumnName("place_id");

                entity.Property(e => e.VisitDate)
                    .HasMaxLength(6)
                    .HasColumnName("visit_date");

                entity.HasMany<Guest>(d => d.Guests).WithMany(g => g.Visits).UsingEntity<VisitGuest>(
                    x => x.HasOne(x => x.Guests)
                    .WithMany().HasForeignKey(x => x.GuestsId),
                    x => x.HasOne(x => x.Visit)
                   .WithMany().HasForeignKey(x => x.VisitId));
            });

            modelBuilder.Entity<VisitGuest>(entity =>
          {
              entity.HasKey(e => new { e.VisitId, e.GuestsId })
                  .HasName("PRIMARY")
                  .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

              entity.ToTable("visit_guests");

              entity.HasIndex(e => e.GuestsId, "FKb79updgdld1cwtrw73a554lvi");

              entity.Property(e => e.VisitId).HasColumnName("visit_id");

              entity.Property(e => e.GuestsId).HasColumnName("guests_id");

          });

            // modelBuilder.Entity<Guest>(entity =>
            //  {
            //      entity.HasMany<Visit>(d => d.Visits).WithMany(g => g.Guests).UsingEntity(j => j.ToTable("visit_guests"));
            //  });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
