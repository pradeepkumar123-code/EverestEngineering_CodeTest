using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverestEngineering_CodeTest.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Discount {  get; set; }  

        public decimal DeliveryCost {  get; set; }
        public decimal TotalCost { get; set; }
        public string PackageId { get; set; }
        public int PackageWeight { get; set; }
        public int Distance { get; set; }
        public string OfferCode { get; set; }

        public double EstimatedDeliveryTime {  get; set; }  

    }
}
