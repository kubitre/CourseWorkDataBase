using System;

namespace DataBase.Models
{
    class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int CooperatorId { get; set; }
    }
}
