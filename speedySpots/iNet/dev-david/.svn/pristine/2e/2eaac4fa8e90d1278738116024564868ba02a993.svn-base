namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.IO;
   using System.Linq;
   using System.Text;
   using Newtonsoft.Json;
   using Objects;

   public partial class ajax_customer_lookup : AjaxBasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         Response.Clear();

         var oSB = new StringBuilder();
         var oSW = new StringWriter(oSB);

         using (JsonWriter oWriter = new JsonTextWriter(oSW))
         {
            oWriter.Formatting = Formatting.Indented;

            oWriter.WriteStartObject();

            if (IsAuthenticated && ApplicationContext.IsStaff)
            {
               oWriter.WritePropertyName("results");
               oWriter.WriteStartArray();

               if (!string.IsNullOrEmpty(Request.QueryString["q"]))
               {
                  var sQuery = Request.QueryString["q"];
                  DataAccess.Log = new DebugTextWriter();

                  var oResults = DataAccess.fn_Producer_GetCustomerLookup();
                  oResults = oResults.Where(row => (row.FirstName + " " + row.LastName + " (" + row.Username + ")").Contains(sQuery))
                                     .Distinct().Take(30);

                  foreach (var oResult in oResults)
                  {
                     oWriter.WriteStartObject();
                     oWriter.WritePropertyName("id");
                     oWriter.WriteValue(oResult.MPUserID.ToString());
                     oWriter.WritePropertyName("name");
                     oWriter.WriteValue(string.Format("{0} {1} ({2})", oResult.FirstName, oResult.LastName, oResult.Username));
                     oWriter.WriteEndObject();
                  }

                  oWriter.WriteEnd();
               }
            }

            oWriter.WriteEndObject();
         }

         Response.ContentType = "application/json";
         Response.Write(oSB.ToString());
         Response.End();
      }

      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl> {AccessControl.Staff, AccessControl.Admin};
      }
   }

   internal class DebugTextWriter : TextWriter
   {
      public override void Write(char[] buffer, int index, int count)
      {
         Debug.Write(new String(buffer, index, count));
      }

      public override void Write(string value)
      {
         Debug.Write(value);
      }

      public override Encoding Encoding
      {
         get { return Encoding.Default; }
      }
   }
}