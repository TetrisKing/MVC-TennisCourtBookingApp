using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtData.Models;

namespace TennisCourtData
{
    public class MockTennisDAL : ITennisDAL
    {
        private List<Court> courts;
        private List<Slot> slots;
        private List<Booking> bookings;
        public MockTennisDAL()
        {
            courts = new List<Court>();
            slots = new List<Slot>();
            bookings = new List<Booking>();
            DummyData.SetupDummyData(ref courts, ref slots, ref bookings, 3, 8, 10);
        }

        public Booking DeleteBooking(int id)
        {
            var booking = bookings.Fetch(id);
            if (booking != null)
            {
                bookings.Remove(booking);
                booking.slot.booking = null;
            }
            return booking;
        }

        public Booking GetBooking(int id)
        {
            return bookings.Fetch(id);
        }

        public IEnumerable<Booking> GetBookings()
        {
            return bookings;
        }

        public Court GetCourt(int id)
        {
            return courts.Fetch(id);
        }

        public IEnumerable<Court> GetCourts()
        {
            return courts;
        }

        public Slot GetSlot(int id)
        {
            return slots.Fetch(id);
        }

        public IEnumerable<Slot> GetSlots(int? courtId = null)
        {            
            return courtId == null || !courtId.HasValue ? slots : slots.Where(s => s.court.id == courtId);
        }

        public Booking SetBooking(Booking booking)
        {
            var existing = bookings.Fetch(booking.id);
            if (existing == null)
            {
                booking.id = bookings.Any() ? bookings.Max(b => b.id) + 1 : 1000;
                booking.slot = GetSlot(booking.slot.id);
                booking.slot.booking = booking;
                bookings.Add(booking);

                existing = bookings.Fetch(booking.id);
            }
            else {
                existing.Update(booking);
                if (existing.slot.id != booking.slot.id)
                {
                    existing.slot.booking = null;
                    existing.slot = GetSlot(booking.slot.id);
                    existing.slot.booking = existing;
                }
            }
            return existing;
        }

        public Court SetCourt(Court court)
        {
            var existing = courts.Fetch(court.id);
            if (existing == null)
            {
                court.id = courts.Any() ? courts.Max(b => b.id + 1000) + 1 : 1000;
                courts.Add(court);
            }
            else
            {
                courts.Remove(existing);
                courts.Add(court);
            }
            return court;
        }

        public Slot SetSlot(Slot slot)
        {
            var existing = slots.Fetch(slot.id);
            if (existing == null)
            {
                slot.id = slots.Any() ? slots.Max(b => b.id + 1000) + 1: 1000;
                slots.Add(slot);
            }
            else
            {
                slots.Remove(existing);
                slots.Add(slot);
            }
            return slot;
        }

    }
}
