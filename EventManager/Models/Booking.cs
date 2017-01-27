using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EventManager.Areas.Admin.Models;

namespace EventManager.Models
{
    public class Booking
    {
        [ForeignKey("Event")]
        public int BookingId { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        [Display(Name = "First Guest")]
        public string Guest1 { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        [Display(Name = "Second Guest")]
        public string Guest2 { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        [Display(Name = "Third Guest")]
        public string Guest3 { get; set; }
        public virtual Event Event { get; set; }
    }

    public class BookingDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
    }
}