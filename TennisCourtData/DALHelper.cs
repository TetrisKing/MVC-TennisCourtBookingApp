using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtData.Models;

namespace TennisCourtData
{
    public static class DALHelper
    {
        public static Court Fetch(this List<Court> entities, int id) => entities.FirstOrDefault(e => e.id == id);
        public static Slot Fetch(this List<Slot> entities, int id) => entities.FirstOrDefault(e => e.id == id);
        public static Booking Fetch(this List<Booking> entities, int id) => entities.FirstOrDefault(e => e.id == id);
    }
}
