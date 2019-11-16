using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieBookingApp.Models.viewModels
{
    public class BookNowViewModel
    {
        public string Movie_Name { get; set; }
        public DateTime Movie_Date { get; set; }
        public string SeatNo { get; set; }
        public int Amount { get; set; }
        public int MovieId { get; set; }
    }
}
