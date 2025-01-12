using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverestEngineering_CodeTest.Models
{
    public class PackageOffer
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int MaxDistance {  get; set; }
        public int MinDistance { get; set; }
        public int MaxWeight {  get; set; }
        public int MinWeight { get; set; }
        public int OfferValue {  get; set; }

    }
}
