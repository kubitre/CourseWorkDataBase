using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseMiddlware.Workers
{
    class MenuHandler
    {
        public static void AddMenu(Guid idCooperator, string Name)
        {
            using (var context = new Context())
            {
                var userCooperator = context.Cooperators.Where(x => x.Id.Equals(idCooperator)).FirstOrDefault();

                if (userCooperator == null)
                    throw new ArgumentException($"cooperator by id [{idCooperator}] was not exist in database!");

                context.Menus.Add(new Models.Menu { Name = Name, Cooperator = userCooperator, Date = DateTime.Now });
                context.SaveChanges();
            }
        }

        public static void AddDishForMenu(Guid idMenu, Guid idDish)
        {
            using (var context = new Context())
            {
                var menuFromDb = context.Menus.Where(x => x.Id.Equals(idMenu)).FirstOrDefault();

                if (menuFromDb == null)
                    throw new ArgumentException($"menu by id [{idMenu}] was not exist in database!");

                var dishFromDb = context.Dishes.Where(x => x.Id.Equals(idDish)).FirstOrDefault();

                if (dishFromDb == null)
                    throw new ArgumentException($"dish by id [{idDish}] was not exist in database!");

                context.MenuItems.Add(new Models.MenuItems { Menu = menuFromDb, Dish = dishFromDb });

                context.SaveChanges();
            }
        }

        public static void DeleteMenu(Guid idMenu)
        {
            using(var context = new Context())
            {
                var menuFromDb = context.Menus.Where(x => x.Id.Equals(idMenu)).FirstOrDefault();

                if (menuFromDb == null)
                    throw new ArgumentException($"menu by id [{idMenu}] was not exist in database!");

                context.Menus.Remove(menuFromDb);
                context.SaveChanges();
            }
        }

        public static void UpdateMenu(Guid idMenu, DateTime date, Guid idCooperator)
        {
            using(var context = new Context())
            {
                var menuFromDb = context.Menus.Where(x => x.Id.Equals(idMenu)).FirstOrDefault();

                if (menuFromDb == null)
                    throw new ArgumentException($"menu by id [{idMenu}] was not exist in database!");

                var cooperatorFromDb = context.Cooperators.Where(x => x.Id.Equals(idCooperator)).FirstOrDefault();

                if (cooperatorFromDb == null)
                    throw new ArgumentException($"cooperator by id [{idCooperator}] was not exist in database!");

                menuFromDb.Date = date;
                menuFromDb.Cooperator = cooperatorFromDb;

                context.SaveChanges();
            }
        }
    }
}
