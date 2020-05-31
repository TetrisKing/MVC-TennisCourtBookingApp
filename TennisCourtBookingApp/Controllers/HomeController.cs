using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisCourtData;
using Autofac;
using TennisCourtData.Models;

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

        public ActionResult CreateBooking(int slotId)
        {
            var slot = dal.GetSlot(slotId);
            var booking = new Booking() { slot = slot };
            return View("CreateEditBooking", booking);
        }

        [HttpPost]
        public ActionResult CreateBooking(Booking booking)
        {
            var book = dal.SetBooking(booking);
            AddBookingTempDataMessage(book, "Created");
            return RedirectToAction("Index");
        }

        public ActionResult EditBooking(int bookingId)
        {
            var booking = dal.GetBooking(bookingId);
            return View("CreateEditBooking", booking);
        }

        [HttpPost]
        public ActionResult EditBooking(Booking booking)
        {
            var book = dal.SetBooking(booking);
            AddBookingTempDataMessage(book, "Updated");
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