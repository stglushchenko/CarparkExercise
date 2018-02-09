using CarparkExercise.Infrastructure.Exceptions;
using CarparkExercise.IO;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Globalization;
using System.IO;

namespace CarparkExercise.IOTest
{
    [TestClass]
    public class ConsoleInputReaderTest
    {
        private ConsoleInputReader _targetClass;

        [TestInitialize]
        public void Initianlize() => _targetClass = new ConsoleInputReader(Mock.Of<ILogger>());

        [DataTestMethod]
        [DataRow("2018-01-02 13:30", "2095-03-01 00:00")]
        public void Read_NormalInput_NormalOutput(string entry, string exit)
        {
            // arrange
            var input = $"{entry}\r\n{exit}\r\n";

            var expected = (DateTime.Parse(entry), DateTime.Parse(exit));

            (DateTime entry, DateTime exit) actual;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (var sr = new StringReader(input))
                {
                    Console.SetIn(sr);

                    // act
                    actual = _targetClass.Read();
                }
            }

            // assert
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("2018-01-02 13:30", "WrongParam")]
        [DataRow("WrongParam", "2095-03-01 00:00")]
        [ExpectedException(typeof(ConsoleInputException))]
        public void Read_NotParseable_Exception(string entry, string exit)
        {
            // arrange
            var input = $"{entry}\r\n{exit}\r\n";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (var sr = new StringReader(input))
                {
                    Console.SetIn(sr);

                    // act
                    _targetClass.Read();
                }
            }

            // assert expect exception
        }
    }
}
