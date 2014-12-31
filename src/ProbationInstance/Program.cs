using System;

namespace ProbationInstance
{
    class Program
    {
        static void Main()
        {
            //configure
            Probation.Application.Initialize(config =>
                {
                    config.UseDefaultApplicationConfigurationPath();
                    config.UseConsoleLogger();
                });

            //start
            Probation.Application.Start();

            //prevent exit
            Console.ReadLine();
        }
    }
}
