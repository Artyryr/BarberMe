using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models
{
    public class Barber
    {
        [Key]
        public int BarberId { get; set; }
        public int BarbershopId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Email { get; set; }
        
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string PhotoLink { get; set; }
        public List<Schedule> Schedule { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
