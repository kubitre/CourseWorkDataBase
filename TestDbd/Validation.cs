using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerDb.ValidatorsAndCheckers
{
    internal static class Validation
    {
        private const string patternLogin = "([a-zA-Z]{4,}[0-9]{2,})";
        private const string patternPassword = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
        private static int[] LengthLogin = { 6, 30 };
        private static int[] LengthPassword = { 6, 30 };
        private static string prefix = "";
        private static string data = "";

        public static bool Validate(
            int level, 
            string excuteString)
        {
            string paternForRegularExpression = "";
            int[] length;

            switch (level)
            {
                case 0:
                    paternForRegularExpression = patternLogin;
                    length = LengthLogin;
                    prefix = "логин";
                    data = "строчные или заглавные буквы, в количестве, не менее 4, а также цифры, не менее 2!";

                    break;
                case 1:
                    paternForRegularExpression = patternPassword;
                    length = LengthPassword;
                    prefix = "пароль";
                    data = "8 символов, из которых должна быть как минимум 1 заглавная буква, 1 прописная, и 1 цифра!";

                    break;
                default:
                    throw new ArgumentException("don't have anything stage!");
            }

            try
            {
                return StageWorker(excuteString, paternForRegularExpression, length);
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        private static bool StageWorker(
            string excuteString, 
            string pattern,
            int[] length)
        {
            try
            {
                if (
                    StageLengthAndContentChecker(excuteString, length[0], length[1])
                    &
                    StageRegularVerbsChecker(excuteString, pattern)
                )
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            
        }

        private static bool StageRegularVerbsChecker(
            string excuteString, 
            string pattern)
        {
            var regularExpression = new Regex(pattern);
            var matches = regularExpression.Matches(excuteString);

            if (matches.Count == 1)
                return true;
            else
                throw new ArgumentException("Введённые данные не прошли проверку! Пожалуйста, убедитесь, что ваш " + prefix + " содержит "+ data);
        }

        private static bool StageLengthAndContentChecker(
            string excuteString,
            int minLength,
            int maxLength)
        {
            if (string.IsNullOrEmpty(excuteString) | excuteString.Length < minLength | excuteString.Length > maxLength)
                throw new ArgumentException("Введённые вами данные не прошли проверку! Пожалуйста, убедитесь, что вы вводите как минимум " + minLength + " символов и не более " + maxLength);

            return true;
        }
    }
}
