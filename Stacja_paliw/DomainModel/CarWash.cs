using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class CarWash
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Position { get; set; }
        [Required]
        //FIX ME: pewnie jakis inny typ
        public int ServiceType { get; set; }
        //TODO: dodac id klienta po ogarnieciu uzytkownikow

    }
}
