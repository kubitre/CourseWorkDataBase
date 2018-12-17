using DatabaseMiddlware.ObjectsAfterDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DatabaseMiddlware.Workers.Products
{
    public class ProductHandler
    {
        public static Product GetProductByName(string name)
        {
            if (string.IsNullOrEmpty(name) | string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name can not be null for getting product by name!");

            using(var context = new Context())
            {
                var productFromDatabase = context.Products.FirstOrDefault(x => x.Name.Equals(name));
                if (productFromDatabase == null)
                    return null;

                return new Product()
                {
                    Id = productFromDatabase.Id,
                    Name = productFromDatabase.Name,
                    Price = productFromDatabase.Price
                };
            }
        }

        public static List<Product> GetProducts(int offset, int amount)
        {
            using(var context = new Context())
            {
                var products = context.Products.Skip(offset).Take(amount).ToList();

                var listResponse = new List<Product>();

                foreach(var element in products)
                    listResponse.Add(new Product
                    {
                        Id = element.Id,
                        Name = element.Name,
                        Price = element.Price
                    });

                return listResponse;
            }
        }

        public static List<Product> GetProductsByDish(string dishName)
        {
            if (dishName == null)
                throw new ArgumentNullException("dishName for getting products of this dish can not be null!");

            using(var context = new Context())
            {
                var dish = context.Dishes.FirstOrDefault(x => x.Name.Equals(dishName));

                if (dish == null)
                    throw new ArgumentException($"dish by name {dishName} was not existt in database!");

                var productsId = context.Recipes.Where(x => x.Dish.Equals(dish)).ToList();

                var listForResponse = new List<Product>();

                foreach(var productIndex in productsId)
                {
                    var productFromDatabase = context.Products.FirstOrDefault(x => x.Equals(productIndex.Product));
                    var productToAddition = new Product()
                    {
                        Id = productFromDatabase.Id,
                        Name = productFromDatabase.Name,
                        Price = productFromDatabase.Price
                    };

                    listForResponse.Add(productToAddition);
                }

                return listForResponse;
            } 
        }

        public static bool AddProduct(Product newProduct)
        {
            if (newProduct == null)
                throw new ArgumentNullException("new product can not be null for adding to database!");
            using(var context = new Context())
            {
                var ProductForSave = new Models.Product()
                {
                    Name = newProduct.Name,
                    Price = newProduct.Price
                };
                try
                {
                    context.Products.Add(ProductForSave);
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
                
            }
        }

        public static bool UpdateProduct(Guid idProduct, string name, double price)
        {
            using(var context = new Context())
            {
                try
                {
                    var elementFromDb = context.Products.FirstOrDefault(x => x.Id.Equals(idProduct));

                    elementFromDb.Name = name;
                    if(price != 0)
                        elementFromDb.Price = price;

                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool RemoveProductById(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException("id can not be null for removing by id!");

            using(var context = new Context())
            {
                var product = context.Products.FirstOrDefault(x => x.Id.Equals(id));
                if (product == null)
                    return false;

                try
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool RemoveProductByName(string name)
        {
            if (string.IsNullOrEmpty(name) | string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("product name can not be null for removing by name!");

            using(var context = new Context())
            {
                var product = context.Products.FirstOrDefault(x => x.Name.Equals(name));

                if (product == null)
                    return false;

                try
                {
                    context.Remove(product);
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
