using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EverestEngineering_CodeTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EverestEngineering_CodeTest
{
    public class PackageDBContext : DbContext
    {
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageOffer> PackageOffers { get; set; }
        public DbSet<PackageConfiguration> PackageConfigurations { get; set; }
        public PackageDBContext(DbContextOptions<PackageDBContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PackageOffer>().HasData(
                new PackageOffer { Id = 1, Name = "OFR001", MinDistance = 0,MaxDistance =200,MinWeight=70,MaxWeight=200,OfferValue=10},
                new PackageOffer { Id = 2, Name= "OFR002", MinDistance = 50, MaxDistance = 150, MinWeight = 100, MaxWeight = 250, OfferValue = 7 },
                new PackageOffer { Id = 3, Name= "OFR003", MinDistance = 50, MaxDistance = 250, MinWeight = 10, MaxWeight = 150, OfferValue = 5 }  
                );

            modelBuilder.Entity<PackageConfiguration>().HasData(
                new PackageConfiguration {Id = 1,Name="BaseDeliveryCost",Value=100 });
            base.OnModelCreating(modelBuilder);
        }
    }
}
