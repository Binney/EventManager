using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace EventManager.Models
{
    public class Event
    {
        public int ID { get; set; }
        [Required,StringLength(60)]
        public string Name { get; set; }
        [DataType(DataType.Date),DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time),DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan Time { get; set; }
    }

    public class EventDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
    }

}