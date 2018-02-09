using CarparkExercise.RateCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CarparkExercise.Models.Enums.PayRateName;
using System;
using CarparkExercise.Models.Enums;

namespace CarparkExercise.RateCalculatorTest
{
    [TestClass]
    public class PayRateDefinerTest
    {
        private PayRateDefiner _targetClass;

        [TestInitialize]
        public void Initialize()
        {
            _targetClass = new PayRateDefiner();
        }

        [TestMethod]
        public void Define_NormalInput_NormalOutput()
        {
            // arrange
            var testSets = new(DateTime entry, DateTime exit, PayRateName expected)[]
            {
                (new DateTime(2018, 01, 01, 06, 00, 00), new DateTime(2018, 01, 01, 23, 30, 00), EarlyBird),
                (new DateTime(2018, 01, 01, 06, 00, 00), new DateTime(2018, 01, 01, 23, 30, 01), StandardRate),
                (new DateTime(2018, 01, 01, 05, 59, 59), new DateTime(2018, 01, 01, 23, 30, 00), StandardRate),
                (new DateTime(2018, 01, 01, 06, 00, 00), new DateTime(2018, 01, 02, 23, 30, 00), StandardRate),

                (new DateTime(2018, 01, 06, 06, 00, 00), new DateTime(2018, 01, 06, 23, 30, 00), WeekendRate),
                (new DateTime(2018, 01, 06, 00, 00, 00), new DateTime(2018, 01, 07, 23, 59, 59), WeekendRate),
                (new DateTime(2018, 01, 05, 23, 59, 59), new DateTime(2018, 01, 06, 23, 59, 59), StandardRate),
                (new DateTime(2018, 01, 07, 00, 00, 00), new DateTime(2018, 01, 08, 00, 00, 00), StandardRate),

                (new DateTime(2018, 01, 05, 23, 59, 59), new DateTime(2018, 01, 06, 06, 00, 00), NightRate),
                (new DateTime(2018, 01, 05, 18, 00, 00), new DateTime(2018, 01, 06, 06, 00, 00), NightRate),
                (new DateTime(2018, 01, 05, 18, 00, 00), new DateTime(2018, 01, 05, 18, 00, 01), NightRate),
            };

            // act and assert
            foreach(var testSet in testSets)
            {
                Assert.AreEqual(testSet.expected, _targetClass.Define(testSet.entry, testSet.exit));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Define_ExitEarlierThanEntry_ArgumentException()
        {
            // arrange
            var entryTime = DateTime.Now;
            var exitTime = entryTime.AddSeconds(-1);

            // act
            var actual = _targetClass.Define(entryTime, exitTime);

            // assert expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Define_ExitEqualsEntry_ArgumentException()
        {
            // arrange
            var entryTime = DateTime.Now;
            var exitTime = entryTime;

            // act
            var actual = _targetClass.Define(entryTime, exitTime);

            // assert expected exception
        }

    }
}
