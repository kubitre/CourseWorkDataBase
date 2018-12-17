using System;

namespace AdminPanel.Models
{
    [Serializable]
    public class AuthBLock
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Enter { get; set; }
    }
}
