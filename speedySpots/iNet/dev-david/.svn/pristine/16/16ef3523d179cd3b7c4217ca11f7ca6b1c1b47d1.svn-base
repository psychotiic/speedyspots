using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace InvoiceWindowsService
{
    public partial class SSInvoiceWindowsService : ServiceBase
    {
        Controller _controller;
      
        public SSInvoiceWindowsService()
        {
            //Utilities.WriteEventLog("SSInvoiceWindowsService", "Applications", "Create");
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Utilities.WriteEventLog("SSInvoiceWindowsService", "Applications", "Service starting");
            _controller = new Controller();
            _controller.StartServiceProcess();
            //Utilities.WriteEventLog("SSInvoiceWindowsService", "Applications", "Service has started");
        }

        protected override void OnStop()
        {
            _controller.EndServiceProcess();
            //Utilities.WriteEventLog("SSInvoiceWindowsService", "Applications", "Service has stopped");
        }

    }
}
