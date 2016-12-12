using System;
using System.Data.Entity;

namespace Back_End.Models
{
    // This class represents the Session table in the database.
    // A session is created when a device has successfuly authenticated itself with the backend.
    // Sessions will become stale and will need to be recreated occasionally.
    public class Session
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime LastCheckin { get; set; }
    }

    public class SessionDbContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
    }
}