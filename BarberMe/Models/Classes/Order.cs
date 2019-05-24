using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public Service Service { get; set; }

        public Schedule Schedule { get; set; }

        public Payment Payment { get; set; }
        public Barber Barber { get; set; }
        public Barbershop Barbershop { get; set; }

        public double Price { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
