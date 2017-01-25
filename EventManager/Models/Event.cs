using System;
using System.Data.Entity;


namespace EventManager.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }

    public class EventDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
    }

}