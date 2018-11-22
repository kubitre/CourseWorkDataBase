namespace DatabaseMiddlware.Workers.Users
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using DatabaseMiddlware.Models;
    using DatabaseMiddlware.TypeRole;

    public static class UserHandler
    {
        public static void CreateNewUser(string username, string password, int roleId, User creator)
        {
            if (creator.Role.Equals(Role.Type.Administrator) | creator.Role.Equals(Role.Type.Programmer))
            {
                var newUser = new User { Name = username, Password = GetHashSha(password), Role = roleId, LastUpdated = DateTime.Now };
            }
            else if (creator.Role.Equals(Role.Type.Calculator))
                throw new Exception("Calculator was not access for create new users!");
            else
                throw new Exception("Nobody was create new user!");
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
