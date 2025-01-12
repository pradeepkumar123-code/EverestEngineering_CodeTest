using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverestEngineering_CodeTest.Models
{
    public class PackagePayloadModel
    {
        public Package Package { get; set; }
        public List<Package> Packages { get; set; }
        public List<PackageOffer> PackageOffers { get; set; }
        public decimal BaseDeliveryCost {  get; set; }
        public int numVehicles { get; set; }
        public int maxSpeed {  get; set; }  
        public int maxCarriableWeight {  get; set; }    

    }
}
