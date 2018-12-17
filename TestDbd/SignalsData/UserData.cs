using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.SignalsData
{
    [Serializable]
    public class UserData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Cooperator { get; set; }

    }
}
