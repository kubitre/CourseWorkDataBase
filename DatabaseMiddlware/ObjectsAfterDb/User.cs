﻿using DatabaseMiddlware.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseMiddlware.ObjectsAfterDb
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime LastEnter { get; set; }
    }
}
