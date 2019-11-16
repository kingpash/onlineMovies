using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace MovieBookingApp.Models.viewModels
{
    public class MovieDetailViewmodel 
    {   public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateofMovie { get; set; }
        public string MoviePictures { get; set; }

  
    }

}

