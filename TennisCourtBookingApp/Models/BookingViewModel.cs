using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisCourtData.Models;

namespace TennisCourtBookingApp.Models
{
    public class BookingViewModel
    {
        public Booking booking { get; set; }
        public SelectList availableSlots { get; set; }
        public int? selectedSlotId { get; set; }
    }
}