using DataBaseWorker.TypeRole;
using System;

namespace DataBase.Models
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int CooperatorId { get; set; }
        public Role Role { get; set; }
        public DateTime LastEnter { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
