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
        public Service ServiceId { get; set; }
        public Schedule Schedule { get; set; }
        public Payment Payment { get; set; }
        public double Price { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
