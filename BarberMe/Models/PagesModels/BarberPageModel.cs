using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class BarberPageModel
    {
        public Barber Barber { get; set; }
        public List<Review> Reviews { get; set; }
        public Barbershop Barbershop { get; set; }
    }
}
