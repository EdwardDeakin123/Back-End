using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Back_End.Models
{
    // This class represents the ActivityLog table in the database.
    public class ActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityLogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // These represent foreign keys. An ActivityLog object will return the associated User and Activity objects.
        public virtual User User { get; set; }
        public virtual Activity Activity { get; set; }
    }

    public class ActivityLogDbContext : DbContext
    {
        public DbSet<ActivityLog> ActivityLogs { get; set; }
    }
}