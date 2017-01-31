using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace EventManager.Areas.Admin.Models
{
    public class Invitation
    {
        public Invitation()
        {
            Active = true;
        }
        public int ID { get; set; }
        [Required, StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(12, MinimumLength = 12, ErrorMessage = "Code must be 12 characters long")] 
        [RegularExpression(@"^[A-Z0-9]*$", ErrorMessage = "Please use only uppercase letters and numbers")]
        [Display(Name = "Invitation Code")]
        public string InvitationCode { get; set; }
        public bool Active { get; set; }
    }
}