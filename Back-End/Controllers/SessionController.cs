using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Back_End.Controllers
{
    // This controller is intended to query if a specific user has a current session.
    public class SessionController : ApiController
    {
        // GET: /Session/Get
        public Session Get(int userId, int deviceId)
        {
            // Update this to actually check if there is a session in the database for this
            // user on this device.
            Session testSession = new Session();

            testSession.DeviceId = 0;
            testSession.UserId = 0;

            return testSession;
        }
    }
}
