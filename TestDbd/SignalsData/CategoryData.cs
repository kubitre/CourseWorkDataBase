using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.SignalsData
{
    [Serializable]
    public class CategoryData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
