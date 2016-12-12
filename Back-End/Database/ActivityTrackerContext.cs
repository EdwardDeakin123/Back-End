using System.Data.Entity;
using Back_End.Models;

namespace Back_End.Database
{
    // Implementing the DbContext which defines which of the entities are included in our data model.
    // Used the following guide to assist in implementation:
    // https://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application
    public class ActivityTrackerContext : DbContext
    {
        public ActivityTrackerContext() : base("ActivityTrackerContext")
        {
        }

        // Map the models to tables in the database.
        // The models are given the singular name and tables the plural.
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}