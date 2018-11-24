using System;
using System.Collections.Generic;
using System.Text;
using DatabaseMiddlware.ObjectsAfterDb;
using System.Linq;

namespace DatabaseMiddlware.Workers
{
    class CooperatorHandler
    {
        public static void AddCooperator(Guid creator, Cooperator cooperator)
        {
            using (var context = new Context())
            {
                var positionForCooperator = context.Positions.SingleOrDefault(x => x.Name.Equals(cooperator.Position));
                var categoryForCoooperator = context.Categories.SingleOrDefault(x => x.Name.Equals(cooperator.Category));

                if (positionForCooperator == null)
                    throw new ArgumentException($"position by name [{cooperator.Position}] was not exist in database!");

                var streetForCooperator = context.Streets.SingleOrDefault(x => x.Name.Equals(cooperator.Street));

                if (streetForCooperator == null)
                    throw new ArgumentException($"street by name [{cooperator.Street}] was not exist in database !");

                context.Cooperators.Add(new Models.Cooperator
                {
                    FirstName = cooperator.FirstName,
                    MiddleName = cooperator.MiddleName,
                    LastName = cooperator.LastName,
                    BirthDay = cooperator.BirthDay,
                    Salary = cooperator.Salary,
                    Building = cooperator.Building,
                    Flat = cooperator.Flat,
                    Position = positionForCooperator,
                    Category = categoryForCoooperator,
                    Street = streetForCooperator
                });

                context.SaveChanges();
            }
        }

        public static Cooperator GetCooperatorByName(string username)
        {
            using(var context = new Context())
            {
                var userFromDb = context.Users.SingleOrDefault(x => x.Name.Equals(username));

                if (userFromDb == null)
                    throw new ArgumentException($"user by username [{username}] was not exist in database!");

                var cooperatorFromDb = context.Cooperators.SingleOrDefault(x => x.Id.Equals(userFromDb.Cooperator.Id));

                if (cooperatorFromDb == null)
                    throw new ArgumentException($"cooperator by id [{userFromDb.Cooperator.Id}] was not exist in database!");

                return new Cooperator()
                {
                    Id = cooperatorFromDb.Id,
                    FirstName = cooperatorFromDb.FirstName,
                    MiddleName = cooperatorFromDb.MiddleName,
                    LastName = cooperatorFromDb.LastName,
                    Salary = cooperatorFromDb.Salary,
                    BirthDay = cooperatorFromDb.BirthDay,
                    Building = cooperatorFromDb.Building,
                    Flat = cooperatorFromDb.Flat,
                    Category = context.Categories.SingleOrDefault(x => x.Id.Equals(cooperatorFromDb.Category.Id)).Name,
                    Position = context.Positions.SingleOrDefault(x => x.Id.Equals(cooperatorFromDb.Position.Id)).Name,
                    Street = context.Streets.SingleOrDefault(x => x.Id.Equals(cooperatorFromDb.Street.Id)).Name
                };
            }
        }

        public static Cooperator GetCooperatorById(Guid idCooperator)
        {
            using(var context = new Context())
            {
                var cooperatorFromDb = context.Cooperators.SingleOrDefault(x => x.Id.Equals(idCooperator));

                if (cooperatorFromDb == null)
                    throw new ArgumentException($"cooperator by id [{idCooperator}] was not exist in database!");

                return new Cooperator()
                {
                    Id = cooperatorFromDb.Id,
                    FirstName = cooperatorFromDb.FirstName,
                    MiddleName = cooperatorFromDb.MiddleName,
                    LastName = cooperatorFromDb.LastName,
                    Salary = cooperatorFromDb.Salary,
                    BirthDay = cooperatorFromDb.BirthDay,
                    Building = cooperatorFromDb.Building,
                    Flat = cooperatorFromDb.Flat,
                    Category = context.Categories.SingleOrDefault(x => x.Id.Equals(cooperatorFromDb.Category.Id)).Name,
                    Position = context.Positions.SingleOrDefault(x => x.Id.Equals(cooperatorFromDb.Position.Id)).Name,
                    Street = context.Streets.SingleOrDefault(x => x.Id.Equals(cooperatorFromDb.Street.Id)).Name
                };
            }
        }

        public static void UpdateCooperator(Guid idCooperator, Cooperator cooperator)
        {
            using (var context = new Context())
            {
                var cooperatorFromDb = context.Cooperators.SingleOrDefault(x => x.Id.Equals(idCooperator));
                if (cooperatorFromDb == null)
                    throw new ArgumentException($"cooperator by id [{idCooperator}] was not exist in database!");

                if (cooperator.FirstName != null)
                    cooperatorFromDb.FirstName = cooperator.FirstName;

                if (cooperator.MiddleName != null)
                    cooperatorFromDb.MiddleName = cooperator.MiddleName;

                if (cooperator.LastName != null)
                    cooperatorFromDb.LastName = cooperator.LastName;

                if(cooperator.Position != null)
                {
                    var positionFromDb = context.Positions.SingleOrDefault(x => x.Name.Equals(cooperator.Position));

                    if (positionFromDb == null)
                        throw new ArgumentException($"position by name [{cooperator.Position}] was not exist in database!");

                    cooperatorFromDb.Position = positionFromDb;
                }

                if (cooperator.Salary != 0.0 & cooperator.Salary > 0.0)
                    cooperatorFromDb.Salary = cooperator.Salary;

                if(cooperator.Street != null)
                {
                    var streetFromDb = context.Streets.SingleOrDefault(x => x.Name.Equals(cooperator.Street));

                    if (streetFromDb == null)
                        throw new ArgumentException($"street by name [{cooperator.Street}] was not exist in database!");

                    cooperatorFromDb.Street = streetFromDb;
                }

                if(cooperator.Category != null)
                {
                    var categoryFromDb = context.Categories.SingleOrDefault(x => x.Name.Equals(cooperator.Category));

                    if (categoryFromDb == null)
                        throw new ArgumentException($"category by name [{cooperator.Category}] was not exist in database!");

                    cooperatorFromDb.Category = categoryFromDb;
                }

                if (cooperator.BirthDay != null)
                    cooperatorFromDb.BirthDay = cooperator.BirthDay;

                if (cooperator.Building != 0)
                    cooperatorFromDb.Building = cooperator.Building;

                if (cooperator.Flat != 0)
                    cooperatorFromDb.Flat = cooperator.Flat;

                context.SaveChanges();
            }
        }

        public static void DeleteCooperator(Guid idCooperator)
        {
            using (var context = new Context())
            {
                var cooperatorFromDb = context.Cooperators.SingleOrDefault(x => x.Id.Equals(idCooperator));

                if (cooperatorFromDb == null)
                    throw new ArgumentException($"cooperator by id [{idCooperator}] was not exist in database!");

                context.Cooperators.Remove(cooperatorFromDb);

                context.SaveChanges();
            }
        }
    }
}
