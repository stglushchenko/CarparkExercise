using CarparkExercise.Infrastructure.Exceptions;
using CarparkExercise.Infrastructure.Interfaces.RateCalculator;
using CarparkExercise.Models.Enums;
using CarparkExercise.RateCalculator;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using static CarparkExercise.Models.Enums.PayRateName;

namespace CarparkExercise.RateCalculatorTest
{
    [TestClass]
    public class CalculationStrategyTest
    {
        private CalculationStrategy _targetClass;

        [TestInitialize]
        public void Initialize()
        {
            var earlyBird = new Mock<ICalculation>();
            earlyBird.Setup(c => c.AppliesTo(EarlyBird)).Returns(true);
            earlyBird.SetReturnsDefault(1m);
            earlyBird.SetReturnsDefault("Early Bird");

            var standard = new Mock<ICalculation>();
            standard.Setup(c => c.AppliesTo(StandardRate)).Returns(true);
            standard.SetReturnsDefault(2m);
            standard.SetReturnsDefault("Standard Rate");

            var calculationTypes = new ICalculation[]
            {
                earlyBird.Object,
                standard.Object
            };

            _targetClass = new CalculationStrategy(calculationTypes, Mock.Of<ILogger>());
        }

        [DataTestMethod]
        [DataRow(EarlyBird, 1)]
        [DataRow(StandardRate, 2)]
        public void Calculate_KnownCalcType_NormalOutput(PayRateName payRateName, int expected)
        {
            // arrange

            // act
            var actual = _targetClass.Calculate(payRateName, DateTime.Now, DateTime.Now);

            // assert
            Assert.AreEqual(Convert.ToDecimal(expected), actual);
        }

        [DataTestMethod]
        [DataRow(EarlyBird, "Early Bird")]
        [DataRow(StandardRate, "Standard Rate")]
        public void GetName_KnownCalcType_NormalOutput(PayRateName payRateName, string expected)
        {
            // arrange

            // act
            var actual = _targetClass.GetName(payRateName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedStrategyException))]
        public void Calculate_UnknownCalcType_NotImplementedExeption()
        {
            // arrange

            // act
            _targetClass.Calculate(NightRate, DateTime.Now, DateTime.Now);

            // assert expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedStrategyException))]
        public void GetName_UnknownCalcType_NotImplementedExeption()
        {
            // arrange

            // act
            _targetClass.GetName(NightRate);

            // assert expected exception
        }
    }
}
