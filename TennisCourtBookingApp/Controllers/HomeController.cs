using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisCourtData;
using Autofac;
using TennisCourtData.Models;
using TennisCourtBookingApp.Models;

namespace TennisCourtBookingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITennisDAL dal;

        public HomeController(ITennisDAL dal)
        {
            this.dal = dal;
        }

        public ActionResult Index()
        {
            var courts = dal.GetCourts();
            return View(courts);
        }

        public ActionResult CreateBooking(int courtId, int slotId)
        {
            var availableCourtSlots = dal.GetSlots(courtId).Where(s => s.booking == null);
            var selectItemSlots = availableCourtSlots.Select(s => new SelectListItem() { Text = s.name, Value = s.id.ToString(), Selected = s.id==slotId });
            var selectList = new SelectList(selectItemSlots, "Value", "Text", selectItemSlots.SingleOrDefault(s=>s.Selected)?.Value);

            var booking = new Booking() { slot = availableCourtSlots.FirstOrDefault(s => s.id == slotId) };
            var bookViewModel = new BookingViewModel()
            {
                booking = booking,
                availableSlots = selectList
            };

            return View("CreateEditBooking", bookViewModel);
        }

        [HttpPost]
        public ActionResult CreateBooking(BookingViewModel bookingViewModel)
        {
            if (bookingViewModel.selectedSlotId.HasValue) {
                var slot = dal.GetSlot(bookingViewModel.selectedSlotId.Value);
                if (slot != null)
                {
                    bookingViewModel.booking.slot = slot;
                    var book = dal.SetBooking(bookingViewModel.booking);
                    AddBookingTempDataMessage(book, "Created");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditBooking(int bookingId)
        {
            var booking = dal.GetBooking(bookingId);
            var availableCourtSlots = dal.GetSlots(booking.slot.court.id).Where(s => s.booking == null || s.booking.id == bookingId);
            var selectItemSlots = availableCourtSlots.Select(s => new SelectListItem() { Text = s.name, Value = s.id.ToString(), Selected = s.id == booking.slot.id });
            var selectList = new SelectList(selectItemSlots, "Value", "Text", selectItemSlots.SingleOrDefault(s => s.Selected)?.Value);

            var bookViewModel = new BookingViewModel()
            {
                booking = booking,
                availableSlots = selectList
            };
            return View("CreateEditBooking", bookViewModel);
        }

        [HttpPost]
        public ActionResult EditBooking(BookingViewModel bookingViewModel)
        {
            if (bookingViewModel.selectedSlotId.HasValue)
            {
                var slot = dal.GetSlot(bookingViewModel.selectedSlotId.Value);
                if (slot != null)
                {
                    bookingViewModel.booking.slot = slot;
                    var book = dal.SetBooking(bookingViewModel.booking);
                    AddBookingTempDataMessage(book, "Updated");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBooking(int bookingId)
        {
            var book = dal.DeleteBooking(bookingId);
            AddBookingTempDataMessage(book, "Deleted");
            return RedirectToAction("Index");
        }

        private void AddBookingTempDataMessage(Booking book, string action)
        {
            if (book != null)
                TempData["message"] = $"Booking {action} - Court:{book.slot.court.name} Slot:{book.slot.name} Name:{book.name}";
        }
    }
}