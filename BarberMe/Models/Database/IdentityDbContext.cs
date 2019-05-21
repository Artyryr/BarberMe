using BarberMe.Models.Classes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.Database
{
    public class IdentityDbContext : IdentityDbContext<BarbershopUser>
    {
        /// <summary>
        /// Constructor for a IdentityDbContext class
        /// </summary>
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options) { }
    }
}
