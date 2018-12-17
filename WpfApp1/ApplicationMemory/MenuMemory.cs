using System.Collections.Generic;
using System.Linq;
using networkMenu = AdminPanel.NetworkMiddleware.NetworkData.Menu;


namespace AdminPanel.ApplicationMemory
{
    public class MenuMemory
    {
        private List<networkMenu> _menus;

        public MenuMemory()
        {
            this._menus = new List<networkMenu>();
        }

        public void AddNewMenu(networkMenu menu)
        {
            if(this._menus != null)
            {
                this._menus.Add(menu);
            }
            else
            {
                this._menus = new List<networkMenu>();
                this._menus.Add(menu);
            }
        }

        public networkMenu GetMenu() => this._menus.LastOrDefault();
        public List<networkMenu> GetMenus() => this._menus;
    }
}
