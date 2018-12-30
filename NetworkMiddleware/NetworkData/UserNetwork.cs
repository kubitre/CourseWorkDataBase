using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkData
{
    public class UserNetwork
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Cooperator { get; set; }
        public string Password { get; set; }
        public DateTime LastEnter { get; set; }
        public string Role { get; set; }
    }
}
