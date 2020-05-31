using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtData.Models;

namespace TennisCourtData
{
    public interface ITennisDAL
    {
        Court GetCourt(int id);
        IEnumerable<Court> GetCourts();
        Court SetCourt(Court court);


        Slot GetSlot(int id);
        IEnumerable<Slot> GetSlots();
        Slot SetSlot(Slot slot);


        Booking GetBooking(int id);
        IEnumerable<Booking> GetBookings();
        Booking SetBooking(Booking booking);
        Booking DeleteBooking(int id);
    }
}
