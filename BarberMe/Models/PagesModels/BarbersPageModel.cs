using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class BarbersPageModel
    {
        public List<Barber> Barbers { get; set; }
        public Barbershop Barbershop { get; set; }
    }
}
