

using ServerDB.ServerData;
using ServerDB.Signals;
using System;
using System.Text.RegularExpressions;

namespace ServerDB.HandlersForRequest
{
    public class HandlerRequestBuilder
    {
        private static string _patternRequest = "[a-zA-Z0-9 ]{2,}";

        public static IHandler RequestBuild(StateObject state)
        {

            foreach (var action in new Regex(_patternRequest).Matches(state.MessageForReciveData.ToString()))
            {
                if ((action as Match).Success)
                {
                    switch((action as Match).Value)
                    {
                        case AuthentificateSignal.ActionType:
                            Console.WriteLine($"[LOG {DateTime.Now}]: Authentification action type!");
                            return new Authentification(state.MessageForReciveData.ToString());

                        case TestConnectionSignal.ActionType:
                            Console.WriteLine($"[LOG {DateTime.Now}]: Test Connection action type!");
                            return new TestConnection();

                        case GetDishSignal.ActionType:
                            Console.WriteLine($"[LOG {DateTime.Now}]: Get Dish action type!");
                            return new GetDish();

                        case GetMenuSignal.ActionType:
                            break;

                        case GetCooperatorSignal.ActionType:
                            break;
                    }
                }
            }

            return new NullHandler();
        }
    }
}
