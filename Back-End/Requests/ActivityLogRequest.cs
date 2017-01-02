using System;

namespace Back_End.Requests
{
    public class ActivityLogRequest
    {
        // Was unable to POST multiple simple types using the Web API.
        // The solution is to use a class that wraps the parameters.
        // https://weblog.west-wind.com/posts/2012/May/08/Passing-multiple-POST-parameters-to-Web-API-Controller-Methods
        public int ActivityId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}