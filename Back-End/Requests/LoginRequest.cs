using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back_End.Requests
{
    public class LoginRequest
    {
        // Was unable to POST multiple simple types using the Web API.
        // The solution is to use a class that wraps the parameters.
        // https://weblog.west-wind.com/posts/2012/May/08/Passing-multiple-POST-parameters-to-Web-API-Controller-Methods
        public string Username { get; set; }
        public string Password { get; set; }
    }
}