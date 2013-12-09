namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Web.UI.WebControls;
   using Business;
   using DataAccess;
   using InetSolution.Web;
   using Objects;
   using Telerik.Web.UI;

   public partial class view_notes_script : SiteBasePage
   {
      private IASpot m_oIASpot;
      private string fileFolder = string.Empty;

      protected void Page_Load(object sender, EventArgs e)
      {
         if (Request.QueryString["id"] != null)
         {
            m_oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]));
         }

         if (m_oIASpot == null)
         {
            Response.Redirect("~/Default.aspx");
         }

         if (!IsPostBack)
         {
            if (m_oIASpot != null)
            {
               m_oTalentFees.DataSource = m_oIASpot.IASpotFees;
               m_oTalentFees.DataBind();

               IList<IASpotFile> spotFiles =
                  m_oIASpot.IASpotFiles.Where(row => row.IASpotFileTypeID == ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Production)).ToList();
               if (spotFiles.Count > 0)
               {
                  rptProductionFiles.DataSource = spotFiles;
                  rptProductionFiles.DataBind();
               }
               else
               {
                  divReferenceFiles.Visible = false;
               }
            }
         }
      }

      protected void OnSave(object sender, EventArgs e)
      {
         foreach (RepeaterItem oItem in m_oTalentFees.Items)
         {
            var txtSpotFee = (RadNumericTextBox) oItem.FindControl("m_txtTalentFee");
            var txtSpotLength = (TextBox) oItem.FindControl("m_txtActualTime");

            var oIASpotFee = DataAccess.IASpotFees.First(row => row.IASpotFeeID == MemberProtect.Utility.ValidateInteger(txtSpotFee.Attributes["FeeID"]));
            if (oIASpotFee != null)
            {
               oIASpotFee.Fee = MemberProtect.Utility.ValidateDecimal(txtSpotFee.Text);
               if (txtSpotLength != null)
               {
                  oIASpotFee.LengthActual = txtSpotLength.Text;
               }
            }

            DataAccess.SubmitChanges();
         }

         ShowMessage("Actual time and fees were saved.", MessageTone.Positive);
      }

      protected void m_oTalentFees_ItemDataBound(object sender, RepeaterItemEventArgs e)
      {
         if (e.Item == null)
         {
            return;
         }
         var spotFee = e.Item.DataItem as IASpotFee;
         if (spotFee == null)
         {
            return;
         }
         var txtSpotLength = e.Item.FindControl("m_txtActualTime") as TextBox;

         if (spotFee.IASpotFeeTypeID == (int) SpotFeeTypes.ListeningFee)
         {
            txtSpotLength.Visible = false;
         }
         else
         {
            txtSpotLength.Text = spotFee.LengthActual;
            txtSpotLength.Attributes.Add("placeholder", "mm:ss");
         }
      }

      protected void rptProductionFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
      {
         if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
         {
            var litPlayer = (Literal) e.Item.FindControl("litPlayer");
            var spotFile = (IASpotFile) e.Item.DataItem;

            litPlayer.Text = BuildPlayerScript(spotFile);
         }
      }

      private string BuildPlayerScript(IASpotFile spotFile)
      {
         if (spotFile.FilenameUnique.ToLower().EndsWith("mp3"))
         {
            var path = ResolveUrl(string.Format("~/spotfile/{0}", spotFile.IASpotFileID));
            var playerID = string.Format("player{0}", spotFile.IASpotFileID);

            var divTag = "<div id=\"{0}\"></div>";
            var scriptTag = string.Format("<script type=\"text/javascript\">setSamplePlayer('{0}','{1}');</script>", path, playerID);

            divTag = string.Format(divTag, playerID);

            return string.Format("{0}\n{1}", scriptTag, divTag);
         }

         return string.Empty;
      }

      #region Public Properties

      public IASpot IASpot
      {
         get { return m_oIASpot; }
      }

      #endregion

      #region Virtual Methods

      public override List<AccessControl> GetAccessControl()
      {
         var oAccessControl = new List<AccessControl>();

         oAccessControl.Add(AccessControl.Admin);
         oAccessControl.Add(AccessControl.Staff);

         return oAccessControl;
      }

      #endregion
   }
}