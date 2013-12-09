namespace SpeedySpots.Business.Services
{
   using System.Linq;
   using DataAccess;
   using Elmah;

   public class RequestStatusLookup
   {
      private readonly DataAccessDataContext _dataContext;

      public RequestStatusLookup(DataAccessDataContext dataContext)
      {
         _dataContext = dataContext;
      }

      public IARequestStatus GetRequestStatus(RequestStatus requestStatus)
      {
         IARequestStatus iaRequestStatus = null;
         switch (requestStatus)
         {
            case RequestStatus.Submitted:
               iaRequestStatus = _dataContext.IARequestStatus.SingleOrDefault(row => row.Name == "Submitted");
               break;
            case RequestStatus.NeedsEstimate:
               iaRequestStatus = _dataContext.IARequestStatus.SingleOrDefault(row => row.Name == "Needs Estimate");
               break;
            case RequestStatus.WaitingEstimateApproval:
               iaRequestStatus = _dataContext.IARequestStatus.SingleOrDefault(row => row.Name == "Waiting Estimate Approval");
               break;
            case RequestStatus.InProduction:
               iaRequestStatus = _dataContext.IARequestStatus.SingleOrDefault(row => row.Name == "In Production");
               break;
            case RequestStatus.Completed:
               iaRequestStatus = _dataContext.IARequestStatus.SingleOrDefault(row => row.Name == "Completed");
               break;
            case RequestStatus.Canceled:
               iaRequestStatus = _dataContext.IARequestStatus.SingleOrDefault(row => row.Name == "Canceled");
               break;
            case RequestStatus.Processing:
               iaRequestStatus = _dataContext.IARequestStatus.SingleOrDefault(row => row.Name == "Processing");
               break;
            default:
               throw new ApplicationException(string.Format("Request status is undefined or doesn't exist: {0}", requestStatus.ToString()));
         }

         return iaRequestStatus;
      }
   }
}