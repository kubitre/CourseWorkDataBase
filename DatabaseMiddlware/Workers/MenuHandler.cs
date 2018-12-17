using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseMiddlware.ObjectsAfterDb;

namespace DatabaseMiddlware.Workers.Menus
{
    public class MenuHandler
    {
        public static bool AddMenu(ObjectsAfterDb.Menu menu)
        {
            using(var context = new Context())
            {
                try
                {
                    var user = context
                                            .Users
                                            .Include(x => x.Cooperator)
                                            .FirstOrDefault(x => x.Name.Equals(menu.Coocker))
                                            ;

                    context.Menus.Add(new Models.Menu
                    {
                        Name = menu.Name,
                        Cooperator = user.Cooperator,
                        Date = menu.Date,
                    });
                    context.SaveChanges();

                    var menuFromDb = context.Menus.FirstOrDefault(x => x.Name.Equals(menu.Name));

                    foreach(var dish in menu.Dishes)
                    {
                        var contextProducts = context
                                                    .Recipes
                                                    .Where(x => x.Dish.Name.Equals(dish))
                                                    .Include(y => y.Dish)
                                                    .Include(y => y.Product)
                                                    .ToList();

                        var contextDish = context.Dishes.FirstOrDefault(x => x.Name.Equals(dish));

                        var outer = 0.0;

                        foreach (var product in contextProducts)
                        {
                            outer += product.Amount * product.Product.Price;
                        }

                        context.MenuItems.Add(new Models.MenuItems
                        {
                            Dish = contextDish,
                            Menu = menuFromDb,
                            Outer = outer
                        });


                    }

                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }

            }
        }
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

        public static bool DeleteMenu(Guid idMenu)
        {
            using(var context = new Context())
            {
                var menuFromDb = context.Menus.Where(x => x.Id.Equals(idMenu)).FirstOrDefault();

                if (menuFromDb == null)
                    throw new ArgumentException($"menu by id [{idMenu}] was not exist in database!");
                try
                {
                    context.Menus.Remove(menuFromDb);
                    context.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static List<Menu> GetMenus(int offset, int amount)
        {
            using(var context = new Context())
            {
                var listWithMenu = new List<Menu>();

                var menus = context.Menus.Skip(offset).Take(amount).ToList();

                foreach(var menu in menus)
                {
                    var menuItems = context
                                            .MenuItems
                                            .Where(x => x.Menu.Equals(menu))
                                            .Include(x => x.Dish)
                                            .Include(x => x.Menu)
                                            .Include(x => x.Menu.Cooperator);

                    var outGlobal = 0.0;
                    var listForOneMenu = new List<string>();

                    foreach(var element in menuItems)
                    {
                        var recipeForDish = context.Recipes
                                                        .Where(x => x.Dish.Equals(element.Dish))
                                                        .Include(x => x.Dish)
                                                        .Include(x => x.Product);

                        var outerForDish = 0.0;

                        foreach(var product in recipeForDish)
                        {
                            outerForDish = product.Product.Price * product.Amount;
                        }
                        outGlobal += outerForDish;

                        listForOneMenu.Add($"{element.Dish.Name}({outerForDish})");
                    }
                    listWithMenu.Add(new Menu() { Id = menu.Id, Name = menu.Name, Coocker = menu.Cooperator.FirstName, Date = menu.Date, Dishes = listForOneMenu, Outer = outGlobal });
                                            
                }

                return listWithMenu;
            }
        }

        public static bool UpdateMenu(Guid idMenu, DateTime date, Guid idCooperator)
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

                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
