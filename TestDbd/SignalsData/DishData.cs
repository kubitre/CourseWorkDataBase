using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.SignalsData
{
    [Serializable]
    public class DishData
    {
        public string DishName { get; set; }
        public List<RecipeData> recipe { get; set; }
        public string Cooperator { get; set; }
        public DateTime Date { get; set; }
    }
}
