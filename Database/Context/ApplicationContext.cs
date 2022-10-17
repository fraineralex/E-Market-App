using EMarketApp.Core.Domain.Common;
using EMarketApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMarketApp.Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Ads>? Ads { get; set; }
        public DbSet<Categories>? Categories { get; set; }
        public DbSet<Users>? Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreateBy = "DefaultAppUser";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API

            #region tables
            modelBuilder.Entity<Ads>().ToTable("Ads");
            modelBuilder.Entity<Categories>().ToTable("Categories");
            modelBuilder.Entity<Users>().ToTable("Users");
            #endregion

            #region "primary keys"
            modelBuilder.Entity<Ads>().HasKey(ad => ad.Id);
            modelBuilder.Entity<Categories>().HasKey(category => category.Id);
            modelBuilder.Entity<Users>().HasKey(user => user.Id);
            #endregion

            #region relationships

            modelBuilder.Entity<Users>()
                 .HasMany(user => user.Ads)
                 .WithOne(ad => ad.Users)
                 .HasForeignKey(ad => ad.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

          modelBuilder.Entity<Categories>()
                .HasMany(category => category.Ads)
                .WithOne(ad => ad.Categories)
                .HasForeignKey(ad => ad.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "property configurations"

            #region ads
            modelBuilder.Entity<Ads>()
                .Property(ad => ad.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Ads>()
                .Property(ad => ad.ImagePathOne)
                .IsRequired();

            modelBuilder.Entity<Ads>()
                .Property(ad => ad.ImagePathTwo);

            modelBuilder.Entity<Ads>()
                .Property(ad => ad.ImagePathThree);

            modelBuilder.Entity<Ads>()
                .Property(ad => ad.ImagePathFour);

            modelBuilder.Entity<Ads>()
                .Property(ad => ad.Description)
                .IsRequired();

            modelBuilder.Entity<Ads>()
                .Property(ad => ad.Price)
                .IsRequired();

            modelBuilder.Entity<Ads>()
                .Property(ad => ad.CategoryId)
                .IsRequired();
            #endregion

            #region categories
            modelBuilder.Entity<Categories>()
                .Property(categories => categories.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Categories>()
                .Property(categories => categories.Description)
                .IsRequired();
            #endregion

            #region users
            modelBuilder.Entity<Users>()
                .Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Users>()
                .Property(user => user.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Users>()
                .Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Users>()
                .Property(user => user.Phone)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Users>()
                .Property(user => user.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Users>()
                .Property(user => user.Password)
                .IsRequired()
                .HasMaxLength(50);
            #endregion

            #endregion
        }

    }
}
