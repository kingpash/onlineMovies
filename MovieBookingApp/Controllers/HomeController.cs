using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieBookingApp.Models;
using MovieBookingApp.Models.viewModels;
using MovieBookingApp.Data;
using Microsoft.AspNetCore.Identity;

namespace MovieBookingApp.Controllers
{
    public class HomeController : Controller
    {
        int count = 1;
        bool flag = true;

        private UserManager<ApplicationUser> _usermanager;

        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }

        public IActionResult Index()
        {
            var getMovieList = _context.MovieDetails.ToList();
            return View(getMovieList);
        }

        [HttpGet]
        public IActionResult BookNow(int Id)
        {
            BookNowViewModel vm = new BookNowViewModel();
            var item = _context.MovieDetails.Where(a => a.Id == Id).FirstOrDefault();
            vm.Movie_Name = item.Movie_Name;
            vm.Movie_Date = item.DateAndTime;
            vm.MovieId = item.Id;

            return View(vm);
        }
        [HttpPost]
        public IActionResult BookNow(BookNowViewModel vm)
        {
            List<BookingTable> booking = new List<BookingTable>();
            List<Cart> carts = new List<Cart>();
            string seatno = vm.SeatNo.ToString();
            int movieID = vm.MovieId;

            string[] seatnoArray = seatno.Split(',');
            count = seatnoArray.Length;

            if (checkedseat(seatno, movieID) == false)
            {
                foreach (var item in seatnoArray)
                {
                    carts.Add(new Cart
                    {
                        Amount = 150,
                        MovieId = vm.MovieId,
                        UserId = _usermanager.GetUserId(HttpContext.User),
                        Date = vm.Movie_Date,
                        seatno = item
                    });
                }
                foreach (var item in carts)
                {
                    _context.Cart.Add(item);
                    _context.SaveChanges();
                }
                TempData["Success"] = "Seat number booked! Check your cart for details.";
            }
            else
            {
                TempData["seatnomsg"] = "Seat number taken, please select a different one.";
            }
            return RedirectToAction("BookNow");
        }
        private bool checkedseat(string seatno, int movieID)
        {
            //throw new NotImplementedException();
            string seats = seatno;
            string[] seatreserve = seats.Split(',');
            var seatnolist = _context.BookingTable.Where(a => a.MovieDetailsId == movieID).ToList();
            foreach (var item in seatnolist)
            {
                string alreadybooked = item.seatno;
                foreach (var item1 in seatreserve)
                {
                    if (item1 == alreadybooked)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public IActionResult Checakseat(DateTime Movie_Date, BookNowViewModel booknow)
        {
            string seatno = string.Empty;
            var movielist = _context.BookingTable.Where(a => a.Datetopresent == Movie_Date).ToList();
            if(movielist != null)
            {
                var getseatno = movielist.Where(b => b.MovieDetailsId == booknow.MovieId).ToList();
                if (getseatno != null)
                {
                    foreach (var item in getseatno)
                    {
                        seatno = seatno = " " + item.seatno.ToString();
                    }
                    TempData["SNO"] = "Already Booked " + seatno;
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
