using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back_End.Requests
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}