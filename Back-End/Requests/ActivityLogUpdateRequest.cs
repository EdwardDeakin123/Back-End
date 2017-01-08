using System;

namespace Back_End.Requests
{
    public class ActivityLogUpdateRequest
    {
        public int ActivityLogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}