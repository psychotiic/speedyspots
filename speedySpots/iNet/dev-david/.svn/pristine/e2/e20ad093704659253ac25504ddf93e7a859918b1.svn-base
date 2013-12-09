using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using System.Text;

namespace SpeedySpots
{
    public partial class producer_talent_availability_rss : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder oSB = new StringBuilder();

            Response.Clear();
            Response.ContentType = "application/rss+xml";

            oSB.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            oSB.AppendLine("<rss version=\"2.0\">");

            oSB.AppendLine("<channel>");
            oSB.AppendLine("<title>Talent Unavailability</title>");
            oSB.AppendLine("<description></description>");
            oSB.AppendLine(string.Format("<link>{0}</link>", ApplicationContext.GetRootUrl(this, "producer-talent-availability-rss.aspx")));
            oSB.AppendLine(string.Format("<lastBuildDate>{0:r}</lastBuildDate>", DateTime.Now));

            bool bIsPublished = false;
            IQueryable<fn_Producer_GetAllTalentUnavailabilityResult> oResults = DataAccess.fn_Producer_GetAllTalentUnavailability().OrderByDescending(row => row.CreatedDateTime).Where(row => row.Status == "Pending");
            if(oResults.Count() > 0)
            {
                foreach(fn_Producer_GetAllTalentUnavailabilityResult oResult in oResults)
                {
                    if(!bIsPublished)
                    {
                        oSB.AppendLine(string.Format("<pubDate>{0:r}</pubDate>", oResult.CreatedDateTime));
                        bIsPublished = true;
                    }

                    oSB.AppendLine("<item>");
                    oSB.AppendLine(string.Format("<title>{0} - {1:d}</title>", oResult.Talent, oResult.FromDateTime));
                    oSB.AppendLine(string.Format("<guid>{0}?id={1}</guid>", ApplicationContext.GetRootUrl(this, "producer-talent-availability.aspx"), oResult.IATalentUnavailabilityID));
                    oSB.AppendLine(string.Format("<description>Talent will be unavailable from {0:d} and good for {1:d} delivery when they return.</description>", oResult.FromDateTime, oResult.ToDateTime));
                    oSB.AppendLine(string.Format("<link>{0}</link>", ApplicationContext.GetRootUrl(this, "producer-talent-availability.aspx")));
                    oSB.AppendLine(string.Format("<pubDate>{0:ddd, dd, MMM yyyy HH:mm:ss} EST</pubDate>", oResult.CreatedDateTime));
                    oSB.AppendLine("</item>");
                }
            }

            oSB.AppendLine("</channel>");
            oSB.AppendLine("</rss>");

            Response.Write(oSB.ToString());
            Response.End();
        }

        #region Overridden Members
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Public);

            return oAccessControl;
        }
        #endregion
    }
}