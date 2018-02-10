using System;
using System.Globalization;
using System.Threading;

namespace CarparkExercise.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var auCultureInfo = new CultureInfo("en-AU");
            CultureInfo.DefaultThreadCurrentCulture = auCultureInfo;
            Thread.CurrentThread.CurrentCulture = auCultureInfo;

            var bootstrapper = new Bootstrapper();
            bootstrapper.ConfigureContainer();
            try
            {
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
