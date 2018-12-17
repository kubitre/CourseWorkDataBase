using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel
{
    public class SettingsTheme
    {
        public string NameAppTheme { get; set; }
        public string NameAccentTheme { get; set; }

        public void ChangeAppTheme()
        {
            if (this.NameAppTheme == "BaseDark")
                this.NameAppTheme = "BaseLight";
            else
                this.NameAppTheme = "BaseDark";
        }
        public void ChangeAppTheme(string themeName) => this.NameAppTheme = themeName;
    }
}
