using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Elmah;

namespace SpeedySpots
{
    public partial class download : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] != null && Request.QueryString["type"] != null)
            {
                try
                {
                    int iFileID = MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]);
                    string sType = Request.QueryString["type"].ToUpper();

                    string sPath = string.Empty;
                    string sFilenameUnique = string.Empty;

                    if(sType == "REQUEST")
                    {
                        IARequestFile oIARequestFile = DataAccess.IARequestFiles.SingleOrDefault(row => row.IARequestFileID == iFileID);
                        if(oIARequestFile != null)
                        {
                            if(MemberProtect.CurrentUser.UserID == oIARequestFile.IARequest.MPUserID || MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem("IsStaff")))
                            {
                                sFilenameUnique = oIARequestFile.FilenameUnique;
                                string sFileAndPath = string.Format("{0}{1}", ApplicationContext.UploadPath, sFilenameUnique);

                                Response.ContentType = "application/octet-stream";
                                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"; size={1}", oIARequestFile.Filename, oIARequestFile.FileSize));
                                Response.TransmitFile(sFileAndPath);
                            }
                            else
                            {
                                Response.Write("Error.");
                            }
                        }
                        else
                        {
                            Response.Write("Error.");
                        }
                    }
                    else if(sType == "REQUESTPRODUCTION")
                    {
                        IARequestProductionFile oIARequestProductionFile = DataAccess.IARequestProductionFiles.SingleOrDefault(row => row.IARequestProductionFileID == iFileID);
                        if(oIARequestProductionFile != null)
                        {
                            if(MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem("IsCustomer")) || MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem("IsStaff")))
                            {
                                sFilenameUnique = oIARequestProductionFile.FilenameUnique;
                                string sFileAndPath = string.Format("{0}{1}", ApplicationContext.UploadPath, sFilenameUnique);

                                Response.ContentType = "application/octet-stream";
                                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"; size={1}", oIARequestProductionFile.Filename, oIARequestProductionFile.FileSize));
                                Response.TransmitFile(sFileAndPath);
                            }
                            else
                            {
                                Response.Write("Error.");
                            }
                        }
                        else
                        {
                            Response.Write("Error.");
                        }
                    }
                    else if(sType == "SPOT")
                    {
                        IASpotFile oIASpotFile = DataAccess.IASpotFiles.SingleOrDefault(row => row.IASpotFileID == iFileID);
                        if(oIASpotFile != null)
                        {
                            if(MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem("IsStaff")) || MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem("IsTalent")))
                            {
                                sFilenameUnique = oIASpotFile.FilenameUnique;
                                string sFileAndPath = string.Format("{0}{1}", ApplicationContext.UploadPath, sFilenameUnique);

                                Response.ContentType = "application/octet-stream";
                                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"; size={1}", oIASpotFile.Filename, oIASpotFile.FileSize));
                                Response.TransmitFile(sFileAndPath);
                            }
                            else
                            {
                                Response.Write("Error.");
                            }
                        }
                        else
                        {
                            Response.Write("Error.");
                        }
                    }
                    else if(sType == "JOB")
                    {
                        IAJobFile oIAJobFile = DataAccess.IAJobFiles.SingleOrDefault(row => row.IAJobFileID == iFileID);
                        if(oIAJobFile != null)
                        {
                            if(MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem("IsStaff")) || MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem("IsTalent")))
                            {
                                sFilenameUnique = oIAJobFile.FilenameUnique;
                                string sFileAndPath = string.Format("{0}{1}", ApplicationContext.UploadPath, sFilenameUnique);

                                Response.ContentType = "application/octet-stream";
                                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"; size={1}", oIAJobFile.Filename, oIAJobFile.FileSize));
                                Response.TransmitFile(sFileAndPath);
                            }
                            else
                            {
                                Response.Write("Error.");
                            }
                        }
                        else
                        {
                            Response.Write("Error.");
                        }
                    }
                    else if(sType == "DELIVERY")
                    {
                        // These are 'public' files but we obscure them by requiring the 'request production file id' along with the file id to keep people from guessing anything.
                        int iIARequestProductionID = MemberProtect.Utility.ValidateInteger(Request.QueryString["pid"]);

                        IARequestProductionFile oIARequestProductionFile = DataAccess.IARequestProductionFiles.SingleOrDefault(row => row.IARequestProductionFileID == iFileID);
                        if(oIARequestProductionFile != null)
                        {
                            // At a minimum the spot file passed in must be associated with the file being downloaded
                            if(oIARequestProductionFile.IARequestProductionID == iIARequestProductionID)
                            {
                                sFilenameUnique = oIARequestProductionFile.FilenameUnique;
                                string sFileAndPath = string.Format("{0}{1}", ApplicationContext.UploadPath, sFilenameUnique);

                                Response.ContentType = "application/octet-stream";
                                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"; size={1}", oIARequestProductionFile.Filename, oIARequestProductionFile.FileSize));
                                Response.TransmitFile(sFileAndPath);
                            }
                            else
                            {
                                Response.Write("Error - IDS don't match ");
                            }
                        }
                        else
                        {
                            Response.Write("Error - Null prod file");
                        }
                    }
                    else if (sType == "MUSIC")
                    {
                        if (MemberProtect.CurrentUser.IsAuthorized)
                        {
                            IAMusic music = Business.Services.MusicSamplesService.GetMusicSample(iFileID, DataAccess);
                            if (music != null)
                            {
                                string fullPath = this.ResolveUrl(string.Format("~/Music/{0}/{1}", music.Path, music.Filename));
                                Response.ContentType = "application/octet-stream";
                                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", music.FileNameDisplay));
                                Response.TransmitFile(fullPath);
                            }
                            else
                            {
                                Response.Clear();
                                Page.Response.StatusCode = 404;
                                Response.End();
                            }
                        }
                        else
                        {
                            Response.Clear();
                            Page.Response.StatusCode = 404;
                            Response.End();
                        }
                    }
                }
                catch(Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    Response.Write("Error - " + ex.Message);
                }
                finally
                {
                    Response.End();
                }
            }
            else
            {
                Response.Write("Error Type & ID don't match");
            }
        }

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Public);

            return oAccessControl;
        }
        #endregion
    }
}