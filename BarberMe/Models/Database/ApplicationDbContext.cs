using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.Database
{
    /// <summary>  
    ///  This provides a representation of database tables in App.
    /// </summary>  
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Basic constructor for class
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        /// <summary>
        /// Representation Of Barbers table
        /// </summary>
        public DbSet<Barber> Barbers { get; set; }

        /// <summary>
        /// Representation Of Barbershops table
        /// </summary>
        public DbSet<Barbershop> Barbershops { get; set; }

        /// <summary>
        /// Representation Of Barbers table
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Representation Of Services table
        /// </summary>
        public DbSet<Service> Services { get; set; }

        /// <summary>
        /// Representation Of ServiceTypes table
        /// </summary>
        public DbSet<ServiceType> ServiceTypes { get; set; }

        /// <summary>
        /// Representation Of Schedules table
        /// </summary>
        public DbSet<Schedule> Schedules { get; set; }

        /// <summary>
        /// Representation Of Reviews table
        /// </summary>
        public DbSet<Review> Reviews { get; set; }

        /// <summary>
        /// Representation Of Payments table
        /// </summary>
        public DbSet<Payment> Payments { get; set; }
    }
}

