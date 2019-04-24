using System;
using System.Diagnostics;

namespace CookEat
{
    public static class ExceptionHandler
    {
        public static void Handle(Exception exception)
        {
            Console.WriteLine($"WARNING: {exception.ToStringDemystified()}");
        }
    }
}