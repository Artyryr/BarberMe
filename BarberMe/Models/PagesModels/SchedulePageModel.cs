using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.PagesModels
{
    public class SchedulePageModel
    {
        public Dictionary<int, List<List<Schedule>>> AllSchedules { get; set; }
        public List<Barber> Barbers { get; set; }
    }
}
