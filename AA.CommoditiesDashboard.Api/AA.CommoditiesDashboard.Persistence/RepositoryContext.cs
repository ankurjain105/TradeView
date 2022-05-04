using AA.CommoditiesDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AA.CommoditiesDashboard.Persistence
{
    public class RepositoryContext : DbContext, IDbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        public DbSet<CommodityModel> CommodityModels { get; set; }
        public DbSet<CommodityModelTrade> Trades { get; set; }
        public DbSet<CommodityModelValuation> Valuations { get; set; }
        public DbSet<CommodityModelPosition> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>().ToTable("Model", "model");
            modelBuilder.Entity<Commodity>().ToTable("Commodity", "model");

            modelBuilder.Entity<CommodityContract>().ToTable("CommodityContract", "model");
            
            var builder = modelBuilder.Entity<CommodityModel>().ToTable("CommodityModel", "model");
            builder.HasOne(x => x.Commodity).WithMany().HasForeignKey("CommodityId");
            builder.HasOne(x => x.Model).WithMany().HasForeignKey("ModelId");
            
            modelBuilder.Entity<CommodityModelTrade>().ToTable("CommodityModelTrade", "trade")
                .HasOne(x => x.CommodityContract).WithMany().HasForeignKey("CommodityContractId");
            
            modelBuilder.Entity<CommodityModelPosition>().ToTable("CommodityModelPosition", "trade");
            
            modelBuilder.Entity<CommodityModelValuation>().ToTable("CommodityModelValuation", "trade")
                .HasOne(x => x.CommodityModel).WithMany().HasForeignKey("CommodityModelId"); ;
        }
    }
}