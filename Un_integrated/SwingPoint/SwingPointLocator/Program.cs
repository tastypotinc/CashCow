#region Namespaces

using System;
using SwingPointLocator.Classes;

#endregion Namespaces

namespace SwingPointLocator
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Locating swing points ...");

            var swingPointLocatorService = new SwingPointLocatorService();
            swingPointLocatorService.LocateSwingPoints(@"../../Data/Downloads", @"../../Data/SwingPoint");

            Console.WriteLine("... Done locating swing points");
            Console.ReadKey();
        }
    }
}
