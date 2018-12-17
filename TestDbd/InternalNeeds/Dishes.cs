using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.InternalNeeds
{
    public class Dishes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        List<DishBlocks> dishBlokcs { get; set; }
    }
}
