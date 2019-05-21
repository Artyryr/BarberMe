using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.Database
{
    public interface IServiceRepository
    {
        IQueryable<Barber> Barbers { get; }
        IQueryable<Barbershop> Barbershops { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<Payment> Payments { get; }
        IQueryable<Review> Reviews { get; }
        IQueryable<Schedule> Schedules { get; }
        IQueryable<Service> Service { get; }
        IQueryable<ServiceType> ServiceTypes { get; }
    }
}
