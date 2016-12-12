using System;
using System.Data.Entity;

namespace Back_End.Models
{
    // This class represents the ActivityLog table in the database.
    public class ActivityLog
    {
        public int ActivityLogId { get; set; }
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class ActivityLogDbContext : DbContext
    {
        public DbSet<ActivityLog> ActivityLogs { get; set; }
    }
}