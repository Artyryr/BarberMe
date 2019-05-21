using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models
{
    public class Service
    {
        ///<value>
        /// Id of a specific service
        ///</value>
        [Key]
        public int ServiceId { get; set; }

        ///<value>
        /// Id of a specific barbershop
        ///</value>
        [Required]
        public int BarbershopId { get; set; }

        ///<value>
        /// Service Name of a specific service
        ///</value>
        [Required]
        public string ServiceName { get; set; }

        ///<value>
        /// Description of a specific service
        ///</value>
        [Required]
        public string ServiceDescription { get; set; }

        ///<value>
        /// Id of service type
        ///</value>
        [Required]
        public int ServiceTypeId { get; set; }

        ///<value>
        /// Duration of a specific service
        ///</value>
        [Required]
        public int ServiceDuration { get; set; }

        ///<value>
        /// Price of a specific service
        ///</value>
        [Required]
        public double ServicePrice { get; set; }
    }
}
