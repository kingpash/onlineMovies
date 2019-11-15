using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingApp.Models
{
    public class ApplicationUser
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateAndTime { get; set; }
        public string MoviePicture { get; set; }


    }
}
