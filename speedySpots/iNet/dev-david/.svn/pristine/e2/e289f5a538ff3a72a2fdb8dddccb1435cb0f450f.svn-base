namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using Objects;

   public partial class ajax_validate_payment : AjaxBasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         var sResponse = "error";
         Response.Clear();

         try
         {
            if (!IsPostBack)
            {
               if (!string.IsNullOrEmpty(Request.QueryString["count"]))
               {
                  var iCount = MemberProtect.Utility.ValidateInteger(Request.QueryString["count"]);
                  for (var i = 0; i < iCount; i++)
                  {
                     var sInvoiceNumber = Request.QueryString["invoice" + i];
                     var fAmount = MemberProtect.Utility.ValidateDecimal(Request.QueryString["amount" + i]);

                     var oQBInvoice = DataAccess.QBInvoices.SingleOrDefault(row => row.InvoiceNumber == sInvoiceNumber);
                     if (oQBInvoice == null) continue;

                     if (oQBInvoice.Amount != fAmount)
                     {
                        sResponse = "confirm";
                     }
                  }

                  if (sResponse != "confirm")
                  {
                     sResponse = "continue";
                  }
               }
            }
         }
         catch (Exception oException)
         {
            sResponse = "error: " + oException.Message;
         }

         Response.Write(sResponse);
         Response.End();
      }

      public override bool GetSSL()
      {
         return true;
      }

      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl> {AccessControl.Public};
      }
   }
}