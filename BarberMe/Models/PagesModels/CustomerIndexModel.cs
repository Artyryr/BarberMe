using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class CustomerIndexModel
    {
        public IEnumerable<Barbershop> Barbershops { get; set; }
    }
}
