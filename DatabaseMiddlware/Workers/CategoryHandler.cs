using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseMiddlware.Workers
{
    public class CategoryHandler
    {
        public static List<ObjectsAfterDb.Category> GetCategories(int offset, int amount)
        {
            using(var context = new Context())
            {
                var listWithResponse = new List<ObjectsAfterDb.Category>();

                var categories = context.Categories.Skip(offset).Take(amount).ToList();

                foreach(var element in categories)
                {
                    listWithResponse.Add(new ObjectsAfterDb.Category
                    {
                        Id = element.Id,
                        Name = element.Name
                    });
                }

                return listWithResponse;
            }
        }

        public static bool CreateNewCategory(string name)
        {
            using(var context = new Context())
            {
                try
                {
                    context.Categories.Add(new Models.Category
                    {
                        Name = name
                    });
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool ChangeCategory(Guid older, string name)
        {
            using (var context = new Context())
            {
                try
                {
                    var changeElement = context.Categories.FirstOrDefault(x => x.Id.Equals(older));
                    changeElement.Name = name;
                    context.SaveChanges();
                    return true;

                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool DeleteCategory(Guid category)
        {
            using(var context = new Context())
            {
                try
                {
                    var deletetElement = context.Categories.FirstOrDefault(x => x.Id.Equals(category));
                    context.Categories.Remove(deletetElement);
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
