using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class LoyalityProg
    {
        public int Id { get; set; }
        public int RequiredPoints { get; set; }
        public decimal Discount { get; set; }
    }
}
