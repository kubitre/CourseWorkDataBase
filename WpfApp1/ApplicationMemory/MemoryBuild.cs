using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using networkDish = AdminPanel.NetworkMiddleware.NetworkData.Dish;
using networkCooperator = AdminPanel.NetworkMiddleware.NetworkData.Cooperator;

namespace AdminPanel.ApplicationMemory
{
    public class MemoryBuild
    {
        private UserMemory          _memoryUser;
        private List<string>        _historyOpened;
        private DishMemory          _dishMemory;
        private CooperatorMemory    _cooperatorMemory;
        private MenuMemory          _menuMemory;
        private SettingsTheme       _settingsTheme;
        private string              _applicationType;

        public MemoryBuild()
        {
            this._memoryUser = new UserMemory();
            this._historyOpened = new List<string>();
            this._dishMemory = new DishMemory();
            this._cooperatorMemory = new CooperatorMemory();
            this._menuMemory = new MenuMemory();
            this._settingsTheme = new SettingsTheme() { NameAppTheme = "BaseDark", NameAccentTheme = "Cobalt" };
        }

        public void SetUpApplicationType(int type)
        {
            switch (type)
            {
                case 0:
                    this._applicationType = "admin";
                    break;
                case 1:
                    this._applicationType = "client";
                    break;
            }
        }

        public void AddToHistory(string newAction) => this._historyOpened.Add(newAction);
        public string GetUserName() => this._memoryUser.GetUserName();
        public List<networkDish> GetAllLocalDishes() => this._dishMemory.GetDishes();
        public List<networkCooperator> GetAllLocalCooperators() => this._cooperatorMemory.GetCooperators();
        public networkCooperator GetCooperatorByUser() => this._cooperatorMemory.GetCooperatorByUser(this._memoryUser.GetId());
        public void SetUser(NetworkMiddleware.NetworkData.User user) => this._memoryUser.SetNewUser(user);
        public void ChangeAppTheme() => this._settingsTheme.ChangeAppTheme();
        public string GetAppTheme() => this._settingsTheme.NameAppTheme;
        public string GetAppAccentTheme() => this._settingsTheme.NameAccentTheme;
        public void ChangeAppTheme(string nameTheme) => this._settingsTheme.ChangeAppTheme(nameTheme);
        public void AddUser(NetworkMiddleware.NetworkData.User user) => this._memoryUser.SetNewUser(user);
        public string GetUserRole() => this._memoryUser.GetUserRole();
        public string GetuserRoleOnRussian() => this._memoryUser.GetUserRole();
        public string GetTypeApplication() => this._applicationType;
    }
}
