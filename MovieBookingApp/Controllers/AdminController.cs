using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileUploadControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBookingApp.Data;
using MovieBookingApp.Models;
using MovieBookingApp.Models.viewModels;

namespace MovieBookingApp.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private UploadInterface _upload;
 
        public AdminController(ApplicationDbContext context, UploadInterface upload)
        {
            _context = context;
            _upload = upload;
        }

        [HttpGet]
        public IActionResult index()
        {

            return View();

        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]
        public IActionResult Create(List<IFormFile> files,MovieDetailViewmodel vmodel, MovieDetails movie)
        {


            movie.Movie_Name = vmodel.Name;
            movie.Movie_Description = vmodel.Description;
            movie.DateAndTime = vmodel.DateofMovie;
            foreach (var item in files)
            {
                movie.MoviePicture = "~/uploads/" + item.FileName.Trim();
            }

            _upload.uploadfilemultiple(files);
            _context.MovieDetails.Add(movie);
            _context.SaveChanges();
            TempData["Success"] = "Save your Movie";
            return RedirectToAction(" Create ","Admin");

        }

        [HttpGet]

        public IActionResult checkBookSeat() {

            var getBookingTable = _context.BookingTable.ToList().OrderByDescending(a => a.Datetopresent);

            return View(getBookingTable);
        
        }

        [HttpGet]
        public IActionResult GetUserDetails() {

            var getUserTable = _context.Users.ToList();

            return View(getUserTable);
        
        
        }


    }
}