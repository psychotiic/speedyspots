using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeedySpots.Controls.Tabs
{
    public partial class Talent_Tabs : SiteBaseControl
    {

        public enum TabOption
        {
            Pending,
            ToQC,
            Completed,
            Messages
        }

        public TabOption SelectedTab { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetActiveTab();
            }
        }

        private void SetActiveTab()
        {
            switch (this.SelectedTab)
            {
                case TabOption.Pending:
                    liPending.Attributes.Add("class", "at");
                    break;
                case TabOption.ToQC:
                    liToQC.Attributes.Add("class", "at");
                    break;
                case TabOption.Completed:
                    liCompleted.Attributes.Add("class", "at");
                    break;
                case TabOption.Messages:
                    liMessages.Attributes.Add("class", "at");
                    break;
                default:
                    break;
            
            }
        }
    }
}