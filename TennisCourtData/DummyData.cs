using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtData.Models;

namespace TennisCourtData
{
    public static class DummyData
    {
        public static void SetupDummyData(ref List<Court> courts, ref List<Slot> slots, ref List<Booking> bookings, int courtCount, int slotCount, int bookingCount)
        {
            var newCourts = new List<Court>();
            var newSlots = new List<Slot>();
            var newBookings = new List<Booking>();

            if (bookingCount > courtCount * slotCount)
                bookingCount = courtCount * slotCount;

            //Create Base Data
            newCourts = CreateCourts(courtCount).ToList();

            foreach (Court c in newCourts)
            {
                var cSlots = CreateSlots(c, slotCount);
                newSlots.AddRange(cSlots);
            }

            for (int i = 0; i < bookingCount; i++)
            {
                var slot = RandomEntity(newSlots.Where(s => s.booking == null));
                var booking = CreateBooking(slot);
                newBookings.Add(booking);
            }

            //Link Data
            foreach (var court in newCourts)
            {
                court.slots = newSlots.Where(s => s.court.id == court.id);
            }

            courts.AddRange(newCourts);
            slots.AddRange(newSlots);
            bookings.AddRange(newBookings);
        }

        private static T RandomEntity<T>(IEnumerable<T> entities)
        {
            var rnd = new Random(entities.Count());
            int rndNum = rnd.Next(0, entities.Count() - 1);
            return entities.ElementAt(rndNum);
        }

        private static IEnumerable<Court> CreateCourts(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Court()
                {
                    id = 1000 + (i * 1000),
                    name = $"Court-{i}",
                    slots = new List<Slot>()
                };
            }
        }

        private static IEnumerable<Slot> CreateSlots(Court court, int count)
        {
            int prefix = court.id;
            for (int i = 0; i < count; i++)
            {
                yield return new Slot()
                {
                    id = prefix + i,
                    name = $"Slot-{prefix + i}",
                    court = court,
                    order = i
                };
            }
        }

        private static Booking CreateBooking(Slot slot)
        {
            int prefix = slot.id;
            var booking = new Booking()
            {
                id = prefix,
                name = $"Booking-{prefix}",
                slot = slot
            };
            slot.booking = booking;
            return booking;
        }
    }
}
