using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class AddServiceModel
    {
        public Service Service { get; set; }
        public List<SelectListItem> ServiceTypes { get; set; }
    }
}
