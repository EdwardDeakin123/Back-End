using System.Data.Entity;

namespace Back_End.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
    }

    public class ActivityDbContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
    }
}