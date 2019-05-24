using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class EditBarbershopModel
    {
        public Barbershop Barbershop { get; set; }
        public IFormFile barbershopImage {get;set;}
    }
}
