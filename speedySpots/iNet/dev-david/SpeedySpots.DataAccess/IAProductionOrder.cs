namespace SpeedySpots.DataAccess
{
   using System;

   public partial class IAProductionOrder
   {
      public string NotesForDisplay
      {
         get { return Notes.Replace(Environment.NewLine, "<br/>"); }
      }

      public bool WasProductionOrderPreviouslyCompleted()
      {
         return CreatedDateTime != CompletedDateTime;
      }

      public bool WasProductionOrderPreviouslyOnHold()
      {
         return CreatedDateTime != OnHoldDateTime;
      }
   }
}