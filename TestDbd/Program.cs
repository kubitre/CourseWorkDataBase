using DatabaseMiddlware;

namespace ServerDb
{
    class Program
    {
        static void Main(string[] args)
        {

            //DatabaseMiddlware.Class1.CreateTestUser();
            //new DatabaseMiddlware.Class1().RunFil();
            //new DatabaseMiddlware.Class1().FillProductAndDish();
            //new DatabaseMiddlware.Class1().FillTestDishes();

            //new DatabaseMiddlware.Class1().FillMenu();
            var server = new Server();
            server.Run();
        }
    }
}
