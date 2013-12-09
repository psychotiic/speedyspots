namespace SpeedySpots.DataAccess
{
   using System;

   public partial class IASpot
   {
      public string ProductionNotesForDisplay
      {
         get { return ProductionNotes.Replace(Environment.NewLine, "<br/>"); }
      }

      public bool WasSpotPreviouslyCompleted()
      {
         // If the created and completed date/times are more than 30 seconds different
         // then we'll concider that as "previously completed"
         var span = CompletedDateTime - CreatedDateTime;
         return span.TotalSeconds > 30;
      }
   }
}