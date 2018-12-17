using ServerDb.ServerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.Signals
{
    public static class ActionWorker
    {
        public static bool FinishingTaskSolve(string actionType, bool operationResolved, string messageForSend, StateObject state)
        {
            NetworkData.ReponseAllRequests response;

            if (operationResolved)
            {
                response = new NetworkData.ReponseAllRequests
                {
                    Code = actionType,
                    Status = NetworkResponseCodes.StatusCode.STATUS_SUCCESS,
                    Reponse = messageForSend
                };
            }
            else
            {
                response = new NetworkData.ReponseAllRequests
                {
                    Code = actionType,
                    Status = NetworkResponseCodes.StatusCode.STATUS_FAILED,
                    Reponse = messageForSend
                };
            }

            var byteForResponse = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(response));

            try
            {
                state.WorkSocket.BeginSend(byteForResponse, 0, byteForResponse.Length, 0, new AsyncCallback(ServerHandlers.SendCallBack.SendCallback), state.WorkSocket);
                ServerDb.Server.allDone.Set();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOG {DateTime.Now}| ActionWorker]: Error for code: {ex.Message}");
                throw new ArgumentException($"Error with code: {ex.Message}");
            }
        }
    }
}
