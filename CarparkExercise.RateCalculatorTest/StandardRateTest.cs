using CarparkExercise.RateCalculator.Calculations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CarparkExercise.RateCalculatorTest
{
    [TestClass]
    public class StandardRateTest
    {
        private StandardRateCalculation _targetClass;

        [TestInitialize]
        public void Initialize()
        {
            _targetClass = new StandardRateCalculation();
        }

        [TestMethod]
        public void Calculate_NoMoreThanAnHour_5()
        {
            // arrange
            var entryTime = DateTime.Now;
            var exitTime1 = entryTime.AddSeconds(1);
            var exitTime2 = entryTime.AddMinutes(30);
            var exitTime3 = entryTime.AddHours(1);
            var expected = 5;

            // act
            var actual1 = _targetClass.Calculate(entryTime, exitTime1);
            var actual2 = _targetClass.Calculate(entryTime, exitTime2);
            var actual3 = _targetClass.Calculate(entryTime, exitTime3);

            // assert
            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
            Assert.AreEqual(expected, actual3);
        }

        [TestMethod]
        public void Calculate_NoMoreThanTwoHours_10()
        {
            // arrange
            var entryTime = DateTime.Now;
            var exitTime1 = entryTime.AddHours(1).AddSeconds(1);
            var exitTime2 = entryTime.AddHours(1).AddMinutes(30);
            var exitTime3 = entryTime.AddHours(2);
            var expected = 10;

            // act
            var actual1 = _targetClass.Calculate(entryTime, exitTime1);
            var actual2 = _targetClass.Calculate(entryTime, exitTime2);
            var actual3 = _targetClass.Calculate(entryTime, exitTime3);

            // assert
            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
            Assert.AreEqual(expected, actual3);
        }

        [TestMethod]
        public void Calculate_MoreThanTreeHours_NormalOutput()
        {
            // arrange
            var entryTime = new DateTime(2018, 01, 01, 12, 00, 00);
            var exitTime1 = new DateTime(2018, 01, 01, 15, 00, 01);
            var exitTime2 = new DateTime(2018, 01, 01, 23, 59, 59);
            var exitTime3 = new DateTime(2018, 01, 02, 00, 00, 00);
            var exitTime4 = new DateTime(2018, 01, 04, 00, 00, 00);

            var expected1 = 20;
            var expected2 = 20;
            var expected3 = 40;
            var expected4 = 80;

            // act
            var actual1 = _targetClass.Calculate(entryTime, exitTime1);
            var actual2 = _targetClass.Calculate(entryTime, exitTime2);
            var actual3 = _targetClass.Calculate(entryTime, exitTime3);
            var actual4 = _targetClass.Calculate(entryTime, exitTime4);

            // assert
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);
            Assert.AreEqual(expected4, actual4);
        }

        [TestMethod]
        public void Calculate_NoMoreThanThreeHours_15()
        {
            // arrange
            var entryTime = DateTime.Now;
            var exitTime1 = entryTime.AddHours(2).AddSeconds(1);
            var exitTime2 = entryTime.AddHours(2).AddMinutes(30);
            var exitTime3 = entryTime.AddHours(3);
            var expected = 15;

            // act
            var actual1 = _targetClass.Calculate(entryTime, exitTime1);
            var actual2 = _targetClass.Calculate(entryTime, exitTime2);
            var actual3 = _targetClass.Calculate(entryTime, exitTime3);

            // assert
            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
            Assert.AreEqual(expected, actual3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_ExitEarlierThanEntry_ArgumentException()
        {
            // arrange
            var entryTime = DateTime.Now;
            var exitTime = entryTime.AddSeconds(-1);

            // act
            var actual = _targetClass.Calculate(entryTime, exitTime);

            // assert expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_ExitEqualsEntry_ArgumentException()
        {
            // arrange
            var entryTime = DateTime.Now;
            var exitTime = entryTime;

            // act
            var actual = _targetClass.Calculate(entryTime, exitTime);

            // assert expected exception
        }
    }
}
