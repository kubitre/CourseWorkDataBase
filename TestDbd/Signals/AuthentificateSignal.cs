using System;

namespace ServerDB.Signals
{
    [Serializable]
    class AuthentificateSignal : IPayload
    {
        public const string ActionType = "authentification";
        public string Payload;

        public AuthentificateSignal()
        {
        }

        public AuthentificateSignal(string payload)
        {
            this.Payload = payload;
        }

        public void Setting(string payload)
        {
            this.Payload = payload;
        }
    }
}
