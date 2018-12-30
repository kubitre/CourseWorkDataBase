using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class Position_action : ISignal
    {
        public const string ActionTypeGet = "position_get";
        public const string ActionTypeCreate = "position_create";
        public const string ActionTypeUpdate = "position_update";
        public const string ActionTypeDelete = "position_delete";


        public bool Handle(StateObject state, params object[] param)
        {
            try
            {
                Models.Payload payload;

                if (param.Length == 1)
                    payload = new Models.Payload { Count = (int)param[0], Offset = 0 };
                else if (param.Length == 2)
                    payload = new Models.Payload { Count = (int)param[0], Offset = (int)param[1] };
                else
                    payload = new Models.Payload { Count = 0, Offset = 0 };

                var messageForSend = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                return new Action_worker().HandleWorker(state, messageForSend, NetworkResponseCodes.PositionCodes.POSITION_GET_CODE);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool HandleDelete(StateObject state, params object[] param)
        {
            try
            {
                if (param.Length != 1)
                    throw new ArgumentException("error lenght!");

                var id = (Guid)param[0];

                return new Action_worker().HandleWorker(state, id.ToString(), NetworkResponseCodes.PositionCodes.POSITION_DELETE_CODE);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool HandleSet(StateObject state, params object[] param)
        {
            try
            {
                return new Action_worker().HandleWorker(state, (string)param[0], NetworkResponseCodes.PositionCodes.POSITION_CREATE_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool HandleUpdate(StateObject state, params object[] param)
        {
            try
            {
                var objectt = new NetworkData.Position
                {
                    Id = (Guid)param[0],
                    Name = (string)param[1]
                };

                return new Action_worker().HandleWorker(state, Newtonsoft.Json.JsonConvert.SerializeObject(objectt), NetworkResponseCodes.PositionCodes.POSITION_UPDATE_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
