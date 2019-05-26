using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class OrdersPageModel
    {
        public List<Barber> Barbers { get; set; }
        public Dictionary<int,List<Order>> Orders { get; set; }
    }
}
