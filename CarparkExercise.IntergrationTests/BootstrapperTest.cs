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
        [DataRow("2018-01-01 06:00", "2018-01-01 23:30", "Early Bird", 13)]
        [DataRow("2018-01-01 09:00", "2018-01-01 15:30", "Early Bird", 13)]
        [DataRow("2018-01-01 09:01", "2018-01-01 15:30", "Standard Rate", 20)]
        [DataRow("2018-01-01 09:00", "2018-01-01 15:29", "Standard Rate", 20)]
        [DataRow("2018-01-01 05:59", "2018-01-01 23:30", "Standard Rate", 20)]
        [DataRow("2018-01-01 06:00", "2018-01-01 23:31", "Standard Rate", 20)]
        [DataRow("2018-01-01 06:00", "2018-01-02 23:30", "Standard Rate", 40)]
        [DataRow("2018-01-06 06:00", "2018-01-06 23:30", "Weekend Rate", 10)]
        [DataRow("2018-01-06 00:00", "2018-01-07 23:59", "Weekend Rate", 10)]
        [DataRow("2018-01-06 06:00", "2018-01-14 23:59", "Standard Rate", 180)]
        [DataRow("2018-01-05 23:59", "2018-01-07 23:59", "Standard Rate", 60)]
        [DataRow("2018-01-07 00:00", "2018-01-08 00:00", "Standard Rate", 40)]
        [DataRow("2018-01-05 23:59", "2018-01-06 06:00", "Night Rate", 6.50)]
        [DataRow("2018-01-05 18:00", "2018-01-06 06:00", "Night Rate", 6.50)]
        [DataRow("2018-01-05 18:00", "2018-01-05 18:01", "Night Rate", 6.50)]
        public void Run_NormalInput_NormalOutput(string entry, string exit, string expectedPayRateName, double expectedTotalCost)
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
