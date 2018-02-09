using CarparkExercise.Infrastructure.Interfaces.IO;
using System;

namespace CarparkExercise.IO {

    public class ConsoleResultWriter: IConsoleResultWriter
    {
        private const string _payRateNameFormat = "Name of the Rate: {0}";
        private const string _totalPriceFormat = "Total price: {0:C2}";

        public void Write(string payRateName, decimal totalPrice)
        {
            Console.WriteLine(_payRateNameFormat, payRateName);
            Console.WriteLine(_totalPriceFormat, totalPrice);
        }
    }

}