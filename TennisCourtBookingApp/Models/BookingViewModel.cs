using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisCourtData.Models;

namespace TennisCourtBookingApp.Models
{
    public class BookingViewModel
    {
        public Booking booking { get; set; }
        public List<Slot> availableSlots { get; set; }
    }
}