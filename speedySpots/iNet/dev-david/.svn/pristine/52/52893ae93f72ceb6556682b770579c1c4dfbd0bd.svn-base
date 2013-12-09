using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using log4net;

namespace InvoiceWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            log4net.Config.XmlConfigurator.Configure();

            ServicesToRun = new ServiceBase[] 
			{ 
				new SSInvoiceWindowsService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
