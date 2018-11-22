namespace DatabaseMiddlware.Models
{
    using DatabaseMiddlware.TypeRole;
    using System;

    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Cooperator Cooperator { get; set; }
        public int Role { get; set; }
        public DateTime LastEnter { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
