using DatabaseMiddlware.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMiddlware
{
    internal class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=CourseWorkBD;Username=postgres;Password=postgres");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Cooperator> Cooperators { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItems> MenuItems { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkBook> WorkBooks { get; set; }
        
    }
}
