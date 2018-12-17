using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public static class Role
    {
        public enum Type
        {
            Administrator = 0,
            Programmer = 1,
            Calculator = 2
        }

        public static string GetRole(int index)
        {
            switch (index)
            {
                case 0:
                    return Type.Administrator.ToString();
                case 1:
                    return Type.Programmer.ToString();
                case 2:
                    return Type.Calculator.ToString();
                default:
                    return "";
            }
        }

        public static int GetIndexRole(string role)
        {
            switch (role)
            {
                case "Administrator":
                    return 0;

                case "Programmer":
                    return 1;

                case "Calculator":
                    return 2;

                default:
                    return -1;
            }
        }
    }
}
