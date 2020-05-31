using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisCourtData.Models
{
    public class Booking
    {
        public int id { get; set; }
        [System.ComponentModel.DisplayName("Booking")]
        public string name { get; set; }
        public Slot slot { get; set; }

        public Booking()
        {
            id = -1;
        }

        public void Update(Booking booking) {
            name = booking.name;
        }
    }
}