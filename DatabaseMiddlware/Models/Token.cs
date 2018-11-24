using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseMiddlware.Models
{
    class Token
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid TokenId { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeForDelete { get;set }
    }
}
