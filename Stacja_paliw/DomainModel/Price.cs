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
        public int Pb95 { get; set; }
        [Required]
        public int Pb98 { get; set; }
        [Required]
        public int Lpg { get; set; }
        [Required]
        public int On { get; set; }
        [Required]
        public int Wash { get; set; }
        [Required]
        public int Waxing { get; set; }
    }
}
