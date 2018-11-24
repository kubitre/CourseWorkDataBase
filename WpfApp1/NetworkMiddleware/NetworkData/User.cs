using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.NetworkMiddleware.NetworkData
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string AuthCode { get; set; }
        public DateTime LastEnter { get; set; }
    }
}
