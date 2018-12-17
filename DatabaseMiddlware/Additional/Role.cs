﻿namespace DatabaseMiddlware.TypeRole
{
    public static class Role
    {
        public enum Type { 
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
    }
}
