using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketTracker.Models
{
    /// <summary>
    /// Used to edit user role
    /// </summary>
    public class UserWithRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}