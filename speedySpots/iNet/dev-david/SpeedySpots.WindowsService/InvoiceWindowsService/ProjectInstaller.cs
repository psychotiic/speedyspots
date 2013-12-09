using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace InvoiceWindowsService
{
   [RunInstaller(true)]
   public partial class ProjectInstaller : System.Configuration.Install.Installer
   {
      public ProjectInstaller()
      {
         InitializeComponent();
      }
   }
}
