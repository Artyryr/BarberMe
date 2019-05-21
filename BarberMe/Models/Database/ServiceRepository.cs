using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.Database
{
    public class ServiceRepository : IServiceRepository
    {
        private ApplicationDbContext context;
        public ServiceRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Barber> Barbers => context.Barbers;

        public IQueryable<Barbershop> Barbershops => context.Barbershops;

        public IQueryable<Order> Orders => context.Orders;

        public IQueryable<Payment> Payments => context.Payments;

        public IQueryable<Review> Reviews => context.Reviews;

        public IQueryable<Schedule> Schedules => context.Schedules;

        public IQueryable<Service> Service => context.Services;

        public IQueryable<ServiceType> ServiceTypes => context.ServiceTypes;
    }
}
