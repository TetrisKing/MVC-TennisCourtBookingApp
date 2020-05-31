using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisCourtData.Models
{
    public class Slot
    {
        public int id { get; set; }
        [System.ComponentModel.DisplayName("Slot")]
        public string name { get; set; }
        public int order { get; set; }
        public Court court { get; set; }
        public Booking booking { get; set; }

        public Slot()
        {
            id = -1;
        }

        public void Update(Slot slot)
        {
            name = slot.name;
        }
    }
}