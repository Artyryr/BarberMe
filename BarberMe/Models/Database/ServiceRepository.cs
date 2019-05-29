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

        public void AddBarber(Barber barber)
        {
            if (barber.BarberId == 0)
            {
                context.Barbers.Add(barber);
            }
            else
            {
                Barber dbEntry = context.Barbers
                    .FirstOrDefault(p => p.BarberId == barber.BarberId);
                if (dbEntry != null)
                {
                    dbEntry.BarbershopId = barber.BarbershopId;
                    dbEntry.Email = barber.Email;
                    dbEntry.Facebook = barber.Facebook;
                    dbEntry.Instagram = barber.Instagram;
                    dbEntry.FirstName = barber.FirstName;
                    dbEntry.LastName = barber.LastName;
                    dbEntry.PhotoLink = barber.PhotoLink;
                    dbEntry.Telephone = barber.Telephone;

                    //dbEntry.Schedule = barber.Schedule;
                    //dbEntry.Reviews = barber.Reviews;
                }
            }
            context.SaveChanges();
        }

        public void RemoveBarber(int id)
        {
            if (context.Barbers.Where(p => p.BarberId == id).FirstOrDefault() != null)
            {
                context.Barbers.Remove(context.Barbers.Where(p => p.BarberId == id).FirstOrDefault());
            }
            context.SaveChanges();
        }

        public void AddBarbershop(Barbershop barbershop)
        {
            if (barbershop.BarbershopId == 0)
            {
                context.Barbershops.Add(barbershop);
            }
            else
            {
                Barbershop dbEntry = context.Barbershops
                    .FirstOrDefault(p => p.BarbershopId == barbershop.BarbershopId);
                if (dbEntry != null)
                {  
                    dbEntry.Email = barbershop.Email;
                    dbEntry.Name = barbershop.Name;
                    dbEntry.Address = barbershop.Address;
                    dbEntry.Telephone = barbershop.Telephone;
                    dbEntry.Description = barbershop.Description;
                    dbEntry.Facebook = barbershop.Facebook;
                    dbEntry.Instagram = barbershop.Instagram;
                    dbEntry.Geoposition = barbershop.Geoposition;
                    dbEntry.PhotoLink = barbershop.PhotoLink;
                    dbEntry.Services = barbershop.Services;
                    dbEntry.BarbershopUserId = barbershop.BarbershopUserId;
                    dbEntry.Barbers = barbershop.Barbers;
                }
            }
            context.SaveChanges();
        }

        public void AddOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            else
            {
                Order dbEntry = context.Orders
                    .FirstOrDefault(p => p.OrderId == order.OrderId);
                if (dbEntry != null)
                {
                    dbEntry.Price = order.Price;
                    dbEntry.FirstName = order.FirstName;
                    dbEntry.LastName = order.LastName;
                    dbEntry.Telephone = order.Telephone;
                    dbEntry.Email = order.Email;
                    dbEntry.Barber = order.Barber;
                    dbEntry.Barbershop = order.Barbershop;
                    dbEntry.Service = order.Service;
                    dbEntry.Schedule = order.Schedule;
                    dbEntry.Payment = order.Payment;
                }
            }
            context.SaveChanges();
        }
        public void AddPayment(Payment payment)
        {
            if (payment.PaymentId == 0)
            {
                context.Payments.Add(payment);
            }
            else
            {
                Payment dbEntry = context.Payments
                    .FirstOrDefault(p => p.PaymentId == payment.PaymentId);
                if (dbEntry != null)
                {

                    dbEntry.CardNumber = payment.CardNumber;
                    dbEntry.ExpiryDate = payment.ExpiryDate;
                    dbEntry.CVV = payment.CVV;
                    dbEntry.CardOwner = payment.CardOwner;
                }
            }
            context.SaveChanges();
        }
        public void AddReview(Review review)
        {
            if (review.ReviewId == 0)
            {
                context.Reviews.Add(review);
            }
            else
            {
                Review dbEntry = context.Reviews
                    .FirstOrDefault(p => p.ReviewId == review.ReviewId);
                if (dbEntry != null)
                {
                    dbEntry.BarberId = review.BarberId;
                    dbEntry.UserName = review.UserName;
                    dbEntry.Email = review.Email;
                    dbEntry.Rating = review.Rating;
                    dbEntry.Description = review.Description;
                }
            }
            context.SaveChanges();
        }

        public void AddSchedule(Schedule schedule)
        {
            if (schedule.ScheduleId == 0)
            {
                context.Schedules.Add(schedule);
            }
            else
            {
                Schedule dbEntry = context.Schedules
                    .FirstOrDefault(p => p.ScheduleId == schedule.ScheduleId);
                if (dbEntry != null)
                {

                    dbEntry.BarberId = schedule.BarberId;
                    dbEntry.Date = schedule.Date;
                    dbEntry.Time = schedule.Time;
                    dbEntry.Availability = schedule.Availability;
                }
            }
            context.SaveChanges();
        }

        public void AddListSchedule(List<Schedule> schedules)
        {
            foreach (var schedule in schedules)
            {
                if (schedule.ScheduleId == 0)
                {
                    context.Schedules.Add(schedule);
                }
                else
                {
                    Schedule dbEntry = context.Schedules
                        .FirstOrDefault(p => p.ScheduleId == schedule.ScheduleId);
                    if (dbEntry != null)
                    {

                        dbEntry.BarberId = schedule.BarberId;
                        dbEntry.Date = schedule.Date;
                        dbEntry.Time = schedule.Time;
                        dbEntry.Availability = schedule.Availability;
                    }
                }
                context.SaveChanges();
            }
        }

        public void AddService(Service service)
        {
            if (service.ServiceId == 0)
            {
                context.Services.Add(service);
            }
            else
            {
                Service dbEntry = context.Services
                    .FirstOrDefault(p => p.ServiceId == service.ServiceId);
                if (dbEntry != null)
                {

                    dbEntry.BarbershopId = service.BarbershopId;
                    dbEntry.ServiceName = service.ServiceName;
                    dbEntry.ServiceDescription = service.ServiceDescription;
                    dbEntry.ServiceTypeId = service.ServiceTypeId;
                    dbEntry.ServiceDuration = service.ServiceDuration;
                    dbEntry.ServicePrice = service.ServicePrice;
                }
            }
            context.SaveChanges();
        }

        public void RemoveService(int id)
        {
            if (context.Services.Where(p => p.ServiceId == id).FirstOrDefault() != null)
            {
                context.Services.Remove(context.Services.Where(p => p.ServiceId == id).FirstOrDefault());
            }
            context.SaveChanges();
        }

        public void AddServiceType(ServiceType type)
        {
            if (type.ServiceTypeId == 0)
            {
                context.ServiceTypes.Add(type);
            }
            else
            {
                ServiceType dbEntry = context.ServiceTypes
                    .FirstOrDefault(p => p.ServiceTypeId == type.ServiceTypeId);
                if (dbEntry != null)
                {

                    dbEntry.ServiceTypeName = type.ServiceTypeName;
                    dbEntry.Description = type.Description;
                }
            }
            context.SaveChanges();
        }
    }
}
