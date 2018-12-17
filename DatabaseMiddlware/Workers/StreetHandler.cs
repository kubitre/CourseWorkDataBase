using DatabaseMiddlware.ObjectsAfterDb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseMiddlware.Workers
{
    public class StreetHandler
    {
        public static bool AddStreet(string name)
        {
            if (string.IsNullOrEmpty(name) | string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name for street can not be null!");
            using(var context = new Context())
            {
                try
                {
                    context.Streets.Add(new Models.Street()
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

        public static Street GetStreetByName(string name)
        {
            if (string.IsNullOrEmpty(name) | string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name for getting street from db can not be null!");

            using(var context = new Context())
            {
                var streetFromDb = context.Streets.FirstOrDefault(x => x.Name.Equals(name));

                if (streetFromDb == null)
                    return null;

                return new Street()
                {
                    Id = streetFromDb.Id,
                    Name = streetFromDb.Name
                };
            }
        }
        public static Street GetStreetById(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException("id for geeting street from db can not be null!");
            using(var context = new Context())
            {
                var streetFromDb = context.Streets.FirstOrDefault(x => x.Id.Equals(id));

                if (streetFromDb == null)
                    return null;

                return new Street()
                {
                    Id = streetFromDb.Id,
                    Name = streetFromDb.Name
                };
            }
        }

        public static List<Street> GetStreets(int offset, int amount)
        {
            using(var context = new Context())
            {
                var streets = context.Streets.Take(amount).Skip(offset).ToList();

                var streetsForResponse = new List<Street>();
                foreach(var element in streets)
                    streetsForResponse.Add(new Street() { Id = element.Id, Name = element.Name });
                

                return streetsForResponse;
            }
        }

        public static bool UpdateStreetName(Guid idStreet, string name)
        {
            using(var context = new Context())
            {
                try
                {
                    var streetFromDb = context.Streets.FirstOrDefault(x => x.Id.Equals(idStreet));

                    streetFromDb.Name = name;
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool RemoveStreetByName(string name)
        {
            if (string.IsNullOrEmpty(name) | string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name for deleting from db can not be null!");

            using(var context = new Context())
            {
                var streetFromDb = context.Streets.FirstOrDefault(x => x.Name.Equals(name));

                if (streetFromDb == null)
                    return false;

                try
                {
                    context.Streets.Remove(streetFromDb);
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }
        public static bool RemoveStreetById(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException("id for removing from db street can not be null!");

            using(var context = new Context())
            {
                var streetFromDb = context.Streets.FirstOrDefault(x => x.Id.Equals(id));

                if (streetFromDb == null)
                    return false;

                try
                {
                    context.Streets.Remove(streetFromDb);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
