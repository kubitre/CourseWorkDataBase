using ServerDB.ServerData;
using DatabaseMiddlware.Workers.Users;
using ServerDB.ModelForTransmition;
using System;

namespace ServerDB.HandlersForRequest
{
    class Authentification : IHandler
    {
        public static string ActionType = "authentification";
        public string Action = ActionType;
        public string Payload = "";

        private static string PatternForCondition = "";

        public Authentification() { }

        public Authentification(string payload)
        {
            this.Payload = payload;
        }

        public void hand(StateObject state)
        {
            try
            {
                var authTok = (Auth)Json.JsonParser.Deserialize(state.MessageForReciveData.ToString());

                if (UserHandler.Authentificate(authTok.Username, authTok.Password))
                {
                    ServerHandlers.SendHandler.Send(state.WorkSocket, $"authentificate succes! token : {Guid.NewGuid()}");
                }
                else
                {
                    ServerHandlers.SendHandler.Send(state.WorkSocket, "authentificate error! error code: User does not exist in database!");
                }
            }
            catch(Exception ex)
            {
                ServerDb.Server.NewExceptionRequest(ex.Message);
                ServerHandlers.SendHandler.Send(state.WorkSocket, "authentificate error! Does not prepare recieved data !");
            }
        }
    }
}
