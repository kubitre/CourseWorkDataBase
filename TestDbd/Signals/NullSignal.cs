using ServerDb.ServerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.Signals
{
    public class NullSignal : ISignal
    {
        public string ActionType = "null";

        public bool HandleCreate(string Payload, StateObject state) => ActionWorker.FinishingTaskSolve(ActionType, false, "error request!", state);

        public bool HandleDelete(string Payload, StateObject state) => ActionWorker.FinishingTaskSolve(ActionType, false, "error request!", state);

        public bool HandleGet(string Payload, StateObject state) => ActionWorker.FinishingTaskSolve(ActionType, false, "error request!", state);

        public bool HandleUpdate(string Payload, StateObject state) => ActionWorker.FinishingTaskSolve(ActionType, false, "error request!", state);
    }
}
