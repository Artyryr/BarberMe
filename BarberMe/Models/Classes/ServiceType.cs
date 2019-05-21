using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models
{
    public class ServiceType
    {
        ///<value>
        /// id of a service type
        ///</value>
        [Key]
        public int ServiceTypeId { get; set; }

        ///<value>
        /// Name of a service type
        ///</value>
        [Required]
        public string ServiceTypeName { get; set; }

        ///<value>
        /// Service type description
        ///</value>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Basic constructor for a service Type
        /// </summary>
        public ServiceType()
        {

        }

        /// <summary>
        /// Service type constructor 
        /// </summary>
        /// <param name="serviceTypeName">Name of type</param>
        /// <param name="description">Description </param>
        public ServiceType(string serviceTypeName, string description)
        {
            ServiceTypeName = serviceTypeName;
            Description = description;
        }
    }
}
