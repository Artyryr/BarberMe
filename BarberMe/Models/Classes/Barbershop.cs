using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models
{
    public class Barbershop
    {

        [Key]
        public int BarbershopId { get; set; }
        [Required]
        public string BarbershopUserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Description { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Geoposition { get; set; }
        public string PhotoLink { get; set; }
        public List<Barber> Barbers { get; set; }
        public List<Service> Services{ get; set; }
    }
}
