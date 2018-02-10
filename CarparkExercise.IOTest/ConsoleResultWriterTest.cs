using CarparkExercise.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace CarparkExercise.IOTest
{
    [TestClass]
    public class ConsoleResultWriterTest
    {
        private const string _currencyFormat = "{0:C2}";
        private ConsoleResultWriter _targetClass;

        [TestInitialize]
        public void Initialize() => _targetClass = new ConsoleResultWriter();

        [TestMethod]
        public void Write_NormalInput_NormalOutput()
        {
            var payRateName = "Test Rate";
            var totalPrice = 123m;

            var expected = $"Name of the Rate: Test Rate\r\nTotal price: {string.Format(_currencyFormat, 123m)}\r\n";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // act
                _targetClass.Write(payRateName, totalPrice);

                // assert
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}
