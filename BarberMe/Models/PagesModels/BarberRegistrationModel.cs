using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class BarberRegistrationModel
    {
        public Barber Barber { get; set; }
        public int barbershopId { get; set; }
        public IFormFile BarberImage { get; set; }
    }
}
