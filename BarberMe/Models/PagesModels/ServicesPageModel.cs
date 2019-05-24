using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class ServicesPageModel
    {
        public List<Service> Services { get; set; }
        public Barbershop Barbershop { get; set; }
    }
}
