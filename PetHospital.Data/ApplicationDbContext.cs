using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetHospital.Data.Entities;
using PetHospital.Data.Entities.Identity;

namespace PetHospital.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole,
    IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public DbSet<Animal> Animal { get; set; }
    public DbSet<Clinic> Clinic { get; set; }
    public DbSet<Contacts> Contacts { get; set; }
    public DbSet<Diseases> Diseases { get; set; }
    public DbSet<Photo> Photo { get; set; }
    public DbSet<Subscription> Subscription { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<Subscription>()
            .HasOne(t => t.User)
            .WithOne(t => t.Subscription)
            .HasForeignKey<User>(t => t.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Role>()
            .HasMany(t => t.UserRoles)
            .WithOne(t => t.Role)
            .HasForeignKey(t => t.RoleId)
            .IsRequired();
        modelBuilder
            .Entity<User>()
            .HasMany(t => t.UserRoles)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .IsRequired();

        modelBuilder
            .Entity<Animal>()
            .HasMany(t => t.Photos)
            .WithOne(t => t.Animal)
            .HasForeignKey(t => t.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<User>()
            .HasMany(t => t.Animals)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .HasPrincipalKey(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Contacts>()
            .HasOne(x => x.User)
            .WithMany(x => x.Contacts)
            .HasForeignKey( x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder
            .Entity<Diseases>()
            .HasOne(t => t.Animal)
            .WithMany(t => t.Diseases)
            .HasForeignKey(t => t.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<UserClinic>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserClinic)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<UserClinic>()
            .HasOne(x => x.Clinic)
            .WithMany(x => x.UserClinic)
            .HasForeignKey(x => x.ClinicId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<AnimalClinic>()
            .HasOne(x => x.Clinic)
            .WithMany(x => x.AnimalClinic)
            .HasForeignKey(x => x.ClinicId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<AnimalClinic>()
            .HasOne(x => x.Animal)
            .WithMany(x => x.AnimalClinic)
            .HasForeignKey(x => x.AnimalId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        //modelBuilder
        //    .Entity<User>()
        //    .HasMany(x => x.Clinic)
        //    .WithMany(x => x.User);
    }
}