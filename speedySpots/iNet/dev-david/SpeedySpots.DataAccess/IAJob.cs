namespace SpeedySpots.DataAccess
{
   using System;

   public partial class IAJob
   {
      public string JobIDForDisplay
      {
         get { return string.Format("{0}-{1}", IARequest.RequestIdForDisplay, Sequence); }
      }

      public bool WasJobPreviouslyCompleted()
      {
         return MPUserIDCompleted != Guid.Empty || CompletedDateTime != new DateTime(1950, 1, 1);
      }
   }
}