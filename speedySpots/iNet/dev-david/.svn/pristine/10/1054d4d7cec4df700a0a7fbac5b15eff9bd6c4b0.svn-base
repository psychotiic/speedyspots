using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class RequestsService
    {
        public static List<RequestNote> GetNotesForRequest(int requestID, DataAccessDataContext context)
        {
            var query = from n in context.IARequestNotes
                        where n.IARequestID == requestID
                        join u in context.MPUserDatas on n.MPUserID equals u.MPUserID
                        orderby n.CreatedDateTime
                        select new RequestNote
                        {
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Note = n.Note,
                            CreatedDateTime = n.CreatedDateTime
                        };

            return query.ToList();
        }

        public static IList<ss_Requests_DashboardResult> GetRequestDashboardForStaffUser(StaffRequestDashboardQuery query, DataAccessDataContext context)
        {
            DateTime? requestDate = null;

            if (query.RequestDate == DateTime.MinValue)
            {
                requestDate = null;
            }
            else
            {
                requestDate = query.RequestDate;
            }

            string labelsForQuery = string.Empty;


            IList<ss_Requests_DashboardResult> results = context.ss_Requests_Dashboard(query.RequestNumber,
                                                                                        requestDate, 
                                                                                        query.RequestedBy, 
                                                                                        query.LanugagesAsStringList, 
                                                                                        query.StatusAsStringList, 
                                                                                        query.RequestASAP,
                                                                                        query.GetLabelsForQuery(),
                                                                                        query.LabelsShowOnlyUnlabeled,
                                                                                        query.OrderBy_ForQuery, 
                                                                                        query.PageSize, 
                                                                                        query.PageNumber).ToList();

            return results;
        }

        public static List<StatusOption> GetStatusOption(DataAccessDataContext context)
        {
            return StatusService.GetStatuses(context)[StatusType.RequestStatus];
        }

        public static void UpdateNotifyEMails(int requestID, string notifyAddresses, DataAccessDataContext context)
        {
            context.ExecuteCommand("UPDATE IARequest SET NotificationEmails={0} WHERE IARequestID={1}", notifyAddresses, requestID);
            CreateRequestNote(requestID, "Notify contacts changed", context);
        }

        public static void CreateRequestNote(int requestID, string sNote, DataAccessDataContext context)
        {
            Guid userID = RequestContextHelper.MemberProtect.CurrentUser.UserID;
            context.ExecuteCommand("INSERT INTO IARequestNote VALUES ({0},{1},{2},GETDATE())", requestID, userID, sNote);
        }

    }
}