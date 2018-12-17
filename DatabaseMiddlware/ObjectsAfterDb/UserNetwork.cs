using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseMiddlware.ObjectsAfterDb
{
    public class UserNetwork
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public DateTime LastEnter { get; set; }
        public string Cooperator { get; set; }
        public string Role { get; set; }
    }
}
