using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.SignalsData
{
    [Serializable]
    public class AuthData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Enter { get; set; }
    }
}
