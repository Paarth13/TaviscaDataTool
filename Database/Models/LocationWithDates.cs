﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database.Models
{
    public class LocationWithDates
    {
       public string Place { get; set; }
        public List<HotelsAtLocation> HotelsAtParticularLocation = new List<HotelsAtLocation>();
        public int totalBookings = 0;

    }
    public class HotelsAtLocation
    {
      public  string HotelName { get; set; }
        public int Bookings { get; set; }
    }
}