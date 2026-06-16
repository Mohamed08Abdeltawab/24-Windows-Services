using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //if you in debug mode, you can run the service as a console application
            //if you in release mode, you can run the service as a windows service return false and run the service as a windows service
            if (Environment.UserInteractive)
            {
                Module2Test service = new Module2Test();
                service.StartInConsole();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new Module2Test()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
