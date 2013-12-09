using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.InetActive.Objects;
using InetSolution.Web.InetActive;

namespace SpeedySpots.InetActive
{
    public partial class Default : InetActiveBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            m_divNews.InnerHtml += new Rss().GetRecentArticles("http://feeds.feedburner.com/TurnLeftBlog", 5);
        }
    }
}