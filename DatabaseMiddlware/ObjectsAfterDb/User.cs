using DatabaseMiddlware.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseMiddlware.ObjectsAfterDb
{
    class User
    {
        public Token token { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
