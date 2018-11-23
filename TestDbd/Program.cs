using DatabaseMiddlware;

namespace ServerDb
{
    class Program
    {
        static void Main(string[] args)
        {

            DatabaseMiddlware.Class1.CreateTestUser();
            var server = new Server();
            server.Run();
        }
    }
}
