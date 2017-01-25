using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventManager.Models
{
    public class Invitation
    {
        public int ID { get; set; }
        [Required, StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string InvitationCode { get; set; }
    }

    public class InvitationDbContext : DbContext
    {
        public DbSet<Invitation> Invitations { get; set; }
    }
}