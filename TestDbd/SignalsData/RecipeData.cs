using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.SignalsData
{
    [Serializable]
    public class RecipeData
    {
        public int Amount { get; set; }
        public ProductData Product { get; set; }
    }
}
