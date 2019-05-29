using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class SearchModel
    {
        public string SearchText { get; set; }
        public List<Barbershop> Barbershops { get; set; }
        public List<Barber> Barbers { get; set; }
        public string SearchCriteria { get; set; }
    }
}
