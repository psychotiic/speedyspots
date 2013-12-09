using System;
using System.Linq;

namespace SpeedySpots.Business
{
   public enum RequestStatus
   {
      Submitted = 3,
      NeedsEstimate = 1,
      WaitingEstimateApproval = 2,
      InProduction = 5,
      Completed = 6,
      Canceled = 7,
      Processing = 4,
   }

   public enum JobStatus
   {
      Incomplete,
      CompleteNeedsProduction,
      Complete,
      ReCutRequested,
   }

   public enum ProductionOrderStatus
   {
      Incomplete,
      Complete,
   }

   public enum SpotStatus
   {
      OnHold,
      Unviewed,
      Viewed,
      Finished,
      NeedsFix,
   }

   public enum SpotFileTypes
   {
      Production,
      Talent,
   }

   public enum SpotFeeTypes
   {
      VoiceOver = 1,
      VoiceOverWithCharacterOrAccent = 2,
      GraphicTranslation = 3,
      VoiceOverTranslation = 4,
      Recut = 5,
      ListeningFee = 6,
      Demo = 7
   }

   public enum JobFileType
   {
      Music,
      SFX,
      ReferenceFile,
      SplitFiles,
      ConvertFileType,
      Other,
   }
}