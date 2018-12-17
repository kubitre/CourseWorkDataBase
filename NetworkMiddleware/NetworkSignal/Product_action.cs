using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class Product_action : ISignal
    {
        public const string ActionTypeGet = "product_get";
        public const string ActionTypeCreate = "product_create";
        public const string ActionTypeUpdate = "product_update";
        public const string ActionTypeDelete = "product_delete";

        public bool Handle(StateObject state, params object[] param)
        {
            try
            {
                if (param.Length != 1)
                    throw new ArgumentException("Must be 1 parameter: count cooperators need get!");


                var payload = new Payload
                {
                    Count = (int)param[0],
                    Offset = 0
                };

                var messageForSend = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                return new Action_worker().HandleWorker(state, messageForSend, NetworkResponseCodes.ProductCodes.PRODUCT_GET_CODE);
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

                return new Action_worker().HandleWorker(state, id.ToString(), NetworkResponseCodes.ProductCodes.PRODUCT_DELETE_CODE);
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
                var network_product = (NetworkData.Product)param[0];

                var messageForSend = Newtonsoft.Json.JsonConvert.SerializeObject(network_product);
                return new Action_worker().HandleWorker(state, messageForSend, NetworkResponseCodes.ProductCodes.PRODUCT_CREATE_CODE);
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
                var payload = (string)param[0];

                return new Action_worker().HandleWorker(state, payload, NetworkResponseCodes.ProductCodes.PRODUCT_UPDATE_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
