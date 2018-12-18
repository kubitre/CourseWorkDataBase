using AdminPanel.NetworkMiddleware.NetworkData;
using System;

namespace AdminPanel.ApplicationMemory
{
    public class UserMemory
    {
        private User currentUser;

        public UserMemory()
        {
            this.currentUser = null;
        }

        public UserMemory(User newUser)
        {
            this.currentUser = newUser;
        }

        public void SetNewUser(User newUser)
        {
            this.currentUser = newUser;
        }

        public string GetUserName() => this.currentUser.UserName;
        public Guid GetId() => this.currentUser.Id;
        public string GetUserRole() => AdminPanel.Models.Role.GetRole(Int32.Parse(this.currentUser.AuthCode));
        public string GetUserRoleOnRussian() => AdminPanel.Models.Role.GetRoleOnRussian(Int32.Parse(this.currentUser.AuthCode));
    }
}
