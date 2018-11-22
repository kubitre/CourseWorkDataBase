using DatabaseMiddlware.ObjectsAfterDb;
using System;
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

                if(cooperator != null)
                {
                    context.Dishes.Add(new Models.Dish { Name = name, Cooperator = cooperator});
                    context.SaveChanges();
                }
                else
                    throw new ArgumentException("Cooperator was not found!");
            }
        }

        public static Dish GetDish(Guid id)
        {
            using(var context = new Context())
            {
                var dish = context.Dishes.Where(x => x.Id == id).ToArray().FirstOrDefault();
                if (dish == null)
                    throw new ArgumentException("Dish does not exist!");

                var recipe = context.Recipes.Where(x => x.Dish.Id == id).ToList();
                if (recipe == null)
                    throw new ArgumentException("Recpe does not exist!");



                var productsInDish = from recipeProduct in context.Recipes
                                     join product in context.Products on recipeProduct.Product.Id equals product.Id
                                     select new { Name = product.Name, Amount = recipeProduct.Amount, Price = product.Price };

                Console.WriteLine($"Dish [{dish.Id};{dish.Name}]:");


                var outer = 0.0;


                foreach(var element in productsInDish.ToList())
                {
                    outer += element.Price * element.Amount;
                    Console.WriteLine($"Product [{element.Name}] : {element.Amount} x {element.Price} = {element.Price * element.Amount}");
                }

                Console.WriteLine($"Summary: {outer}");

                return new Dish();

            }
        }
        

        public static void RemoveDish(string name)
        {

        }

        public static void RemoveDish(Guid id)
        {

        }
    }
}
