using CarparkExercise.ConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace CarparkExercise.IntergrationTests
{
    [TestClass]
    public class BootstrapperTest
    {
        private Bootstrapper _targetClass;

        [TestInitialize]
        public void Initialize()
        {
            _targetClass = new Bootstrapper();
            _targetClass.ConfigureContainer();
        }

        [DataTestMethod]
        [DataRow("2018-01-02 13:30", "2095-03-01 00:00", "Standard Rate", 563660)]
        public void Run_NormalInput_NormalOutput(string entry, string exit, string expectedPayRateName, int expectedTotalCost)
        {
            // arrange
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Entry date and time (yyyy-MM-dd hh:mm): Exit date and time (yyyy-MM-dd hh:mm): ");
            stringBuilder.Append($"Name of the Rate: {expectedPayRateName}\r\nTotal price: {expectedTotalCost:C2}\r\n");

            var expected = stringBuilder.ToString();
            string actual = null;
            var input = $"{entry}\r\n{exit}\r\n";

            // act
            using (var streamWriter = new StringWriter())
            {
                Console.SetOut(streamWriter);

                using (var stramReader = new StringReader(input))
                {
                    Console.SetIn(stramReader);

                    // act
                    _targetClass.Run();

                    actual = streamWriter.ToString();
                }
            }

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
