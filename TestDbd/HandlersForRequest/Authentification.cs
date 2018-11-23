namespace ServerDB.HandlersForRequest
{
    class Authentification : IHandler
    {
        private string payload = "";

        public Authentification(string payload)
        {
            this.payload = payload;
        }

        public void hand()
        {
            throw new System.NotImplementedException();
        }
    }
}
