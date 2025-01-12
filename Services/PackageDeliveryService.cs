using EverestEngineering_CodeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EverestEngineering_CodeTest.Services
{
    public class PackageDeliveryService
    {
        private readonly PackageDBContext _dbContext;

        public PackageDeliveryService(PackageDBContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// To estimate the total delivery cost of each package with an offer code(if applicable).
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Package GetTotalDeliveryCostofPackage(PackagePayloadModel data)
        {
            try
            {
                var package = data.Package;
                var offerData = data.PackageOffers.Where(a => a.MinWeight <= package.PackageWeight && a.MaxWeight >= package.PackageWeight
               && a.MinDistance <= package.Distance && a.MaxDistance >= package.Distance).FirstOrDefault();
                package.DeliveryCost = (data.BaseDeliveryCost + (package.PackageWeight * 10) + (package.Distance * 5));
                if (offerData != null)
                {
                    if (offerData.Name == package.OfferCode)
                    {
                        package.Discount = (package.DeliveryCost * offerData.OfferValue) / 100;
                    }
                }
                package.TotalCost = (package.DeliveryCost - package.Discount);
                return package;
            }
            catch (Exception ex) { 
              Console.WriteLine(ex.Message);
                throw;
            }
            
        }


        /// <summary>
        /// To calculate estimated delivery time for every package by maximizing no.of packages inevery shipment.
        /// </summary>
        /// <param name="data"></param>
        public void CalculateDeliveryTimes(PackagePayloadModel data)
        {
            try
            {
                var packages = data.Packages;
                for (int i = 0; i < packages.Count; i++)
                {
                    PackagePayloadModel payloadData = new PackagePayloadModel();
                    payloadData.Package = packages[i];
                    payloadData.PackageOffers = data.PackageOffers.ToList();
                    payloadData.BaseDeliveryCost = data.BaseDeliveryCost;
                    packages[i] = GetTotalDeliveryCostofPackage(payloadData);
                }

                var sortedPackages = packages.OrderByDescending(p => p.PackageWeight)
                                              .ThenBy(p => p.Distance)
                                              .ThenBy(p => p.Id)
                                              .ToList();

                double currentTime = 0;
                List<Vehicle> vehicles = Enumerable.Range(0, data.numVehicles).Select(i => new Vehicle()).ToList();

                while (sortedPackages.Any())
                {
                    foreach (var vehicle in vehicles)
                    {
                        if (!sortedPackages.Any()) break;

                        var shipment = new List<Package>();
                        int currentWeight = 0;

                        foreach (var pkg in sortedPackages.ToList())
                        {
                            if (currentWeight + pkg.PackageWeight <= data.maxCarriableWeight)
                            {
                                shipment.Add(pkg);
                                currentWeight += pkg.PackageWeight;
                                sortedPackages.Remove(pkg);
                            }
                        }

                        if (shipment.Any())
                        {
                            int maxDistance = shipment.Max(p => p.Distance);
                            double tripTime = 2 * ((double)maxDistance / data.maxSpeed); // Round Trip
                            currentTime = Math.Max(currentTime, vehicle.NextAvailableTime);

                            foreach (var pkg in shipment)
                            {
                                pkg.EstimatedDeliveryTime = currentTime + (double)pkg.Distance / data.maxSpeed;
                            }

                            vehicle.NextAvailableTime = currentTime + tripTime;
                        }
                    }
                }
            }
            catch (Exception ex) { 
              Console.WriteLine($"{ex.Message}");
                throw;
            }
            
        }



    }
}
