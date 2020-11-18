using System;

namespace ResultSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Refactoring_0.UserService();
            service.UpdateUserName("1", "Foo");
        }
    }
}
