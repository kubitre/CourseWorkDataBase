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
            Calculator = 2,
            Recruitment = 3,
            Cook = 4, 
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
                case 3:
                    return Type.Recruitment.ToString();
                case 4:
                    return Type.Cook.ToString();
                default:
                    return "";
            }
        }

        public static string GetRoleOnRussian(int index)
        {
            switch (index)
            {
                case 0:
                    return "Администратор";
                case 1:
                    return "Программист";
                case 2:
                    return "Бухгалтер";
                case 3:
                    return "Кадровый агент";
                case 4:
                    return "Повар";

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

                case "Recruitment":
                    return 3;

                case "Cook":
                    return 4;

                default:
                    return -1;
            }
        }
    }
}
