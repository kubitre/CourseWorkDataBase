using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Web;

namespace DatabaseMiddlware
{
    public class Class1
    {
        static void Main(string[] args)
        {

        }

        public void TestGetDish()
        {
            using(var context = new Context())
            {
                var dish = DatabaseMiddlware.Workers.Dishes.DishHandler.GetDish(context.Dishes.Where(x => x.Name.Equals("Борщ мясной")).FirstOrDefault().Id);
            }
            
        }

        public void FillTestDishes()
        {
            using (var context = new Context())
            {
                context.Products.Add(new Models.Product { Name = "Мясо", Price = 1231 });
                context.Products.Add(new Models.Product { Name = "Свекла", Price = 345345 });
                context.Products.Add(new Models.Product { Name = "Лук репчатый", Price = 1231 });
                context.Products.Add(new Models.Product { Name = "Белокачанная капуста", Price = 1234 });
                context.Products.Add(new Models.Product { Name = "Коренья", Price = 1235 });
                context.Products.Add(new Models.Product { Name = "Томатное пюре", Price = 1234 });
                context.Products.Add(new Models.Product { Name = "Уксус", Price = 1235 });
                context.Products.Add(new Models.Product { Name = "Сахар", Price = 2134 });

                var cooperator = context.Cooperators.Where(x => x.FirstName.Equals("Кислов")).FirstOrDefault();

                context.Dishes.Add(new Models.Dish { Name = "Тест1", Cooperator = cooperator });
                context.Dishes.Add(new Models.Dish { Name = "Тест2", Cooperator = cooperator });
                context.Dishes.Add(new Models.Dish { Name = "Тест3", Cooperator = cooperator });
                //context.Dishes.Add(new Models.Dish { Name = "Тест4", Cooperator = cooperator });
                //context.Dishes.Add(new Models.Dish { Name = "Тест5", Cooperator = cooperator });

                context.SaveChanges();


                var dish = context.Dishes.Where(x => x.Name.Equals("Тест1")).FirstOrDefault();

                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish,
                    Product = context.Products
                                        .Where(x => x.Name.Equals("Мясо")).FirstOrDefault(),
                    Amount = 0.5
                }
                );

                context.Recipes.Add(new Models.Recipe()
                {
                    Dish = dish,
                    Product = context.Products.Where(x => x.Name.Equals("Свекла")).FirstOrDefault(),
                    Amount = 0.8
                });

                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish,
                    Product = context.Products.Where(x => x.Name.Equals("Лук репчатый")).FirstOrDefault(),
                    Amount = 1.2
                });

                var dish2 = context.Dishes.Where(x => x.Name.Equals("Тест2")).FirstOrDefault();

                context.Recipes.Add(new Models.Recipe()
                {
                    Dish = dish2,
                    Product = context.Products.Where(x => x.Name.Equals("Коренья")).FirstOrDefault(),
                    Amount = 9.2
                });

                context.Recipes.Add(new Models.Recipe()
                {
                    Dish = dish2,
                    Product = context.Products.Where(x => x.Name.Equals("Сахар")).FirstOrDefault(),
                    Amount = 2.3
                });

                var dish3 = context.Dishes.Where(x => x.Name.Equals("Тест3")).FirstOrDefault();

                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish3,
                    Product = context.Products.Where(x => x.Name.Equals("Уксус")).FirstOrDefault(),
                    Amount = 4.21
                });
                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish3,
                    Product = context.Products
                                        .Where(x => x.Name.Equals("Мясо")).FirstOrDefault(),
                    Amount = 42.3
                });

                context.SaveChanges();


               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Свекла")).FirstOrDefault(),
               //     Amount = 0.3
               // }
               // );

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Лук репчатый")).FirstOrDefault(),
               //     Amount = 1
               // }
               // );

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Белокачанная капуста")).FirstOrDefault(),
               //     Amount = 0.2
               // }
               // );

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Коренья")).FirstOrDefault(),
               //     Amount = 2
               // }
               // );

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Томатное пюре")).FirstOrDefault(),
               //     Amount = 0.12
               // }
               // );

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Уксус")).FirstOrDefault(),
               //     Amount = 0.06
               // }
               // );

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Сахар")).FirstOrDefault(),
               //     Amount = 0.06
               // }
               // );

               // var dish2 = context.Dishes.Where(x => x.Name.Equals("Тест2")).FirstOrDefault();

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish2,
               //     Product = context.Products
               //                      .Where(x => x.Name.Equals("Мясо")).FirstOrDefault(),
               //     Amount = 0.5
               // }
               // );

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish2,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Свекла")).FirstOrDefault(),
               //     Amount = 0.3
               // }
               // );

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish2,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Лук репчатый")).FirstOrDefault(),
               //     Amount = 1
               // }
               // );

               // var dish3 =  context.Dishes.Where(x => x.Name.Equals("Тест3")).FirstOrDefault();
               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish3,
               //     Product = context.Products
               //                     .Where(x => x.Name.Equals("Мясо")).FirstOrDefault(),
               //     Amount = 0.5
               // }
               //);

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish3,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Свекла")).FirstOrDefault(),
               //     Amount = 0.3
               // }
               // );
               // var dish4 =  context.Dishes.Where(x => x.Name.Equals("Тест4")).FirstOrDefault();

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish4,
               //     Product = context.Products
               //                     .Where(x => x.Name.Equals("Мясо")).FirstOrDefault(),
               //     Amount = 0.23
               // }
               //);

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish4,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Свекла")).FirstOrDefault(),
               //     Amount = 0.56
               // }
               // );
               // var dish5 =  context.Dishes.Where(x => x.Name.Equals("Тест5")).FirstOrDefault();

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish5,
               //     Product = context.Products
               //                     .Where(x => x.Name.Equals("Мясо")).FirstOrDefault(),
               //     Amount = 0.9
               // }
               //);

               // context.Recipes.Add(new Models.Recipe
               // {
               //     Dish = dish5,
               //     Product = context.Products
               //                         .Where(x => x.Name.Equals("Свекла")).FirstOrDefault(),
               //     Amount = 0.1
               // }
               // );

               // context.SaveChanges();
            }
        }

        public void RunFil()
        {
            FillStreets();
            FillCategories();
            FillPositions();
            FillCooperator();
            FillUser();
            FillProductAndDish();
        }

        public void FillMenu()
        {
            using (var context = new Context())
            {
                var cooperatorCalculator = context.Cooperators.FirstOrDefault(x => x.FirstName.Equals("Кислов"));
                context.Menus.Add(new Models.Menu
                {
                    Name = "Завтрак",
                    Cooperator = cooperatorCalculator,
                    Date = DateTime.Now
                });
                context.Menus.Add(new Models.Menu
                {
                    Name = "Обед",
                    Cooperator = cooperatorCalculator,
                    Date = DateTime.Now
                });

                context.Menus.Add(new Models.Menu
                {
                    Name = "Ужин",
                    Cooperator = cooperatorCalculator,
                    Date = DateTime.Now
                });
                context.SaveChanges();

                var menu = context.Menus.FirstOrDefault(x => x.Name.Equals("Завтрак"));
                var dish1 = context.Dishes.FirstOrDefault(x => x.Name.Equals("Тест1"));
                var dish2 = context.Dishes.FirstOrDefault(x => x.Name.Equals("Тест2"));

                var recipeForDish1 = context
                                            .Recipes
                                                .Where(x => x.Dish.Equals(dish1))
                                                .Include(x => x.Dish)
                                                .Include(x => x.Product);

                var outer1 = 0.0;

                foreach(var product in recipeForDish1)
                {
                    outer1 += product.Amount * product.Product.Price;
                }


                context.MenuItems.Add(new Models.MenuItems
                {
                    Menu = menu,
                    Dish = dish1,
                    Outer = outer1
                });

                var recipeForDish2 = context
                                            .Recipes
                                                .Where(x => x.Dish.Equals(dish2))
                                                .Include(x => x.Dish)
                                                .Include(x => x.Product);

                var outer2 = 0.0;

                foreach(var product in recipeForDish2)
                {
                    outer2 += product.Amount * product.Product.Price;
                }

                context.MenuItems.Add(new Models.MenuItems
                {
                    Menu = menu,
                    Dish = dish2,
                    Outer = outer2
                });
                context.SaveChanges();
            }
        }

        public static void CreateTestUser()
        {
            using (var context = new Context())
            {
                context.Users.Add(new Models.User
                {
                    Cooperator = context.Cooperators.Where(x => x.FirstName.Equals("Аносов")).FirstOrDefault(),
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Name = "test1234",
                    Role = (int)TypeRole.Role.Type.Programmer,
                    Password = DatabaseMiddlware.Workers.Users.UserHandler.GetHashSha("test1234")
                });

                context.SaveChanges();
            }
        }

        public void FillStreets()
        {
            using(var context = new Context())
            {
                context.Streets.Add(new Models.Street { Name = "Романтиков" });
                context.Streets.Add(new Models.Street { Name = "Стрижово" });
                context.Streets.Add(new Models.Street { Name = "Лыщинского" });
                context.Streets.Add(new Models.Street { Name = "Октябрьская" });

                context.SaveChanges();
            }
        }
        public void FillCategories()
        {
            using(var context = new Context())
            {
                context.Categories.Add(new Models.Category { Name = "главный" });
                context.Categories.Add(new Models.Category { Name = "су-шеф" });
                context.Categories.Add(new Models.Category { Name = "шеф" });

                context.SaveChanges();
            }
        }
        public void FillPositions()
        {
            using(var context = new Context())
            {
                context.Positions.Add(new Models.Position { Name = "программист" });
                context.Positions.Add(new Models.Position { Name = "повар" });
                context.Positions.Add(new Models.Position { Name = "бухгалтер" });
                context.Positions.Add(new Models.Position { Name = "менеджер" });

                context.SaveChanges();
            }

        }
        public void FillUser()
        {
            using(var context = new Context())
            {
                context.Users.Add(new Models.User
                {
                    Cooperator = context.Cooperators.Where(x => x.FirstName.Equals("Аносов")).FirstOrDefault(),
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Name = "kubitre1234",
                    Role = (int)TypeRole.Role.Type.Programmer,
                    Password = DatabaseMiddlware.Workers.Users.UserHandler.GetHashSha("Test1234")
                });

                context.Users.Add(new Models.User
                {
                    Cooperator = context.Cooperators.Where(x => x.FirstName.Equals("Кислов")).FirstOrDefault(),
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Name = "kislovs1234",
                    Role = (int)TypeRole.Role.Type.Calculator,
                    Password = DatabaseMiddlware.Workers.Users.UserHandler.GetHashSha("Test1234")
                });

                context.Users.Add(new Models.User
                {
                    Cooperator = context.Cooperators.Where(x => x.FirstName.Equals("Воливач")).FirstOrDefault(),
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Name = "kostya05983",
                    Role = (int)TypeRole.Role.Type.Programmer,
                    Password = DatabaseMiddlware.Workers.Users.UserHandler.GetHashSha("test1234")
                });

                context.Users.Add(new Models.User
                {
                    Cooperator = context.Cooperators.Where(x => x.FirstName.Equals("Хнюнин")).FirstOrDefault(),
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Name = "mihailxn1234",
                    Role = (int)TypeRole.Role.Type.Programmer,
                    Password = DatabaseMiddlware.Workers.Users.UserHandler.GetHashSha("Test1234")
                });

                context.SaveChanges();
            }
        }
        public void FillCooperator()
        {
            using (var context = new Context())
            {
                context.Cooperators.Add(new Models.Cooperator
                {
                    FirstName = "Аносов",
                    MiddleName = "Константин",
                    LastName = "Геннадьевич",
                    BirthDay = new DateTime(1998, 7, 17),
                    Flat = 47,
                    Building = 9,
                    Street = context.Streets.Where(x => x.Name.Equals("Романтиков")).FirstOrDefault(),
                    Category = context.Categories.Where(x => x.Name.Equals("главный")).FirstOrDefault(),
                    Position = context.Positions.Where(x => x.Name.Equals("программист")).FirstOrDefault(),
                    Salary = 100292.1
                });

                context.Cooperators.Add(new Models.Cooperator
                {
                    FirstName = "Воливач",
                    MiddleName = "Константин",
                    LastName = "Денисович",
                    BirthDay = new DateTime(1998, 5, 3),
                    Flat = 45,
                    Building = 13,
                    Street = context.Streets.Where(x => x.Name.Equals("Стрижово")).FirstOrDefault(),
                    Category = context.Categories.Where(x => x.Name.Equals("су-шеф")).FirstOrDefault(),
                    Position = context.Positions.Where(x => x.Name.Equals("повар")).FirstOrDefault(),
                    Salary = 5000
                });

                context.Cooperators.Add(new Models.Cooperator
                {
                    FirstName = "Кислов",
                    MiddleName = "Сергей",
                    LastName = "Александрович",
                    BirthDay = new DateTime(1998, 7, 17),
                    Flat = 66,
                    Building = 4,
                    Street = context.Streets.Where(x => x.Name.Equals("Лыщинского")).FirstOrDefault(),
                    Category = context.Categories.Where(x => x.Name.Equals("главный")).FirstOrDefault(),
                    Position = context.Positions.Where(x => x.Name.Equals("бухгалтер")).FirstOrDefault(),
                    Salary = 35234.3
                });

                context.Cooperators.Add(new Models.Cooperator
                {
                    FirstName = "Хнюнин",
                    MiddleName = "Михаил",
                    LastName = "Турунтьевич",
                    BirthDay = new DateTime(1998, 7, 17),
                    Flat = 34,
                    Building = 2,
                    Street = context.Streets.Where(x => x.Name.Equals("Октябрьская")).FirstOrDefault(),
                    Category = context.Categories.Where(x => x.Name.Equals("главный")).FirstOrDefault(),
                    Position = context.Positions.Where(x => x.Name.Equals("менеджер")).FirstOrDefault(),
                    Salary = 63743.2
                });

                context.SaveChanges();
            }
        }

        public void FillProductAndDish()
        {
            using (var context = new Context())
            {
                context.Products.Add(new Models.Product { Name = "Мясо", Price = 275.14 });
                context.Products.Add(new Models.Product { Name = "Свекла", Price = 55.1 });
                context.Products.Add(new Models.Product { Name = "Лук репчатый", Price = 60.0 });
                context.Products.Add(new Models.Product { Name = "Белокачанная капуста", Price = 21.2 });
                context.Products.Add(new Models.Product { Name = "Коренья", Price = 28 });
                context.Products.Add(new Models.Product { Name = "Томатное пюре", Price = 45.5 });
                context.Products.Add(new Models.Product { Name = "Уксус", Price = 30 });
                context.Products.Add(new Models.Product { Name = "Сахар", Price = 39.9 });

                context.SaveChanges();

                var cooperator = context.Cooperators.Where(x => x.FirstName.Equals("Аносов")).FirstOrDefault();

                context.Dishes.Add(new Models.Dish { Name = "Борщ мясной", Cooperator = cooperator });

                context.SaveChanges();

                var dish = context.Dishes.Where(x => x.Name.Equals("Борщ мясной")).FirstOrDefault();

                context.Recipes.Add(new Models.Recipe {
                    Dish = dish,
                    Product = context.Products
                                        .Where(x => x.Name.Equals("Мясо")).FirstOrDefault(),
                    Amount = 0.5
                    }
                );

                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish,
                    Product = context.Products
                                        .Where(x => x.Name.Equals("Свекла")).FirstOrDefault(),
                    Amount = 0.3
                }
                );

                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish,
                    Product = context.Products
                                        .Where(x => x.Name.Equals("Лук репчатый")).FirstOrDefault(),
                    Amount = 1
                }
                );

                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish,
                    Product = context.Products
                                        .Where(x => x.Name.Equals("Белокачанная капуста")).FirstOrDefault(),
                    Amount = 0.2
                }
                );

                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish,
                    Product = context.Products
                                        .Where(x => x.Name.Equals("Коренья")).FirstOrDefault(),
                    Amount = 2
                }
                );

                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish,
                    Product = context.Products
                                        .Where(x => x.Name.Equals("Томатное пюре")).FirstOrDefault(),
                    Amount = 0.12
                }
                );

                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish,
                    Product = context.Products
                                        .Where(x => x.Name.Equals("Уксус")).FirstOrDefault(),
                    Amount = 0.06
                }
                );

                context.Recipes.Add(new Models.Recipe
                {
                    Dish = dish,
                    Product = context.Products
                                        .Where(x => x.Name.Equals("Сахар")).FirstOrDefault(),
                    Amount = 0.06
                }
                );

                context.SaveChanges();
            }
        }

    }
}
