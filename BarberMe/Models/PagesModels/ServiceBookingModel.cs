using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class ServiceBookingModel
    {
        public Service Service { get; set; }
        public Barber Barber { get; set; }
        public Order Order { get; set; }
        public Barbershop Barbershop { get; set; } 
        public Schedule Schedule { get; set; }
        public List<Barber> Barbers { get; set; }
        public List<Service> Services { get; set; }
        public List<List<Schedule>> RelevantSchedules { get; set; }
        public Payment Payment { get; set; }

        public int BarbershopId { get; set; }
        public int BarberId { get; set; }
        public int ServiceId { get; set; }
        public int ScheduleId { get; set; }
        public int OrderId { get; set; }
    }
}
