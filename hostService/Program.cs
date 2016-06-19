using System.ServiceProcess;

namespace hostService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new hostService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
