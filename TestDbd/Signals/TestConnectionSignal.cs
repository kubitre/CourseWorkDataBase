using ServerDb.NetworkData;
using ServerDb.ServerData;
using System;
using System.Text;

namespace ServerDb.Signals
{
    public class TestConnectionSignal : ISignal
    {
        public const string ActionType = "testconnection";

        public bool HandleCreate(string Payload, StateObject state) => ActionWorker.FinishingTaskSolve(ActionType, false, "not handled!", state);

        public bool HandleDelete(string Payload, StateObject state) => ActionWorker.FinishingTaskSolve(ActionType, false, "not handled!", state);

        public bool HandleGet(string Payload, StateObject state)
        {
            return ActionWorker.FinishingTaskSolve(ActionType, true, "test connection was successfull!", state);
        }

        public bool HandleUpdate(string Payload, StateObject state) => ActionWorker.FinishingTaskSolve(ActionType, false, "not handled!", state);
    }
}
