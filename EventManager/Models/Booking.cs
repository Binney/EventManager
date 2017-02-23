using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventManager.Areas.Admin.Models;

namespace EventManager.Models
{
    public class Booking
    {
        [Key]
        [ForeignKey("Event")]
        [Display(Name = "Event")]
        public int EventId { get; set; }

        [Required, RegularExpression(@"^([\w\.\-]+)@softwire.com$", ErrorMessage = "Please use a softwire email address")]
        [Display(Name = "First Guest")]
        public string Guest1 { get; set; }

        [Required, RegularExpression(@"^([\w\.\-]+)@softwire.com$", ErrorMessage = "Please use a softwire email address")]
        [Display(Name = "Second Guest")]
        public string Guest2 { get; set; }

        [Required, RegularExpression(@"^([\w\.\-]+)@softwire.com$", ErrorMessage = "Please use a softwire email address")]
        [Display(Name = "Third Guest")]
        public string Guest3 { get; set; }

        public virtual Event Event { get; set; }

        public IEnumerable<string> AllGuests => new[] { Guest1, Guest2, Guest3 };
    }


}