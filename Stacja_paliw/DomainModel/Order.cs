using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public enum FuelType
    {
        Pb95,
        Pb98,
        Lpg,
        On
    }

    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public FuelType Fuel { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        //dostawca? z bazy danych
    }
}
