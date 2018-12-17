﻿using DatabaseMiddlware.ModelForTransmition;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.ModelForTransmition
{
    [Serializable]
    public class Auth
    {
        public string Action { get; set; }
        public AuthBLock Payload { get; set; }
    }
}
