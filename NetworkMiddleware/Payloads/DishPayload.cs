using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    [Serializable]
    public class DishPayload
    {
        public int countDishes { get; set; }
        public int offsetAboutFirst { get; set; }
    }
}
