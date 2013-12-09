using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using SpeedySpots.DataAccess;

namespace SpeedySpots.Services.Models.Attributes
{
    public class AuthorizeRequest : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase oHttpContext)
        {
            /*if(oHttpContext.Request.Form["ApplicationLoginID"] != null && oHttpContext.Request.Form["ApplicationKey"] != null)
            {
                Guid oApplicationLoginID = Guid.Empty;
                Guid.TryParse(oHttpContext.Request.Form["ApplicationLoginID"], out oApplicationLoginID);

                DataAccessDataContext oDataAccess = new DataAccessDataContext(ConfigurationManager.ConnectionStrings["MemberProtectConnectionString"].ConnectionString);
                IAApplication oIAApplication = oDataAccess.IAApplications.SingleOrDefault(row => row.ApplicationLoginID == oApplicationLoginID && row.ApplicationKey == oHttpContext.Request.Form["ApplicationKey"]);
                if(oIAApplication != null)
                {
                    oHttpContext.Items["IAApplicationID"] = oIAApplication.IAApplicationID;
                    return true;
                }
            }

            return false;*/
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            Dictionary<string, string> oResults = new Dictionary<string,string>();
            oResults.Add("error", "unauthorized");

            filterContext.Result = new ObjectResult<Dictionary<string, string>>(oResults);
        }
    }
}