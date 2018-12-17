using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseMiddlware.Workers
{
    public class PositionHandler
    {
        public static List<ObjectsAfterDb.Position> GetPositions(int offset, int amount)
        {
            using(var context = new Context())
            {
                var listWithResponse = new List<ObjectsAfterDb.Position>();

                var positions = context.Positions.Skip(offset).Take(amount).ToList();

                foreach(var element in positions)
                    listWithResponse.Add(new ObjectsAfterDb.Position
                    {
                        Id = element.Id,
                        Name = element.Name
                    });

                return listWithResponse;
            }
        }

        public static bool CreateNewPosition(string name)
        {
            using (var context = new Context())
            {
                try
                {
                    context.Positions.Add(new Models.Position
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

        public static bool UpdatePosition(Guid idPosition, string name)
        {
            using(var context = new Context())
            {
                try
                {
                    var elementFromDb = context.Positions.FirstOrDefault(x => x.Id.Equals(idPosition));

                    elementFromDb.Name = name;

                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool DeletePosition(Guid idPosition)
        {
            using(var context = new Context())
            {
                try
                {
                    var elementFromDb = context.Positions.FirstOrDefault(x => x.Id.Equals(idPosition));

                    context.Positions.Remove(elementFromDb);
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
