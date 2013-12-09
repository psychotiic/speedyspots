namespace SpeedySpots.DataAccess
{
   using System;

   public partial class IARequest
   {
      public void CreateNote(Guid oMPUserID, string sNote)
      {
         var oIARequestNote = new IARequestNote
         {
            IARequestID = IARequestID,
            MPUserID = oMPUserID,
            Note = sNote,
            CreatedDateTime = DateTime.Now
         };

         IARequestNotes.Add(oIARequestNote);
      }

      public string RequestIdForDisplay
      {
         get { return string.Format(RequestIdFormatString, IARequestID); }
      }

      public string ProductionNotesForDisplay
      {
         get { return ProductionNotes.Replace(Environment.NewLine, "<br/>"); }
      }

      public string ScriptForDisplay
      {
         get { return Script.Replace("\n", "<br/>"); }
      }

      public static string RequestIdFormatString
      {
         get { return "{0:0}"; }
      }
   }
}