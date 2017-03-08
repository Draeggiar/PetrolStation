using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Price
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Pb95 { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Pb98 { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Lpg { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal On { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Wash { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Waxing { get; set; }
    }
}
