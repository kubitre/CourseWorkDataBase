using System;

namespace AdminPanel.NetworkMiddleware.NetworkData
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string AuthCode { get; set; }
        public DateTime LastEnter { get; set; }
    }
}
