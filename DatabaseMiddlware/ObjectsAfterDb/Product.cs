﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseMiddlware.ObjectsAfterDb
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
