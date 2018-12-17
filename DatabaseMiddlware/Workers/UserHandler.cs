namespace DatabaseMiddlware.Workers.Users
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using DatabaseMiddlware.ModelForTransmition;
    using DatabaseMiddlware.Models;
    using DatabaseMiddlware.ObjectsAfterDb;
    using DatabaseMiddlware.TypeRole;
    using User = Models.User;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public static class UserHandler
    {
        public static void CreateNewUser(NewUser newUser, User creator)
        {
            if (newUser == null)
                throw new ArgumentNullException("new user can not be null!");

            using(var context = new Context())
            {

                if (creator.Role.Equals(Role.Type.Administrator) | creator.Role.Equals(Role.Type.Programmer))
                {
                    var creationUser = new User { Name = newUser.UserName, Password = GetHashSha(newUser.Password), Role = newUser.Role, LastUpdated = DateTime.Now, LastEnter = DateTime.Now };

                    context.Users.Add(creationUser);
                    context.SaveChanges();
                }
                else if (creator.Role.Equals(Role.Type.Calculator))
                    throw new Exception("Calculator was not access for create new users!");
                else
                    throw new Exception("Nobody was create new user!");
            }

        }

        public static bool UpdatePassword(string newPassword, Guid idUser)
        {
            if (idUser == null | string.IsNullOrEmpty(newPassword) | string.IsNullOrWhiteSpace(newPassword))
                return false;

            using (var context = new Context())
            {
                var userFromDb = context.Users.FirstOrDefault();

                if (userFromDb == null)
                    return false;

                try
                {
                    userFromDb.Password = GetHashSha(newPassword);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool UpdateUsername(string username, Guid idUser)
        {
            if (idUser == null | string.IsNullOrEmpty(username) | string.IsNullOrWhiteSpace(username))
                return false;

            using (var context = new Context())
            {
                var userFromDb = context.Users.FirstOrDefault();

                if (userFromDb == null)
                    return false;

                try
                {
                    userFromDb.Name = username;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool Authentificate(string username, string password)
        {
            using (var context = new Context())
            {
                var userFromDb = context.Users.Where(x => x.Name.Equals(username) & x.Password.Equals(GetHashSha(password))).FirstOrDefault();

                if(userFromDb == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static int Authentificate(AuthBLock token)
        {
            using (var context = new Context())
            {
                var userFromDb = context.Users.Where(x => x.Name.Equals(token.Username) & x.Password.Equals(GetHashSha(token.Password))).FirstOrDefault();
                

                if (userFromDb == null)
                {
                    return -1;
                }
                else
                {
                    userFromDb.LastEnter = token.Enter;
                    context.SaveChanges();
                    return userFromDb.Role;
                }
            }
        }

        public static List<UserNetwork> GetUsers(int offset, int amount)
        {
            using(var context = new Context())
            {
                var listResponse = new List<UserNetwork>();
                var usersFromDb = context
                                            .Users
                                            .Skip(offset)
                                            .Take(amount)
                                            .Include(x => x.Cooperator)
                                            .ToList();

                foreach(var element in usersFromDb)
                    listResponse.Add(new UserNetwork
                    {
                        Id = element.Id,
                        Username = element.Name,
                        Cooperator = element.Cooperator.FirstName,
                        LastEnter = element.LastEnter,
                        Role = Role.GetRole(element.Role)
                    });

                return listResponse;
                
            }
        }

        public static bool DeleteUser(Guid idUser)
        {
            using(var context =new Context())
            {
                try
                {
                    var userFromDb = context.Users.FirstOrDefault(x => x.Id.Equals(idUser));

                    context.Users.Remove(userFromDb);
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static string GetHashSha(string str)
        {
            using (var sha512Hash = SHA512.Create())
            {
                var fromStrToBytes = Encoding.UTF8.GetBytes(str);
                var hashBytes = sha512Hash.ComputeHash(fromStrToBytes);
                string hash = Encoding.UTF8.GetString(hashBytes);
                return hash;
            }
        }
    }
}
