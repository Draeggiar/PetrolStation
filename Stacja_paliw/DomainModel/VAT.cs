using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class VAT
    {
        public int Id { get; set; }
        public int NoVAT { get; set; }
        public DateTime Date { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string Address { get; set; }
        public long NIP { get; set; }
        public string ProductsAmountOrServices { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPriceNet { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
