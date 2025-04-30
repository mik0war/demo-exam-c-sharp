using Microsoft.EntityFrameworkCore;

namespace demo_exam
{

    public class AppDbContext : DbContext
    {
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PartnerProduct> PartnerProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;" +
                                     "Port=5432;" +
                                     "Database=partners_demo;" +
                                     "Username=postgres;" +
                                     "Password=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
               
            modelBuilder.Entity<Partner>(entity =>
            {
                entity.ToTable("partners");
                entity.Property(p => p.Name)
                .HasColumnName("name");
                entity.Property(p => p.Rating)
                .HasColumnName("rating");
                entity.Property(p => p.Phone)
                .HasColumnName("phone");
                entity.Property(p => p.Email)
                .HasColumnName("e_mail");
                entity.Property(p => p.DirectorSurname)
                .HasColumnName("director_surname");
                entity.Property(p => p.DirectorLastName)
                .HasColumnName("director_last_name");
                entity.Property(p => p.DirectorFirstName)
                .HasColumnName("director_name");
                entity.Property(p => p.Type)
                .HasColumnName("partner_type");
                entity.Property(p => p.Inn)
                .HasColumnName("inn");
                entity.Property(p => p.Address)
                .HasColumnName("address");
                entity.Property(p => p.Id)
                .HasColumnName("id");
            });
        

            modelBuilder.Entity<PartnerProduct>(entity =>
            {
                entity.ToTable("partner_products");
                entity.HasKey(pp => pp.Id);

                entity.Property(pp => pp.PartnerId).HasColumnName("partner");
                entity.Property(pp => pp.ProductCount).HasColumnName("product_count");

            });

        }
    }
    }
