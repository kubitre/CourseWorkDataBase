namespace ServerDB.Signals
{
    class GetCooperatorSignal: IPayload
    {
        public const string ActionType = "getcooperator";
        private string Payload = "";

        public void Setting(string payload)
        {
            this.Payload = payload;
        }
    }
}
