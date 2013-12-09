namespace SpeedySpots.Objects
{
   using System;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Text.RegularExpressions;
   using System.Web;
   using System.Web.Caching;
   using System.Web.UI;
   using System.Web.UI.WebControls;
   using Business;
   using Business.Helpers;
   using Business.Services;
   using DataAccess;
   using InetSolution.MemberProtect;
   using Telerik.Web.UI;
   using Email = Business.Helpers.Email;

   public class ApplicationContext : IDisposable
   {
      private readonly HttpContext m_oHttpContext;
      private MemberProtect m_oMemberProtect;
      private SitePropertiesService m_siteProperties;

      // Separators
      private readonly string m_sSeparator = char.ConvertFromUtf32(200);
      private readonly string m_sSubSeparator = char.ConvertFromUtf32(201);

      public ApplicationContext(HttpContext oHttpContext)
      {
         m_oHttpContext = oHttpContext;

         var oSettings = new MemberProtectSettings(Settings.ConnectionString, m_oHttpContext.Session.SessionID, m_oHttpContext.Request.UserAgent,
                                                   m_oHttpContext.Request.UserHostAddress);
         m_oMemberProtect = new MemberProtect(oSettings);

         RequestContextHelper.Init(m_oMemberProtect);
      }

      public void Dispose()
      {
         if (m_oMemberProtect != null)
         {
            m_oMemberProtect.Dispose();
            m_oMemberProtect = null;
         }
      }

      #region Public Methods

      public Guid GetOrganizationID()
      {
         return MemberProtect.Organization.GetID("InetSolution, Inc.");
      }

      public Guid GetOrgID()
      {
         return GetOrgID(MemberProtect.CurrentUser.UserID);
      }

      public Guid GetOrgID(Guid oMPUserID)
      {
         var oMPOrgID = Guid.Empty;
         var sCacheItemKey = "OrgID-" + oMPUserID.ToString();

         if (HttpContext.Current.Cache[sCacheItemKey] == null)
         {
            var oMPOrgUser = DataAccess.MPOrgUsers.FirstOrDefault(row => row.MPUserID == oMPUserID);
            if (oMPOrgUser != null)
            {
               oMPOrgID = oMPOrgUser.MPOrgID;
               HttpContext.Current.Cache.Add(sCacheItemKey, oMPOrgID, null, DateTime.Now.AddSeconds(120), TimeSpan.Zero, CacheItemPriority.High, null);
            }
         }
         else
         {
            oMPOrgID = (Guid) HttpContext.Current.Cache[sCacheItemKey];
         }

         return oMPOrgID;
      }

      public bool UpdateUserOrgID(Guid oMPUserID, Guid oMPOrgID)
      {
         var sCacheItemKey = "OrgID-" + oMPUserID.ToString();

         if (HttpContext.Current.Cache[sCacheItemKey] != null)
         {
            HttpContext.Current.Cache[sCacheItemKey] = oMPOrgID;
            return true;
         }

         return false;
      }

      public int GetCompanyID(Guid oMPOrgID)
      {
         var oMPOrg = DataAccess.MPOrgs.SingleOrDefault(row => row.MPOrgID == oMPOrgID);
         return oMPOrg != null ? oMPOrg.SMSID : 0;
      }

      public int GetGridPageSize()
      {
         var iGridPageSize = MemberProtect.Utility.ValidateInteger(MemberProtect.CurrentUser.GetDataItem("GridPageSize"));
         if (iGridPageSize <= 0)
         {
            iGridPageSize = 10;
         }

         return iGridPageSize;
      }

      public string GetRootUrl(Page oPage, string sPage)
      {
         sPage = string.Format("~/{0}", sPage);

         if (m_oHttpContext.Request.Url.IsDefaultPort)
         {
            return string.Format("{0}://{1}{2}", m_oHttpContext.Request.Url.Scheme, m_oHttpContext.Request.Url.Host, oPage.ResolveUrl(sPage));
         }
         
         return string.Format("{0}://{1}:{2}{3}", m_oHttpContext.Request.Url.Scheme, m_oHttpContext.Request.Url.Host, m_oHttpContext.Request.Url.Port,
                              oPage.ResolveUrl(sPage));
      }

      public Func<string, string> RootUrlBuilder(Page page)
      {
         return s => GetRootUrl(page, s);
      }

      public string CleanAddressList(string emailList)
      {
         return Email.CleanAddressList(emailList);
      }

      public bool CanCurrentUserViewOrder(IARequest m_oIARequest)
      {
         // Customer must be the originator of the request to view it
         if (IsCustomer && m_oIARequest.MPUserID == MemberProtect.CurrentUser.UserID)
         {
            return true;
         }
         if (IsCustomer && m_oIARequest.MPUserID != MemberProtect.CurrentUser.UserID)
         {
            // See if this is a request from an assigned coworker
            var query = from c in DataAccess.IACustomerCoworkers
                        where c.MPUserID == MemberProtect.CurrentUser.UserID && c.MPUserIDCoworker == m_oIARequest.MPUserID
                        select c;

            return (query.Any());
         }

         return false;
      }

      public int GetRequestStatusID(RequestStatus oRequestStatus)
      {
         var iRequestStatusID = 0;
         var sCacheItemKey = "RequestStatus-" + oRequestStatus.ToString();

         if (HttpContext.Current.Cache[sCacheItemKey] == null)
         {
            var requestStatusLookup = new RequestStatusLookup(DataAccess);
            iRequestStatusID = requestStatusLookup.GetRequestStatus(oRequestStatus).IARequestStatusID;
            HttpContext.Current.Cache.Add(sCacheItemKey, iRequestStatusID, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero, CacheItemPriority.Normal, null);
         }
         else
         {
            iRequestStatusID = (int) HttpContext.Current.Cache[sCacheItemKey];
         }

         return iRequestStatusID;
      }

      public int GetJobStatusID(JobStatus oJobStatus)
      {
         var iJobStatusID = 0;
         var sCacheItemKey = "JobStatus-" + oJobStatus.ToString();

         if (HttpContext.Current.Cache[sCacheItemKey] == null)
         {
            iJobStatusID = GetJobStatusFromDB(oJobStatus).IAJobStatusID;
            HttpContext.Current.Cache.Add(sCacheItemKey, iJobStatusID, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero, CacheItemPriority.Normal, null);
         }
         else
         {
            iJobStatusID = (int) HttpContext.Current.Cache[sCacheItemKey];
         }

         return iJobStatusID;
      }

      public int GetProductionOrderStatusID(ProductionOrderStatus oProductionOrderStatus)
      {
         var iProductionOrderStatusID = 0;
         var sCacheItemKey = "ProductionOrderStatus-" + oProductionOrderStatus.ToString();

         if (HttpContext.Current.Cache[sCacheItemKey] == null)
         {
            iProductionOrderStatusID = GetProductionOrderStatusFromDB(oProductionOrderStatus).IAProductionOrderStatusID;
            HttpContext.Current.Cache.Add(sCacheItemKey, iProductionOrderStatusID, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero, CacheItemPriority.Normal, null);
         }
         else
         {
            iProductionOrderStatusID = (int) HttpContext.Current.Cache[sCacheItemKey];
         }

         return iProductionOrderStatusID;
      }

      public int GetSpotStatusID(SpotStatus oSpotStatus)
      {
         var iSpotStatusID = 0;
         var sCacheItemKey = "SpotStatus-" + oSpotStatus.ToString();

         if (HttpContext.Current.Cache[sCacheItemKey] == null)
         {
            iSpotStatusID = GetSpotStatusFromDB(oSpotStatus).IASpotStatusID;
            HttpContext.Current.Cache.Add(sCacheItemKey, iSpotStatusID, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero, CacheItemPriority.Normal, null);
         }
         else
         {
            iSpotStatusID = (int) HttpContext.Current.Cache[sCacheItemKey];
         }

         return iSpotStatusID;
      }

      public int GetSpotFileTypeID(SpotFileTypes oSpotFileType)
      {
         var iSpotFileTypeID = 0;
         var sCacheItemKey = "SpotFileType-" + oSpotFileType.ToString();

         if (HttpContext.Current.Cache[sCacheItemKey] == null)
         {
            iSpotFileTypeID = GetSpotFileTypeFromDB(oSpotFileType).IASpotFileTypeID;
            HttpContext.Current.Cache.Add(sCacheItemKey, iSpotFileTypeID, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero, CacheItemPriority.Normal, null);
         }
         else
         {
            iSpotFileTypeID = (int) HttpContext.Current.Cache[sCacheItemKey];
         }

         return iSpotFileTypeID;
      }

      /// <summary>
      /// Cancel buttons should be shown when:
      /// 1. (Request status is 'Submitted' OR 'Needs Estimate' OR 'WaitingEstimateApproval') AND there are no jobs on the request
      /// 
      /// Cancel button should NOT be shown when:
      /// 2. Has an Estimate that requires payment and there are already jobs on the request
      /// </summary>
      /// <param name="oIARequest"></param>
      /// <returns></returns>
      public bool CanRequestBeCanceled(IARequest oIARequest)
      {
         var bReturn = (oIARequest.IARequestStatusID == GetRequestStatusID(RequestStatus.Submitted) ||
                         oIARequest.IARequestStatusID == GetRequestStatusID(RequestStatus.NeedsEstimate) ||
                         oIARequest.IARequestStatusID == GetRequestStatusID(RequestStatus.WaitingEstimateApproval)) && oIARequest.IAJobs.Count == 0;

         // 1. (Request status is 'Submitted' OR 'Needs Estimate' OR 'WaitingEstimateApproval') AND there are no jobs on the request

         // 2. Has an Estimate that requires payment and there are already jobs on the request
         if (oIARequest.IARequestEstimates.Any())
         {
            if (oIARequest.IARequestEstimates[0].IsPaymentRequired && oIARequest.IAJobs.Any())
            {
               bReturn = false;
            }
         }

         return bReturn;
      }

      public void LoadLanguages(ref RadComboBox cboLanguages)
      {
         cboLanguages.Items.Clear();
         cboLanguages.Items.Add(new RadComboBoxItem("English", "English"));
         cboLanguages.Items.Add(new RadComboBoxItem("Spanish", "Spanish"));
      }

      public void LoadLanguages(ref DropDownList cboLanguages)
      {
         cboLanguages.Items.Clear();
         cboLanguages.Items.Add(new ListItem("English", "English"));
         cboLanguages.Items.Add(new ListItem("Spanish", "Spanish"));
      }

      public void LoadCreditCardTypes(ref RadComboBox comboBox)
      {
         comboBox.Items.Clear();

         comboBox.Items.AddRange(CreditCards
                                    .LoadCreditCardTypes()
                                    .ConvertAll(kvp => new RadComboBoxItem(kvp.Value, kvp.Key)));
      }

      public void LoadCreditCardExpirationMonths(ref RadComboBox cboCreditCardExpirationMonths)
      {
         cboCreditCardExpirationMonths.Items.Clear();

         foreach (var month in CreditCards.LoadCreditCardExpirationMonths())
         {
            cboCreditCardExpirationMonths.Items.Add(new RadComboBoxItem(month.Value, month.Key));
         }
      }

      public void LoadCreditCardExpirationMonths(ref DropDownList cboCreditCardExpirationMonths)
      {
         cboCreditCardExpirationMonths.Items.Clear();

         foreach (var month in CreditCards.LoadCreditCardExpirationMonths())
         {
            cboCreditCardExpirationMonths.Items.Add(new ListItem(month.Value, month.Key));
         }
      }

      public void LoadCreditCardExpirationYears(ref RadComboBox cboCreditCardExpirationYears)
      {
         cboCreditCardExpirationYears.Items.Clear();

         foreach (var year in CreditCards.LoadCreditCardExpirationYears())
         {
            cboCreditCardExpirationYears.Items.Add(new RadComboBoxItem(year.Value.ToString(), year.Key.ToString()));
         }
      }

      public void LoadCreditCardExpirationYears(ref DropDownList cboCreditCardExpirationYears)
      {
         cboCreditCardExpirationYears.Items.Clear();

         foreach (var year in CreditCards.LoadCreditCardExpirationYears())
         {
            cboCreditCardExpirationYears.Items.Add(new ListItem(year.Value.ToString(), year.Key.ToString()));
         }
      }

      public void LoadCountries(ref DropDownList cboCountries)
      {
         cboCountries.Items.Clear();

         foreach (var country in Countries.GetCountries())
         {
            cboCountries.Items.Add(new ListItem(country.Value, country.Key));
         }
      }

      public void LoadCountries(ref RadComboBox cboCountries)
      {
         cboCountries.Items.Clear();

         foreach (var country in Countries.GetCountries())
         {
            cboCountries.Items.Add(new RadComboBoxItem(country.Value, country.Key));
         }
      }

      [Obsolete]
      public string GetCreditCardNumber()
      {
         var oIACustomerCreditCard = DataAccess.IACustomerCreditCards.SingleOrDefault(row => row.MPUserID == MemberProtect.CurrentUser.UserID);
         if (oIACustomerCreditCard != null)
         {
            if (MemberProtect.Cryptography.Decrypt(oIACustomerCreditCard.CreditCardNumber) != string.Empty)
            {
               return string.Format("(XXXX-XXXX-XXXX-{0})",
                                    MemberProtect.Utility.Right(MemberProtect.Cryptography.Decrypt(oIACustomerCreditCard.CreditCardNumber), 4));
            }
         }

         return string.Empty;
      }

      [Obsolete]
      public string FormatSecureCreditCardNumber(string sCreditCardNumber)
      {
         return string.Format("(XXXX-XXXX-XXXX-{0})", MemberProtect.Utility.Right(sCreditCardNumber, 4));
      }

      public fn_Message_GetNewResult GetNewMessage()
      {
         return
            DataAccess.fn_Message_GetNew(MemberProtect.CurrentUser.UserID)
                      .Where(row => DateTime.Now >= row.DisplayStartDateTime && DateTime.Now <= row.DisplayEndDateTime)
                      .OrderByDescending(row => row.CreatedDateTime)
                      .FirstOrDefault();
      }

      public string GetOutOfOfficeNotification()
      {
         var bDisplayMessage = false;

         if (SiteProperites.ClosedMessageDisplayAlways)
         {
            bDisplayMessage = true;
         }
         else
         {
            var oNow = new DateTime(1950, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);

            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
               if (oNow.CompareTo(SiteProperites.MondayInDateTime) < 0 || oNow.CompareTo(SiteProperites.MondayOutDateTime) > 0)
               {
                  bDisplayMessage = true;
               }
            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
            {
               if (oNow.CompareTo(SiteProperites.TuesdayInDateTime) < 0 || oNow.CompareTo(SiteProperites.TuesdayOutDateTime) > 0)
               {
                  bDisplayMessage = true;
               }
            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Wednesday)
            {
               if (oNow.CompareTo(SiteProperites.WednesdayInDateTime) < 0 || oNow.CompareTo(SiteProperites.WednesdayOutDateTime) > 0)
               {
                  bDisplayMessage = true;
               }
            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Thursday)
            {
               if (oNow.CompareTo(SiteProperites.ThursdayInDateTime) < 0 || oNow.CompareTo(SiteProperites.ThursdayOutDateTime) > 0)
               {
                  bDisplayMessage = true;
               }
            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
            {
               if (oNow.CompareTo(SiteProperites.FridayInDateTime) < 0 || oNow.CompareTo(SiteProperites.FridayOutDateTime) > 0)
               {
                  bDisplayMessage = true;
               }
            }
               // Sat/Sun
            else
            {
               bDisplayMessage = true;
            }
         }

         return bDisplayMessage 
            ? string.Format("<div class='message' style='margin-top: 18px;'>{0}</div>", SiteProperites.ClosedMessage) 
            : string.Empty;
      }

      #region Request Methods

      public void RequestAssignStaff(IARequest oIARequest, Guid oMPUserID)
      {
         // Only update the staff owner if there is currently no owner
         if (oIARequest.MPUserIDOwnedByStaff == Guid.Empty)
         {
            oIARequest.MPUserIDOwnedByStaff = oMPUserID;

            oIARequest.CreateNote(oMPUserID, "Production Owner");
         }
      }

      public bool DeleteRequest(int iIARequestID)
      {
         var oIARequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == iIARequestID);
         if (oIARequest != null)
         {
            // Delete any associated files
            foreach (var oIARequestFile in oIARequest.IARequestFiles)
            {
               DeleteFile(oIARequestFile.IARequestFileID, "REQUEST");
            }

            DataAccess.IARequests.DeleteOnSubmit(oIARequest);
            DataAccess.SubmitChanges();

            return true;
         }

         return false;
      }

      public string GetJobProducers(int iIAJobID)
      {
         var sProducers = string.Empty;

         var oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == iIAJobID);
         if (oIAJob != null)
         {
            if (oIAJob.MPUserIDCreated != Guid.Empty)
            {
               sProducers = MemberProtect.User.GetDataItem(oIAJob.MPUserIDCreated, "FirstName");

               var sLastName = MemberProtect.User.GetDataItem(oIAJob.MPUserIDCreated, "LastName");
               if (sLastName != string.Empty)
               {
                  sProducers += " " + MemberProtect.Utility.Left(sLastName, 1).ToUpper() + ".";
               }
            }

            if (oIAJob.MPUserIDCreated != oIAJob.MPUserIDProducer)
            {
               if (oIAJob.MPUserIDProducer != Guid.Empty)
               {
                  sProducers += ", " + MemberProtect.User.GetDataItem(oIAJob.MPUserIDProducer, "FirstName");

                  var sLastName = MemberProtect.User.GetDataItem(oIAJob.MPUserIDProducer, "LastName");
                  if (sLastName != string.Empty)
                  {
                     sProducers += " " + MemberProtect.Utility.Left(sLastName, 1) + ".";
                  }
               }
            }
         }

         return sProducers;
      }

      public string GetSpotFees(int iIASpotID)
      {
         var sFees = string.Empty;

         var oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == iIASpotID);
         if (oIASpot != null)
         {
            var i = 0;
            var oFees = new StringBuilder();
            foreach (var oIASpotFee in oIASpot.IASpotFees)
            {
               if (i > 0)
               {
                  oFees.Append("<label>&nbsp;</label>");
               }

               oFees.Append(string.Format("{0} - {1}<br/>", oIASpotFee.Fee.ToString("c"), oIASpotFee.IASpotFeeType.Name));

               i++;
            }

            sFees = oFees.ToString();
         }

         return sFees;
      }

      #endregion

      #region Grid Setting Methods

      public void SaveGridSettings(string sName, string sFilterString)
      {
         var oIAUserSettingsGrid = DataAccess.IAUserSettingsGrids.SingleOrDefault(row => row.MPUserID == MemberProtect.CurrentUser.UserID && row.Name == sName);
         if (oIAUserSettingsGrid == null)
         {
            oIAUserSettingsGrid = new IAUserSettingsGrid();
            DataAccess.IAUserSettingsGrids.InsertOnSubmit(oIAUserSettingsGrid);

            oIAUserSettingsGrid.MPUserID = MemberProtect.CurrentUser.UserID;
            oIAUserSettingsGrid.Name = sName;
            oIAUserSettingsGrid.SortExpression = string.Empty;
         }

         oIAUserSettingsGrid.Filters = sFilterString;

         DataAccess.SubmitChanges();
      }

      public void SaveGridSettings(string sName, GridSortCommandEventArgs e)
      {
         // We're only saving the sorting on the master table, not child 'detail' tables
         if (e.Item.OwnerTableView.Name != "Detail")
         {
            var oIAUserSettingsGrid = DataAccess.IAUserSettingsGrids.SingleOrDefault(row => row.MPUserID == MemberProtect.CurrentUser.UserID && row.Name == sName);
            if (oIAUserSettingsGrid == null)
            {
               oIAUserSettingsGrid = new IAUserSettingsGrid();
               DataAccess.IAUserSettingsGrids.InsertOnSubmit(oIAUserSettingsGrid);

               oIAUserSettingsGrid.MPUserID = MemberProtect.CurrentUser.UserID;
               oIAUserSettingsGrid.Name = sName;
               oIAUserSettingsGrid.Filters = string.Empty;
            }

            oIAUserSettingsGrid.SortExpression = e.SortExpression;
            if (e.NewSortOrder == GridSortOrder.Ascending)
            {
               oIAUserSettingsGrid.SortExpression += " ASC";
            }
            else if (e.NewSortOrder == GridSortOrder.Descending)
            {
               oIAUserSettingsGrid.SortExpression += " DESC";
            }

            DataAccess.SubmitChanges();
         }
      }

      public void LoadGridSettings(string sName, RadGrid oGrid, out string sFilterString)
      {
         sFilterString = string.Empty;

         // Load Sorting Expression
         var oIAUserSettingsGrid = DataAccess.IAUserSettingsGrids.SingleOrDefault(row => row.MPUserID == MemberProtect.CurrentUser.UserID && row.Name == sName);
         if (oIAUserSettingsGrid != null)
         {
            if (oIAUserSettingsGrid.SortExpression != string.Empty)
            {
               oGrid.MasterTableView.SortExpressions.AddSortExpression(oIAUserSettingsGrid.SortExpression);
            }

            // Load filter string
            sFilterString = oIAUserSettingsGrid.Filters;
         }

         // Load Paging Size
         oGrid.PageSize = GetGridPageSize();
      }

      #endregion

      public bool DeleteFile(int iFileID, string sType)
      {
         sType = sType.ToUpper();

         if (sType == "REQUEST")
         {
            var oIARequestFile = DataAccess.IARequestFiles.SingleOrDefault(row => row.IARequestFileID == iFileID);
            if (oIARequestFile != null)
            {
               // Delete physical file on disk first
               var sFilename = string.Format("{0}{1}", UploadPath, oIARequestFile.FilenameUnique);
               if (File.Exists(sFilename))
               {
                  File.Delete(sFilename);
               }

               return true;
            }
         }
         else if (sType == "SPOT")
         {
            var oIASpotFile = DataAccess.IASpotFiles.SingleOrDefault(row => row.IASpotFileID == iFileID);
            if (oIASpotFile != null)
            {
               // Delete physical file on disk first
               var sFilename = string.Format("{0}{1}", UploadPath, oIASpotFile.FilenameUnique);
               if (File.Exists(sFilename))
               {
                  File.Delete(sFilename);
               }

               return true;
            }
         }
         else if (sType == "JOB")
         {
            var oIAJobFile = DataAccess.IAJobFiles.SingleOrDefault(row => row.IAJobFileID == iFileID);
            if (oIAJobFile != null)
            {
               // Delete physical file on disk first
               var sFilename = string.Format("{0}{1}", UploadPath, oIAJobFile.FilenameUnique);
               if (File.Exists(sFilename))
               {
                  File.Delete(sFilename);
               }

               return true;
            }
         }

         return false;
      }

      public bool IsProductionOrderOnHold(int iIAProductionOrderID)
      {
         var oIAProductionOrder = DataAccess.IAProductionOrders.SingleOrDefault(row => row.IAProductionOrderID == iIAProductionOrderID);
         if (oIAProductionOrder != null)
         {
            if (oIAProductionOrder.IASpots.Count() > 0)
            {
               foreach (var oIASpot in oIAProductionOrder.IASpots)
               {
                  if (oIASpot.IASpotStatusID != GetSpotStatusID(SpotStatus.OnHold))
                  {
                     return false;
                  }
               }
            }

            // PO is considered On Hold if any spots are on hold or there are no spots associated with the PO
            return true;
         }
         else
         {
            throw new ApplicationException(string.Format("Production Order '{0}' could not be found.", iIAProductionOrderID));
         }
      }

      public string DisplayProducerLabels(int iIARequestID)
      {
         var sLabels = string.Empty;
         foreach (var oResults in DataAccess.fn_Producer_GetRequestLabels(iIARequestID))
         {
            sLabels += string.Format("<div class='label'><span>{0}</span><a href='javascript:OnRemoveLabel({1});' class='remove'>Remove Label</a></div>",
                                     oResults.Text, oResults.IALabelID);
         }

         if (sLabels.Length > 0)
         {
            sLabels = string.Format("<div class='label-bar group'><div id=\"loading\"><img src=\"img/loading3.gif\" alt=\"loading\" /></div>{0}</div>", sLabels);
         }

         return sLabels;
      }

      public void ArchiveUser(Guid oMPUserID)
      {
         MemberProtect.User.SetDataItem(oMPUserID, "IsArchived", MemberProtect.Utility.BoolToYesNo(true));
         MemberProtect.User.SetLockedStatus(MemberProtect.User.GetUsername(oMPUserID), true);
      }

      public bool CustomerHasCoworkersAssociated()
      {
         return CustomerHasCoworkersAssociated(MemberProtect.CurrentUser.UserID);
      }

      public bool CustomerHasCoworkersAssociated(Guid oMPUserID)
      {
         var customerHasCoworkersAssigned = false;
         var sCacheItemKey = "CustomerHasCoworkersAssociated-" + oMPUserID.ToString();

         if (HttpContext.Current.Cache[sCacheItemKey] == null)
         {
            var query = from c in DataAccess.IACustomerCoworkers
                        where c.MPUserID == oMPUserID
                        select c;

            customerHasCoworkersAssigned = (query.Any());
            HttpContext.Current.Cache.Add(sCacheItemKey, customerHasCoworkersAssigned, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero, CacheItemPriority.Normal,
                                          null);
         }
         else
         {
            customerHasCoworkersAssigned = (bool) HttpContext.Current.Cache[sCacheItemKey];
         }

         return customerHasCoworkersAssigned;
      }

      public void CustomerHasCoworkersAssociatedReset()
      {
         CustomerHasCoworkersAssociatedReset(MemberProtect.CurrentUser.UserID);
      }

      public void CustomerHasCoworkersAssociatedReset(Guid oMPUserID)
      {
         var sCacheItemKey = "CustomerHasCoworkersAssociated-" + oMPUserID.ToString();

         if (HttpContext.Current.Cache[sCacheItemKey] != null)
         {
            HttpContext.Current.Cache.Remove(sCacheItemKey);
         }
      }

      #endregion

      #region Feedback System

      public void SubmitFeedback(string sType, string sFeeling, string sMessage, string sUrl)
      {
         var oUri = new Uri(sUrl);

         var sFilename = string.Empty;
         if (oUri.Segments.Any())
         {
            sFilename = oUri.Segments[oUri.Segments.Length - 1];
         }

         var oIAFeedback = new IAFeedback();
         oIAFeedback.MPUserID = MemberProtect.CurrentUser.UserID;
         oIAFeedback.Type = sType;
         oIAFeedback.Feeling = sFeeling;
         oIAFeedback.Message = sMessage;
         oIAFeedback.Url = oUri.PathAndQuery;
         oIAFeedback.Filename = sFilename;
         oIAFeedback.CreatedDateTime = DateTime.Now;
         DataAccess.IAFeedbacks.InsertOnSubmit(oIAFeedback);
         DataAccess.SubmitChanges();

         // We send an email if the user has a question or a problem
         var sSubject = string.Empty;
         var oSB = new StringBuilder();

         var sUsername = "unknown@speedyspots.com";
         var sFirstName = "?";
         var sLastName = "?";
         if (MemberProtect.CurrentUser.IsAuthorized)
         {
            sUsername = MemberProtect.CurrentUser.Username;
            sFirstName = MemberProtect.CurrentUser.GetDataItem("FirstName");
            sLastName = MemberProtect.CurrentUser.GetDataItem("LastName");
         }

         if (sType == "Question" && !string.IsNullOrEmpty(SiteProperites.FeedbackEmailQuestion))
         {
            var sEmails = SiteProperites.FeedbackEmailQuestion.Split(',').ToList();
            sSubject = string.Format("Feedback Question from {0} {1}", sFirstName, sLastName);

            oSB = new StringBuilder();
            oSB.AppendLine(string.Format("{0} {1} has submitted a question at {2:g}:", sFirstName, sLastName, oIAFeedback.CreatedDateTime));
            oSB.AppendLine();
            oSB.AppendLine(sMessage);
            oSB.AppendLine();
            oSB.AppendLine("Feeling: " + sFeeling);

            EmailCommunicationService.UserFeedbackNoticeSend(oSB, sSubject, sUsername, sEmails);
         }

         if (sType == "Problem" && !string.IsNullOrEmpty(SiteProperites.FeedbackEmailProblem))
         {
            var sEmails = SiteProperites.FeedbackEmailProblem.Split(',').ToList();
            sSubject = string.Format("Feedback Problem from {0} {1}", sFirstName, sLastName);

            oSB = new StringBuilder();
            oSB.AppendLine(string.Format("{0} {1} has submitted a problem at {2:g}:", sFirstName, sLastName, oIAFeedback.CreatedDateTime));
            oSB.AppendLine();
            oSB.AppendLine(sMessage);
            oSB.AppendLine();
            oSB.AppendLine("Feeling: " + sFeeling);

            EmailCommunicationService.UserFeedbackNoticeSend(oSB, sSubject, sUsername, sEmails);
         }
      }

      #endregion

      #region Private Helper Methods

      /// <summary>
      /// Retreives the apporpirate JobStatus object from the DB based on the enum value passed in
      /// </summary>
      /// <param name="oJobStatus"></param>
      /// <returns></returns>
      private IAJobStatus GetJobStatusFromDB(JobStatus oJobStatus)
      {
         IAJobStatus oIAJobStatus = null;
         switch (oJobStatus)
         {
            case JobStatus.Incomplete:
               oIAJobStatus = DataAccess.IAJobStatus.SingleOrDefault(row => row.Name == "Incomplete");
               break;
            case JobStatus.CompleteNeedsProduction:
               oIAJobStatus = DataAccess.IAJobStatus.SingleOrDefault(row => row.Name == "Complete - Needs Production");
               break;
            case JobStatus.Complete:
               oIAJobStatus = DataAccess.IAJobStatus.SingleOrDefault(row => row.Name == "Complete");
               break;
            case JobStatus.ReCutRequested:
               oIAJobStatus = DataAccess.IAJobStatus.SingleOrDefault(row => row.Name == "Re-Cut Requested");
               break;
            default:
               throw new ApplicationException(string.Format("Job status is undefined or doesn't exist: {0}", oJobStatus.ToString()));
         }

         return oIAJobStatus;
      }

      /// <summary>
      /// Retreives the apporpirate ProductionOrderStatus object from the DB based on the enum value passed in
      /// </summary>
      /// <param name="oProductionOrderStatus"></param>
      /// <returns></returns>
      private IAProductionOrderStatus GetProductionOrderStatusFromDB(ProductionOrderStatus oProductionOrderStatus)
      {
         IAProductionOrderStatus oIAProductionOrderStatus = null;
         switch (oProductionOrderStatus)
         {
            case ProductionOrderStatus.Incomplete:
               oIAProductionOrderStatus = DataAccess.IAProductionOrderStatus.SingleOrDefault(row => row.Name == "Incomplete");
               break;
            case ProductionOrderStatus.Complete:
               oIAProductionOrderStatus = DataAccess.IAProductionOrderStatus.SingleOrDefault(row => row.Name == "Complete");
               break;
            default:
               throw new ApplicationException(string.Format("Production Order status is undefined or doesn't exist: {0}", oProductionOrderStatus.ToString()));
         }

         return oIAProductionOrderStatus;
      }

      /// <summary>
      /// Retreives the apporpirate SpotsStatus object from the DB based on the enum value passed in
      /// </summary>
      /// <param name="oSpotStatus"></param>
      /// <returns></returns>
      private IASpotStatus GetSpotStatusFromDB(SpotStatus oSpotStatus)
      {
         IASpotStatus oIASpotStatus = null;

         switch (oSpotStatus)
         {
            case SpotStatus.OnHold:
               oIASpotStatus = DataAccess.IASpotStatus.SingleOrDefault(row => row.Name == "On Hold");
               break;
            case SpotStatus.Unviewed:
               oIASpotStatus = DataAccess.IASpotStatus.SingleOrDefault(row => row.Name == "Unviewed");
               break;
            case SpotStatus.Viewed:
               oIASpotStatus = DataAccess.IASpotStatus.SingleOrDefault(row => row.Name == "Viewed");
               break;
            case SpotStatus.Finished:
               oIASpotStatus = DataAccess.IASpotStatus.SingleOrDefault(row => row.Name == "Finished");
               break;
            case SpotStatus.NeedsFix:
               oIASpotStatus = DataAccess.IASpotStatus.SingleOrDefault(row => row.Name == "Needs Fix");
               break;
            default:
               throw new ApplicationException(string.Format("Spot status is undefined or doesn't exist: {0}", oSpotStatus.ToString()));
         }

         return oIASpotStatus;
      }

      /// <summary>
      /// Retreives the apporpirate SpotFileType object from the DB based on the enum value passed in
      /// </summary>
      /// <param name="oSpotFileType"></param>
      /// <returns></returns>
      private IASpotFileType GetSpotFileTypeFromDB(SpotFileTypes oSpotFileType)
      {
         IASpotFileType oIASpotFileType = null;

         switch (oSpotFileType)
         {
            case SpotFileTypes.Production:
               oIASpotFileType = DataAccess.IASpotFileTypes.SingleOrDefault(row => row.Name == "Production");
               break;
            case SpotFileTypes.Talent:
               oIASpotFileType = DataAccess.IASpotFileTypes.SingleOrDefault(row => row.Name == "Talent");
               break;
            default:
               throw new ApplicationException(string.Format("Spot file type is undefined or doesn't exist: {0}", oSpotFileType.ToString()));
         }

         return oIASpotFileType;
      }

      private bool HasRole(string sRole)
      {
         var sCacheItemKey = sRole + "-" + MemberProtect.CurrentUser.UserID;
         var iReturnValue = false;

         if (HttpContext.Current.Cache[sCacheItemKey] == null)
         {
            iReturnValue = MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem(sRole));
            HttpContext.Current.Cache.Add(sCacheItemKey, iReturnValue, null, DateTime.Now.AddSeconds(600), TimeSpan.Zero, CacheItemPriority.Normal, null);
         }
         else
         {
            iReturnValue = (bool) HttpContext.Current.Cache[sCacheItemKey];
         }

         return iReturnValue;
      }

      #endregion

      #region Duplication Methods

      public bool Recut(IARequestProduction oIARequestProduction, string sDescription)
      {
         oIARequestProduction.HasRecutBeenRequested = true;
         oIARequestProduction.RecutRequestDateTime = DateTime.Now;
         oIARequestProduction.RecutNotes = sDescription;

         var oIAJob = RequestJobRecut(oIARequestProduction.IAJob);

         var oIARequestNote = new IARequestNote
         {
            IARequestID = oIAJob.IARequest.IARequestID,
            MPUserID = MemberProtect.CurrentUser.UserID,
            Note = string.Format("{0} note - {1}", oIAJob.Name, sDescription),
            CreatedDateTime = DateTime.Now
         };

         DataAccess.IARequestNotes.InsertOnSubmit(oIARequestNote);
         DataAccess.SubmitChanges();

         oIAJob.IARequest.IARequestStatusID = GetRequestStatusID(RequestStatus.Submitted);
         DataAccess.SubmitChanges();

         return true;
      }

      public IAJob RequestJobRecut(IAJob oIAJob)
      {
         var oIAJobDuplicate = new IAJob();
         oIAJobDuplicate.IARequestID = oIAJob.IARequestID;
         oIAJobDuplicate.IAJobStatusID = GetJobStatusID(JobStatus.ReCutRequested);
         oIAJobDuplicate.MPUserID = oIAJob.MPUserID;
         oIAJobDuplicate.MPUserIDCreated = oIAJob.MPUserIDCreated;
         oIAJobDuplicate.MPUserIDCompleted = Guid.Empty;
         oIAJobDuplicate.MPUserIDProducer = Guid.Empty;
         oIAJobDuplicate.Sequence = JobsService.GetNextJobSequenceNumberForRequest(oIAJob.IARequestID, DataAccess);
         oIAJobDuplicate.Name = string.Format("{0} - RECUT", oIAJob.Name);
         oIAJobDuplicate.Language = oIAJob.Language;
         oIAJobDuplicate.Notes = oIAJob.Notes;
         oIAJobDuplicate.QuantityMusic = oIAJob.QuantityMusic;
         oIAJobDuplicate.QuantitySFX = oIAJob.QuantitySFX;
         oIAJobDuplicate.QuantityProduction = oIAJob.QuantityProduction;
         oIAJobDuplicate.QuantityConvert = oIAJob.QuantityConvert;
         oIAJobDuplicate.PriceMusic = oIAJob.PriceMusic;
         oIAJobDuplicate.PriceSFX = oIAJob.PriceSFX;
         oIAJobDuplicate.PriceProduction = oIAJob.PriceProduction;
         oIAJobDuplicate.PriceConvert = oIAJob.PriceConvert;
         oIAJobDuplicate.IsASAP = true;
         oIAJobDuplicate.IsMusic = oIAJob.IsMusic;
         oIAJobDuplicate.IsSFX = oIAJob.IsSFX;
         oIAJobDuplicate.IsProduction = oIAJob.IsProduction;
         oIAJobDuplicate.IsConvert = oIAJob.IsConvert;
         oIAJobDuplicate.IsSentToProduction = false;
         oIAJobDuplicate.ProductionDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
         oIAJobDuplicate.DueDateTime = oIAJob.DueDateTime;
         oIAJobDuplicate.CreatedDateTime = DateTime.Now;
         oIAJobDuplicate.CompletedDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
         DataAccess.IAJobs.InsertOnSubmit(oIAJobDuplicate);
         DataAccess.SubmitChanges();

         // Duplicate Job File Records
         foreach (var oIAJobFile in oIAJob.IAJobFiles)
         {
            var sExtension = string.Empty;
            var iIndex = oIAJobFile.FilenameUnique.LastIndexOf(".");
            if (iIndex > 0)
            {
               sExtension = oIAJobFile.FilenameUnique.Substring(iIndex, oIAJobFile.FilenameUnique.Length - iIndex);
            }

            var sFilename = string.Format("{0}{1}", Guid.NewGuid(), sExtension);

            try
            {
               File.Copy(string.Format("{0}{1}", UploadPath, oIAJobFile.FilenameUnique), string.Format("{0}{1}", UploadPath, sFilename));
            }
            catch (Exception)
            {
            }

            var oIAJobFileDuplicate = new IAJobFile();
            oIAJobFileDuplicate.IAJobID = oIAJobDuplicate.IAJobID;
            oIAJobFileDuplicate.Filename = oIAJobFile.Filename;
            oIAJobFileDuplicate.FilenameUnique = sFilename;
            oIAJobFileDuplicate.FileSize = oIAJobFile.FileSize;
            oIAJobFileDuplicate.CreatedDateTime = DateTime.Now;
            DataAccess.IAJobFiles.InsertOnSubmit(oIAJobFileDuplicate);
            DataAccess.SubmitChanges();
         }

         // Duplicate Production Orders and files
         foreach (var oIAProductionOrder in oIAJob.IAProductionOrders)
         {
            DuplicateProductionOrder(oIAProductionOrder, oIAJobDuplicate.IAJobID);
         }

         return oIAJobDuplicate;
      }

      public IAProductionOrder DuplicateProductionOrder(IAProductionOrder oIAProductionOrder)
      {
         return DuplicateProductionOrder(oIAProductionOrder, oIAProductionOrder.IAJobID);
      }

      public IAProductionOrder DuplicateProductionOrder(IAProductionOrder oIAProductionOrder, int iIAJobID)
      {
         var oIAProductionOrderDuplicate = new IAProductionOrder();
         oIAProductionOrderDuplicate.IAProductionOrderStatusID = GetProductionOrderStatusID(ProductionOrderStatus.Incomplete);
         oIAProductionOrderDuplicate.IAJobID = iIAJobID;
         oIAProductionOrderDuplicate.HasBeenViewedByTalent = false;
         oIAProductionOrderDuplicate.CompletedDateTime = DateTime.Now;
         oIAProductionOrderDuplicate.CreatedDateTime = DateTime.Now;
         oIAProductionOrderDuplicate.ProductionDateTime = DateTime.Now;
         oIAProductionOrderDuplicate.OnHoldDateTime = DateTime.Now;
         oIAProductionOrderDuplicate.IATalentTypeID = oIAProductionOrder.IATalentTypeID;
         oIAProductionOrderDuplicate.MPUserIDTalent = oIAProductionOrder.MPUserIDTalent;
         oIAProductionOrderDuplicate.MPUserIDProducer = oIAProductionOrder.MPUserIDProducer;
         oIAProductionOrderDuplicate.MPUserIDOnHold = oIAProductionOrder.MPUserIDOnHold;
         oIAProductionOrderDuplicate.Notes = string.Empty;
         DataAccess.IAProductionOrders.InsertOnSubmit(oIAProductionOrderDuplicate);
         DataAccess.SubmitChanges();

         // Duplicate spots and files
         foreach (var oIASpot in oIAProductionOrder.IASpots)
         {
            DuplicateSpot(oIASpot, oIAProductionOrderDuplicate.IAProductionOrderID);
         }

         return oIAProductionOrderDuplicate;
      }

      // Duplicate all records for the spot, including duplicate copies of the physical files
      public IASpot DuplicateSpot(IASpot oIASpot)
      {
         return DuplicateSpot(oIASpot, oIASpot.IAProductionOrderID);
      }

      public IASpot DuplicateSpot(IASpot oIASpot, int iIAProductionOrderID)
      {
         // Duplicate Spot Record
         var oIASpotDuplicate = new IASpot();
         oIASpotDuplicate.IAProductionOrderID = iIAProductionOrderID;
         oIASpotDuplicate.IASpotStatusID = GetSpotStatusID(SpotStatus.OnHold);
         oIASpotDuplicate.IASpotTypeID = oIASpot.IASpotTypeID;
         oIASpotDuplicate.PurchaseOrderNumber = oIASpot.PurchaseOrderNumber;
         oIASpotDuplicate.DueDateTime = oIASpot.DueDateTime;
         oIASpotDuplicate.Length = oIASpot.Length;
         oIASpotDuplicate.LengthActual = oIASpot.LengthActual;
         oIASpotDuplicate.Title = oIASpot.Title;
         oIASpotDuplicate.ProductionNotes = oIASpot.ProductionNotes;
         oIASpotDuplicate.Script = oIASpot.Script;
         oIASpotDuplicate.IsAsap = oIASpot.IsAsap;
         oIASpotDuplicate.IsReviewed = true;

         var oIAProductionOrder = DataAccess.IAProductionOrders.SingleOrDefault(row => row.IAProductionOrderID == iIAProductionOrderID);
         if (oIAProductionOrder != null)
         {
            if (oIAProductionOrder.IAJob.IAJobStatusID == GetJobStatusID(JobStatus.ReCutRequested))
            {
               // Mark duplicates created from recut request as not having been reviewed to force SS staff to review each and every spot before
               // being able to send them off to production.
               oIASpotDuplicate.IsReviewed = false;
            }
         }

         oIASpotDuplicate.CreatedDateTime = DateTime.Now;
         oIASpotDuplicate.CompletedDateTime = DateTime.Now;
         DataAccess.IASpots.InsertOnSubmit(oIASpotDuplicate);
         DataAccess.SubmitChanges();

         // Duplicate Spot Fee Records
         foreach (var oIASpotFee in oIASpot.IASpotFees)
         {
            var oIASpotFeeDuplicate = new IASpotFee();
            oIASpotFeeDuplicate.IASpotID = oIASpotDuplicate.IASpotID;
            oIASpotFeeDuplicate.IASpotFeeTypeID = oIASpotFee.IASpotFeeTypeID;
            oIASpotFeeDuplicate.Fee = oIASpotFee.Fee;
            oIASpotFeeDuplicate.LengthActual = oIASpotFee.LengthActual;
            DataAccess.IASpotFees.InsertOnSubmit(oIASpotFeeDuplicate);
            DataAccess.SubmitChanges();
         }

         // Duplicate Spot File Records
         foreach (var oIASpotFile in oIASpot.IASpotFiles.Where(row => row.IASpotFileTypeID == GetSpotFileTypeID(SpotFileTypes.Production)))
         {
            var sExtension = string.Empty;
            var iIndex = oIASpotFile.FilenameUnique.LastIndexOf(".");
            if (iIndex > 0)
            {
               sExtension = oIASpotFile.FilenameUnique.Substring(iIndex, oIASpotFile.FilenameUnique.Length - iIndex);
            }

            var sFilename = string.Format("{0}{1}", Guid.NewGuid(), sExtension);

            try
            {
               File.Copy(string.Format("{0}{1}", UploadPath, oIASpotFile.FilenameUnique), string.Format("{0}{1}", UploadPath, sFilename));
            }
            catch (Exception)
            {
            }

            var oIASpotFileDuplicate = new IASpotFile();
            oIASpotFileDuplicate.IASpotID = oIASpotDuplicate.IASpotID;
            oIASpotFileDuplicate.IASpotFileTypeID = oIASpotFile.IASpotFileTypeID;
            oIASpotFileDuplicate.Filename = oIASpotFile.Filename;
            oIASpotFileDuplicate.FilenameUnique = sFilename;
            oIASpotFileDuplicate.FileSize = oIASpotFile.FileSize;
            oIASpotFileDuplicate.IsDeletable = true;
            oIASpotFileDuplicate.CreatedDateTime = DateTime.Now;
            DataAccess.IASpotFiles.InsertOnSubmit(oIASpotFileDuplicate);
            DataAccess.SubmitChanges();
         }

         return oIASpotDuplicate;
      }

      #endregion

      #region Specific User Type Functions

      public bool IsUserCustomer(Guid oMPUserID)
      {
         return MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(oMPUserID, "IsCustomer"));
      }

      public bool IsUserStaff(Guid oMPUserID)
      {
         return MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(oMPUserID, "IsStaff"));
      }

      public bool IsUserTalent(Guid oMPUserID)
      {
         return MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(oMPUserID, "IsTalent"));
      }

      public bool IsUserAdmin(Guid oMPUserID)
      {
         return MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(oMPUserID, "IsAdmin"));
      }

      #endregion

      #region Current User Type Functions

      public bool IsCustomer
      {
         get { return HasRole("IsCustomer"); }
      }

      public bool IsStaff
      {
         get { return HasRole("IsStaff"); }
      }

      public bool IsTalent
      {
         get { return HasRole("IsTalent"); }
      }

      public bool IsAdmin
      {
         get { return HasRole("IsAdmin"); }
      }

      #endregion

      #region Public Properties

      public HttpContext HttpContext
      {
         get { return m_oHttpContext; }
      }

      public MemberProtect MemberProtect
      {
         get { return m_oMemberProtect; }
      }

      public DataAccessDataContext DataAccess
      {
         get { return RequestContextHelper.CurrentDB(); }
      }

      public string UploadPath
      {
         get { return Settings.UploadPath; }
      }

      public string UploadPathAsURL
      {
         get { return Settings.UploadPathAsURL; }
      }

      public string MusicPath
      {
         get { return Settings.MusicPath; }
      }

      public string InvoicePath
      {
         get { return Settings.InvoicePath; }
      }

      public string Separator
      {
         get { return m_sSeparator; }
      }

      public string SubSeparator
      {
         get { return m_sSubSeparator; }
      }

      public Regex RegexEmail
      {
         get { return Email.EmailAddress; }
      }

      public SitePropertiesService SiteProperites
      {
         get
         {
            if (m_siteProperties == null)
            {
               m_siteProperties = new SitePropertiesService();
            }

            return m_siteProperties;
         }
      }

      #endregion
   }
}