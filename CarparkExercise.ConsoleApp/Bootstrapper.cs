using CarparkExercise.Infrastructure.Interfaces.IO;
using CarparkExercise.Infrastructure.Interfaces.RateCalculator;
using CarparkExercise.Infrastructure.Interfaces.Workflows;
using CarparkExercise.IO;
using CarparkExercise.RateCalculator;
using CarparkExercise.RateCalculator.Calculations;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using Unity;
using Unity.Injection;

namespace CarparkExercise.ConsoleApp
{
    public class Bootstrapper
    {
        private IUnityContainer _container;

        private string _defaultLogFileName = "Logs/ParcelsExercise-.log";
        private string _defaultLogCategoryName = "MainLog";

        public void ConfigureContainer()
        {
            var loggerFactory = ConfigureLogging();

            _container = new UnityContainer()
                .RegisterInstance(loggerFactory.CreateLogger(_defaultLogCategoryName))
                .RegisterSingleton<IConsoleInputReader, ConsoleInputReader>()
                .RegisterSingleton<IConsoleResultWriter, ConsoleResultWriter>()
                .RegisterSingleton<IWorkflow, MainWorkflow>()
                .RegisterSingleton<IPayRateDefiner, PayRateDefiner>()
                .RegisterType<ICalculation, EarlyBirdCalculation>("earlyBirdCalculation")
                .RegisterType<ICalculation, NightRateCalculation>("nightRateCalculation")
                .RegisterType<ICalculation, StandardRateCalculation>("standardRateCalculation")
                .RegisterType<ICalculation, WeekendRateCalculation>("weekendRateCalculation")
                .RegisterType<ICalculationStrategy, CalculationStrategy>(
                    new InjectionConstructor(
                        new ResolvedArrayParameter<ICalculation>(
                            new ResolvedParameter<ICalculation>("earlyBirdCalculation"),
                            new ResolvedParameter<ICalculation>("nightRateCalculation"),
                            new ResolvedParameter<ICalculation>("standardRateCalculation"),
                            new ResolvedParameter<ICalculation>("weekendRateCalculation")
                            
                        ),
                        new ResolvedParameter<Microsoft.Extensions.Logging.ILogger>()
                    )
                );
        }

        public void Run()
        {
            try
            {
                _container.Resolve<IWorkflow>().Run();
            }
            catch (Exception ex)
            {
                _container.Resolve<Microsoft.Extensions.Logging.ILogger>().LogCritical(ex, "The unexpexted exception occured");
                throw;
            }
        }

        private ILoggerFactory ConfigureLogging()
        {
            var minimumLogLevel = LogEventLevel.Information;
#if DEBUG
            minimumLogLevel = LogEventLevel.Debug;
#endif
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.File(_defaultLogFileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog(dispose: true);
            return loggerFactory;
        }
    }
}
