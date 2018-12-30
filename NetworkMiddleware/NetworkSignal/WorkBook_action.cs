using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class WorkBook_action : ISignal
    {
        public const string ActionTypeGet = "workbook_get";
        public const string ActionTypeCreate = "workbook_create";
        public const string ActionTypeUpdate = "workbook_update";
        public const string ActionTypeDelete = "workbook_delete";

        public bool Handle(StateObject state, params object[] param)
        {
            try
            {
                AdminPanel.Models.Payload payload;   
                if(param.Length == 1)
                {
                    payload = new Models.Payload
                    {
                        Count = (int)param[0],
                        Offset = 0
                    };
                }
                else
                {
                    payload = new Models.Payload
                    {
                        Count = 50,
                        Offset = 0
                    };
                }

                return new Action_worker().HandleWorker(state, Newtonsoft.Json.JsonConvert.SerializeObject(payload), NetworkResponseCodes.WorkBookCodes.WORKBOOK_GET_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool HandleDelete(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }

        public bool HandleSet(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }

        public bool HandleUpdate(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
