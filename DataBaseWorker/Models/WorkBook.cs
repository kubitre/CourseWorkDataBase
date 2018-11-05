using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAgregator.Models
{
    class WorkBook
    {
        public int Id { get; set; }
        public int CooperatorId { get; set; }
        public int PositionId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
    }
}
