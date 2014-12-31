using System;

namespace FixerInstance
{
    class Program
    {
        static void Main()
        {
            //configure
            Fixer.Application.Initialize(config =>
                {
                    config.UseDefaultApplicationConfigurationPath();
                    config.UseConsoleLogger();
                });

            //start
            Fixer.Application.Start();

            //prevent exit
            Console.ReadLine();
        }
    }
}
