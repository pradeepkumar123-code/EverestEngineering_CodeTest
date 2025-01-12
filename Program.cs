using EverestEngineering_CodeTest.Models;
using EverestEngineering_CodeTest.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EverestEngineering_CodeTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddDbContext<PackageDBContext>(options => options.UseInMemoryDatabase("PackageDb"))
            .BuildServiceProvider();

            using var context = serviceProvider.GetRequiredService<PackageDBContext>();
            context.Database.EnsureCreated();

            var packageService = new PackageDeliveryService(context);

            //to estimate the total delivery cost of each package with an offer code(if applicable).
            Console.WriteLine("Enter Base Delivery Cost and Number of Packages:");
            string[] firstLine = Console.ReadLine().Split();
            decimal baseDeliveryCost = decimal.Parse(firstLine[0]);
            int numPackages = int.Parse(firstLine[1]);

            List<PackageOffer> packageOffers = context.PackageOffers.ToList();
            List<Package> packages = new List<Package>();
            Console.WriteLine("Enter Package Details (ID Weight Distance OfferCode):");
            for (int i = 0; i < numPackages; i++)
            {
                string[] pkgInput = Console.ReadLine().Split();
                packages.Add(new Package
                {
                    Name = pkgInput[0],
                    PackageWeight = int.Parse(pkgInput[1]),
                    Distance = int.Parse(pkgInput[2]),
                    OfferCode = pkgInput[3]
                });
            }

            for (int i = 0; i < packages.Count; i++) {
                PackagePayloadModel data = new PackagePayloadModel();
                data.Package = packages[i];
                data.PackageOffers = packageOffers.ToList();
                data.BaseDeliveryCost = baseDeliveryCost;
                packages[i] = packageService.GetTotalDeliveryCostofPackage(data);
                Console.WriteLine($"Package ID : {packages[i].Name} , Discount : {packages[i].Discount},Total Cost : {packages[i].TotalCost}");
            }

            //to calculate the estimated delivery time for every package by maximizing no.of packages in every shipment.
            Console.WriteLine("Enter Number of Vehicles, Max Speed, and Max Carriable Weight:");
            string[] vehicleDetails = Console.ReadLine().Split();
            int numVehicles = int.Parse(vehicleDetails[0]);
            int maxSpeed = int.Parse(vehicleDetails[1]);
            int maxWeight = int.Parse(vehicleDetails[2]);

            PackagePayloadModel payloadData = new PackagePayloadModel();
            payloadData.Packages = packages;
            payloadData.PackageOffers = packageOffers.ToList();
            payloadData.BaseDeliveryCost = baseDeliveryCost;
            payloadData.numVehicles = numVehicles;
            payloadData.maxSpeed = maxSpeed;
            payloadData.maxCarriableWeight = maxWeight;
            packageService.CalculateDeliveryTimes(payloadData);

            foreach (var pkg in packages.OrderBy(p => p.EstimatedDeliveryTime))
            {
                Console.WriteLine($"Package ID : {pkg.Name} , Discount : {pkg.Discount}, Total Cost : {pkg.TotalCost}, Estimated Delivery Time : {pkg.EstimatedDeliveryTime}");
            }


        }
    }
}
