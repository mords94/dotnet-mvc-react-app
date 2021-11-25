using Microsoft.EntityFrameworkCore;

namespace dotnet.Models
{
    public partial class DefaultdbContext : DbContext
    {
        public DefaultdbContext()
        {
        }

        public DefaultdbContext(DbContextOptions<DefaultdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> country { get; set; }
        public virtual DbSet<Guest> guest { get; set; }
        public virtual DbSet<Place> aplace { get; set; }
        public virtual DbSet<Role> role { get; set; }
        public virtual DbSet<User> user { get; set; }
        public virtual DbSet<Visit> visit { get; set; }

        // public DbSet<BaseModel> GetDbSet<T>() where T : BaseModel, new()
        // {
        //     return GetDbSet(new T());
        // }

        // public DbSet<Country> GetDbSet(Country model) => Countries;
        // public DbSet<Guest> GetDbSet(Guest model) => Guests;
        // public DbSet<Place> GetDbSet(Place model) => Places;
        // public DbSet<Role> GetDbSet(Role model) => Roles;
        // public DbSet<User> GetDbSet(User model) => Users;
        // public DbSet<Visit> GetDbSet(Visit model) => Visits;


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.HasCharSet("utf8mb4")
        //         .UseCollation("utf8mb4_0900_ai_ci");

        //     modelBuilder.Entity<Country>(entity =>
        //     {
        //         entity.ToTable("country");

        //         entity.Property(e => e.Id)
        //             .ValueGeneratedNever()
        //             .HasColumnName("id");

        //         entity.Property(e => e.Name)
        //             .HasMaxLength(255)
        //             .HasColumnName("name");
        //     });

        //     modelBuilder.Entity<Guest>(entity =>
        //     {
        //         entity.ToTable("guest");

        //         entity.Property(e => e.Id)
        //             .ValueGeneratedNever()
        //             .HasColumnName("id");

        //         entity.Property(e => e.Email)
        //             .HasMaxLength(255)
        //             .HasColumnName("email");

        //         entity.Property(e => e.FirstName)
        //             .HasMaxLength(255)
        //             .HasColumnName("first_name");

        //         entity.Property(e => e.LastName)
        //             .HasMaxLength(255)
        //             .HasColumnName("last_name");

        //         entity.Property(e => e.Phone)
        //             .HasMaxLength(255)
        //             .HasColumnName("phone");
        //     });

        //     modelBuilder.Entity<HibernateSequence>(entity =>
        //     {
        //         entity.HasNoKey();

        //         entity.ToTable("hibernate_sequence");

        //         entity.Property(e => e.NextVal).HasColumnName("next_val");
        //     });

        //     modelBuilder.Entity<Place>(entity =>
        //     {
        //         entity.ToTable("place");

        //         entity.HasIndex(e => e.OwnerId, "FK5rljo0tn7t3tjjkhpw25aur2m");

        //         entity.Property(e => e.Id).HasColumnName("id");

        //         entity.Property(e => e.AddressLine)
        //             .HasMaxLength(255)
        //             .HasColumnName("address_line");

        //         entity.Property(e => e.City)
        //             .HasMaxLength(255)
        //             .HasColumnName("city");

        //         entity.Property(e => e.Name)
        //             .HasMaxLength(255)
        //             .HasColumnName("name");

        //         entity.Property(e => e.OwnerId).HasColumnName("owner_id");

        //         entity.HasOne(d => d.Owner)
        //             .WithMany(p => p.Places)
        //             .HasForeignKey(d => d.OwnerId)
        //             .HasConstraintName("FK5rljo0tn7t3tjjkhpw25aur2m");
        //     });

        //     modelBuilder.Entity<Role>(entity =>
        //     {
        //         entity.ToTable("role");

        //         entity.Property(e => e.Id).HasColumnName("id");

        //         entity.Property(e => e.RoleType)
        //             .HasMaxLength(20)
        //             .HasColumnName("role_type");
        //     });

        //     modelBuilder.Entity<RoleUser>(entity =>
        //     {
        //         entity.HasKey(e => new { e.RoleId, e.UsersId })
        //             .HasName("PRIMARY")
        //             .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        //         entity.ToTable("role_users");

        //         entity.HasIndex(e => e.UsersId, "UK_ljs8l2207x0igrfp8dw2edxql")
        //             .IsUnique();

        //         entity.Property(e => e.RoleId).HasColumnName("role_id");

        //         entity.Property(e => e.UsersId).HasColumnName("users_id");

        //         entity.HasOne(d => d.Role)
        //             .WithMany(p => p.RoleUsers)
        //             .HasForeignKey(d => d.RoleId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("FKele6ufqrv6w1uoxqw6h1vkki0");

        //         entity.HasOne(d => d.Users)
        //             .WithOne(p => p.RoleUser)
        //             .HasForeignKey<RoleUser>(d => d.UsersId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("FKipeyaf3dve9njdrl1t23ndidv");
        //     });

        //     modelBuilder.Entity<User>(entity =>
        //     {
        //         entity.ToTable("user");

        //         entity.HasIndex(e => e.RoleId, "FKn82ha3ccdebhokx3a8fgdqeyy");

        //         entity.Property(e => e.Id)
        //             .ValueGeneratedNever()
        //             .HasColumnName("id");

        //         entity.Property(e => e.Email)
        //             .HasMaxLength(255)
        //             .HasColumnName("email");

        //         entity.Property(e => e.FirstName)
        //             .HasMaxLength(255)
        //             .HasColumnName("first_name");

        //         entity.Property(e => e.LastName)
        //             .HasMaxLength(255)
        //             .HasColumnName("last_name");

        //         entity.Property(e => e.Password)
        //             .HasMaxLength(255)
        //             .HasColumnName("password");

        //         entity.Property(e => e.Phone)
        //             .HasMaxLength(255)
        //             .HasColumnName("phone");

        //         entity.Property(e => e.RoleId).HasColumnName("role_id");

        //         entity.HasOne(d => d.Role)
        //             .WithMany(p => p.Users)
        //             .HasForeignKey(d => d.RoleId)
        //             .HasConstraintName("FKn82ha3ccdebhokx3a8fgdqeyy");
        //     });

        //     modelBuilder.Entity<Visit>(entity =>
        //     {
        //         entity.ToTable("visit");

        //         entity.HasIndex(e => e.PlaceId, "FKeykr09xn08xkwsq51ivwyhpov");

        //         entity.Property(e => e.Id)
        //             .ValueGeneratedNever()
        //             .HasColumnName("id");

        //         entity.Property(e => e.FinishDate)
        //             .HasMaxLength(6)
        //             .HasColumnName("finish_date");

        //         entity.Property(e => e.PlaceId).HasColumnName("place_id");

        //         entity.Property(e => e.VisitDate)
        //             .HasMaxLength(6)
        //             .HasColumnName("visit_date");

        //         entity.HasOne(d => d.Place)
        //             .WithMany(p => p.Visits)
        //             .HasForeignKey(d => d.PlaceId)
        //             .HasConstraintName("FKeykr09xn08xkwsq51ivwyhpov");
        //     });

        //     modelBuilder.Entity<VisitGuest>(entity =>
        //     {
        //         entity.HasKey(e => new { e.VisitId, e.GuestsId })
        //             .HasName("PRIMARY")
        //             .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        //         entity.ToTable("visit_guests");

        //         entity.HasIndex(e => e.GuestsId, "FKb79updgdld1cwtrw73a554lvi");

        //         entity.Property(e => e.VisitId).HasColumnName("visit_id");

        //         entity.Property(e => e.GuestsId).HasColumnName("guests_id");

        //         entity.HasOne(d => d.Guests)
        //             .WithMany(p => p.VisitGuests)
        //             .HasForeignKey(d => d.GuestsId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("FKb79updgdld1cwtrw73a554lvi");

        //         entity.HasOne(d => d.Visit)
        //             .WithMany(p => p.VisitGuests)
        //             .HasForeignKey(d => d.VisitId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("FKa51vs3624xcdoui85jrjn90hf");
        //     });

        //     OnModelCreatingPartial(modelBuilder);
        // }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
