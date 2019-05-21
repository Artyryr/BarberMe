using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class RegistrationModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmationPassword { get; set; }

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
        public IFormFile barbershopImage { get; set; }
    }
}
