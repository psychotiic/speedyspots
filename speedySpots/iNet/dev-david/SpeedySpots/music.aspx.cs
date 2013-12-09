using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using Telerik.Web.UI;
using SpeedySpots.DataAccess;

namespace SpeedySpots
{
    public partial class music : SiteBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.Form["rate"]))
                {
                    LoadGenreFilterOptions();
                    LoadLengthFilterOptions();
                    LoadTempoFilterOptions();
                }
                else
                {
                    SetRating();
                }
            }
        }

        protected void LoadGenreFilterOptions()
        {
            ddlGenre.DataValueField = "IAMusicGroupID";
            ddlGenre.DataTextField = "Name";
            ddlGenre.DataSource = DataAccess.IAMusicGroups.OrderBy(row => row.Name);
            ddlGenre.DataBind();
            ddlGenre.Items.Insert(0, new ListItem("",""));
        }

        protected void LoadLengthFilterOptions()
        {
           ddlLength.Items.Insert(0, new ListItem("", ""));
           ddlLength.Items.Insert(1, new ListItem(":30 or less", "30"));
           ddlLength.Items.Insert(2, new ListItem(":60", "60"));
        }

        protected void LoadTempoFilterOptions()
        {
            ddlTempo.DataValueField = "IAMusicTempoID";
            ddlTempo.DataTextField = "Display";
            ddlTempo.DataSource = DataAccess.IAMusicTempos.OrderBy(row => row.IAMusicTempoID);
            ddlTempo.DataBind();
            ddlTempo.Items.Insert(0, new ListItem("", ""));
        }

        protected void OnNeedDataSourceRequests(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var oResults = (from m in DataAccess.IAMusics
                            join r in DataAccess.IAMusicUserRatings
                                on m.IAMusicID equals r.IAMusicID into mr
                            from x in mr.Where(r => r.MPUserID == MemberProtect.CurrentUser.UserID).DefaultIfEmpty()
                            select new
                            {
                                IAMusicID = m.IAMusicID,
                                FileNameDisplay = m.FileNameDisplay,
                                Filename = m.Filename,
                                IAMusicGroupID = m.IAMusicGroupID,
                                IAMusicGroupName = m.IAMusicGroup.Name,
                                IAMusicTempoID = m.IAMusicTempoID,
                                IAMusicTempoName = m.IAMusicTempo.Display,
                                LengthSecs = m.LengthSecs,
                                LengthBeds = m.LengthBeds,
                                Path = m.Path,
                                Rating = (x.Rating == null) ? 0 : x.Rating
                            });

            if (!string.IsNullOrEmpty(ddlGenre.SelectedValue))
            {
                int resultID;
                if (int.TryParse(ddlGenre.SelectedValue, out resultID))
                {
                    oResults = oResults.Where(row => row.IAMusicGroupID == resultID);
                }
            }

            if (!string.IsNullOrEmpty(ddlLength.SelectedValue))
            {
                int resultID;
                if (int.TryParse(ddlLength.SelectedValue, out resultID))
                {
                    oResults = oResults.Where(row => row.LengthSecs == resultID);
                }
            }

            if (!string.IsNullOrEmpty(ddlTempo.SelectedValue))
            {
                int resultID;
                if (int.TryParse(ddlTempo.SelectedValue, out resultID))
                {
                    oResults = oResults.Where(row => row.IAMusicTempoID == resultID);
                }
            }

            if (!string.IsNullOrEmpty(ddlRating.SelectedValue))
            {
                int resultID = 0;
                if (int.TryParse(ddlRating.SelectedValue, out resultID) && resultID > 0)
                {
                    oResults = oResults.Where(row => row.Rating == resultID);
                }
            }

            m_grdMusic.DataSource = oResults;
        }

        protected void SetRating()
        {
            int musicID = 0;
            short rating = 0;
            short result = 0;
            string resultJson = string.Empty;

            string ratingValue = Request.Form["rate"].ToString();

            ParseRatingValuesFromUser(ratingValue, ref musicID, ref rating);

            if (musicID > 0)
            {
                result = SaveUserRating(musicID, rating);
            }

            resultJson = "{\"result\": " + result.ToString() + "}";

            Response.Clear();
            Response.ContentType = "application/json";
            Response.Write(resultJson);
            Response.End();
        }

        private void ParseRatingValuesFromUser(string ratingValue, ref int musicID, ref short rating)
        {
            if (ratingValue.Contains("_"))
            {
                string[] ratingsPair = ratingValue.Split('_');
                if (ratingsPair.Length == 2)
                {
                    int.TryParse(ratingsPair[0], out musicID);
                    short.TryParse(ratingsPair[1], out rating);
                }
            }
        }

        private short SaveUserRating(int musicID, short rating)
        {
            IAMusicUserRating userRating = DataAccess.IAMusicUserRatings.SingleOrDefault(r => r.MPUserID == MemberProtect.CurrentUser.UserID && r.IAMusicID == musicID);

            if (rating > 0)
            {
                rating = (rating > (short)3) ? (short)3 : rating;
                if (userRating != null)
                {
                    userRating.Rating = rating;
                }
                else
                {
                    userRating = new IAMusicUserRating();
                    userRating.MPUserID = MemberProtect.CurrentUser.UserID;
                    userRating.IAMusicID = musicID;
                    userRating.Rating = rating;
                    DataAccess.IAMusicUserRatings.InsertOnSubmit(userRating);
                }
            }
            else
            {
                if (userRating != null)
                {
                    DataAccess.IAMusicUserRatings.DeleteOnSubmit(userRating);
                }
            }

            try
            {
                DataAccess.SubmitChanges();
                return rating;
            }
            catch
            {
                return 0;
            }
        }

        protected void onItemDataBound(object source, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;
                var myMusic = oDataItem.DataItem;
                int usersRating = (int)DataBinder.Eval(e.Item.DataItem, "Rating");

                IAMusic music = new IAMusic
                {
                    IAMusicID = (int)DataBinder.Eval(e.Item.DataItem, "IAMusicID"),
                    Path = (string)DataBinder.Eval(e.Item.DataItem, "Path"),
                    Filename = (string)DataBinder.Eval(e.Item.DataItem, "Filename")
                };

                HyperLink hlSample = oDataItem.FindControl("hlSample") as HyperLink;
                HyperLink hlFile = oDataItem.FindControl("hlFile") as HyperLink;
                Literal litPlayer = oDataItem.FindControl("litPlayer") as Literal;
                DropDownList ddlRating = oDataItem.FindControl("ddlRating") as DropDownList;
                
                foreach (ListItem li in ddlRating.Items)
                {
                    if (usersRating.ToString() == li.Value)
                    {
                        li.Selected = true;
                    }

                    li.Value = music.IAMusicID.ToString() + "_" + li.Value;
                }
                string downloadPath = string.Empty;
                litPlayer.Text = BuildPlayerScript(music, out downloadPath);

                hlFile.Text = music.Filename;
                hlFile.NavigateUrl = string.Format("~/download/music/{0}", music.IAMusicID);
            }
        }

        private string BuildPlayerScript(IAMusic music, out string filePath)
        {
            string path = music.Path.StartsWith("\\") ? music.Path.Substring(1, music.Path.Length - 1) : music.Path;
            path = ResolveUrl(ApplicationContext.MusicPath + path + music.Filename);
            filePath = path;

            string playerID = "player" + music.IAMusicID.ToString();
            
            string divTag = "<div id=\"{0}\"></div>";
            string scriptTag = string.Format("<script type=\"text/javascript\">setSamplePlayer('{0}','{1}');</script>", path, playerID); 
            
            divTag = string.Format(divTag, playerID);

            return scriptTag + "\n" + divTag;
        }

        protected void OnFilterLibrary(object sender, EventArgs e)
        {
            m_grdMusic.CurrentPageIndex = 0;
            m_grdMusic.Rebind();
        }

        #region Overridden Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Customer);
            oAccessControl.Add(AccessControl.Staff);

            return oAccessControl;
        }
        #endregion
    }   
}