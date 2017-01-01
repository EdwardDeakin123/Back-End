using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Back_End.Models
{
    // This class represents the User table in the database.
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        // Prevent the password from being serialized and sent over the API as that could be a security risk.
        // http://stackoverflow.com/questions/11851207/prevent-property-from-being-serialized-in-web-api
        [JsonIgnore]
        public string Password { get; set; }
    }

    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}