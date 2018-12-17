using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseMiddlware.ModelForTransmition
{
    [Serializable]
    public class AuthBLock
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Enter { get; set; }
    }
}
