using DatabaseMiddlware.ObjectsAfterDb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseMiddlware.Workers.Dishes
{
    public static class DishHandler
    {
        public static void AddDish(string name, Guid cooperatorId)
        {
            using(var context = new Context())
            {
                var cooperator = context.Cooperators.Where(x => x.Id == cooperatorId).ToArray()[0];

                if (cooperator == null)
                    throw new ArgumentException($"Cooperator by id [{cooperatorId}] was not exist in database!");

                context.Dishes.Add(new Models.Dish { Name = name, Cooperator = cooperator });
                context.SaveChanges();
            }
        }

        public static Dish GetDish(Guid id)
        {
            using(var context = new Context())
            {
                var dish = context.Dishes.Where(x => x.Id == id).ToArray().FirstOrDefault();
                if (dish == null)
                    throw new ArgumentException($"dish by id [{id}] was not exist in database!");

                var recipe = context.Recipes.Where(x => x.Dish.Id == id).ToList();
                if (recipe == null)
                    throw new ArgumentException($"recipe for dish id [{id}] was not exist in database");

                var productsInDish = from recipeProduct in context.Recipes
                                     join product in context.Products on recipeProduct.Product.Id equals product.Id
                                     select new { Name = product.Name, Amount = recipeProduct.Amount, Price = product.Price };

                var outer = 0.0;

                var products = new List<string>();

                foreach(var element in productsInDish.ToList())
                {
                    outer += element.Price * element.Amount;
                    products.Add($"{element.Name} ({element.Amount} x {element.Price})");
                }

                return new Dish()
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Outer = outer,
                    Products = products
                };
            }
        }
        
        public static List<Dish> GetDishes(int offset, int count)
        {
            using(var context = new Context())
            {
                var dishes = context.Dishes.Skip(offset).Take(count).ToList();
                var result = new List<Dish>();
                
                foreach(var element in dishes)
                {
                    var recipe = context.Recipes.Where(x => x.Dish.Id == element.Id).ToList();
                    if (recipe == null)
                        throw new ArgumentException($"recipe for dish id [{element.Id}] was not exist in database");

                    var productsInDish = from recipeProduct in context.Recipes
                                         join product in context.Products on recipeProduct.Product.Id equals product.Id
                                         select new { Name = product.Name, Amount = recipeProduct.Amount, Price = product.Price };

                    var outer = 0.0;

                    var products = new List<string>();

                    foreach (var elementinProducts in productsInDish.ToList())
                    {
                        outer += elementinProducts.Price * elementinProducts.Amount;
                        products.Add($"{elementinProducts.Name} ({elementinProducts.Amount} x {elementinProducts.Price})");
                    }

                    result.Add(new Dish()
                    {
                        Id = element.Id,
                        Name = element.Name,
                        Outer = outer,
                        Products = products
                    });
                }

                return result;
            }
        }

        public static void RemoveDish(string name)
        {
            using (var context = new Context())
            {
                var dishFromBd = context.Dishes.SingleOrDefault(x => x.Name.Equals(name));

                if (dishFromBd == null)
                    throw new ArgumentException($"dish by name [{name}] was not exist in database!");

                context.Dishes.Remove(dishFromBd);
                context.SaveChanges();
            }

        }

        public static void RemoveDish(Guid id)
        {
            using(var context = new Context())
            {
                var dishFromDb = context.Dishes.SingleOrDefault(x => x.Id.Equals(id));

                if (dishFromDb == null)
                    throw new ArgumentException($"dish by id [{id}] was not exist in database!");

                context.Dishes.Remove(dishFromDb);
                context.SaveChanges();
            }
        }
    }
}
