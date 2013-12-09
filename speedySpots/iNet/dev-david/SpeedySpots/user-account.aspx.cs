namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Web.UI.WebControls;
   using Business.Services;
   using DataAccess;
   using InetSolution.Web;
   using Objects;
   using Telerik.Web.UI;

   public partial class user_account : SiteBasePage
   {
      private Guid m_oMPUserID = Guid.Empty;
      private string m_sPassword = string.Empty;

      protected void Page_Load(object sender, EventArgs e)
      {
         if (Request.QueryString["id"] != null)
         {
            m_oMPUserID = MemberProtect.Utility.ValidateGuid(Request.QueryString["id"]);
         }

         if (m_oMPUserID == Guid.Empty)
         {
            m_oMPUserID = MemberProtect.CurrentUser.UserID;
         }

         // If the user is a customer or talent and they are NOT viewing their own profile, redirect them them away
         // - Only Admins may view other people's profiles
         if (ApplicationContext.IsCustomer || ApplicationContext.IsTalent)
         {
            if (m_oMPUserID != MemberProtect.CurrentUser.UserID)
            {
               Response.Redirect("~/Default.aspx");
            }
         }
         else if (ApplicationContext.IsStaff && !ApplicationContext.IsAdmin)
         {
            if (m_oMPUserID != MemberProtect.CurrentUser.UserID)
            {
               // Staff users can only view customer records so if this isn't a customer, kick em out
               if (m_oMPUserID != Guid.Empty && !MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(m_oMPUserID, "IsCustomer")))
               {
                  Response.Redirect("~/Default.aspx");
               }
            }
         }

         if (!MemberProtect.User.Exists(m_oMPUserID))
         {
            m_oMPUserID = MemberProtect.CurrentUser.UserID;
         }

         if (Request.QueryString["mode"] != null)
         {
            if (ApplicationContext.IsAdmin || ApplicationContext.IsStaff)
            {
               if (Request.QueryString["mode"] == "new")
               {
                  m_oMPUserID = Guid.Empty;
               }
            }
         }

         if (!IsPostBack)
         {
            // Only another admin can change the admin status of themselves
            // - Sanity check to protect admins from themselves
            if (ApplicationContext.IsAdmin && m_oMPUserID == MemberProtect.CurrentUser.UserID)
            {
               m_chkIsAdmin.Enabled = false;
            }

            if (MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(m_oMPUserID, "IsArchived")))
            {
               m_btnArchive.Visible = false;
            }
            else
            {
               m_btnReactivate.Visible = false;
            }

            m_hdnAssignedCompanyID.Value = MemberProtect.Utility.FormatGuid(ApplicationContext.GetOrgID(m_oMPUserID));
            m_hdnAssignedCompanyName.Value = MemberProtect.Organization.GetName(new Guid(m_hdnAssignedCompanyID.Value));

            // Load User Profile
            m_txtUsername.Text = MemberProtect.User.GetUsername(m_oMPUserID);
            m_txtFirstName.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "FirstName");
            m_txtLastName.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "LastName");
            m_txtPhone.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "Phone");
            m_txtPhoneExt.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "PhoneExtension");
            m_txtMobilePhone.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "MobilePhone");
            m_txtDepartment.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "Department");
            m_txtComments.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "Comments");

            var sGridPageSize = MemberProtect.User.GetDataItem(m_oMPUserID, "GridPageSize");
            if (sGridPageSize == string.Empty)
            {
               sGridPageSize = "10";
            }

            m_cboGridPageSize.SelectedValue = sGridPageSize;
            m_txtAdditionalEmails.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "AdditionalEmails");

            m_divCompany.Visible = false;
            m_divDuplicates.Visible = false;
            m_divTalentType.Visible = false;
            m_divSchedule.Visible = false;

            if (ApplicationContext.IsStaff)
            {
               m_chkIsCustomer.Checked = MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(m_oMPUserID, "IsCustomer"));
            }

            if (ApplicationContext.IsAdmin && ApplicationContext.IsStaff)
            {
               // Talent Type
               m_chkTalentType.DataValueField = "TalentTypeId";
               m_chkTalentType.DataTextField = "Name";
               m_chkTalentType.DataSource = TalentTypeService.GetTalentTypes(DataAccess);
               m_chkTalentType.DataBind();

               foreach (ListItem oItem in m_chkTalentType.Items)
               {
                  if (
                     DataAccess.IAUserTalentTypes.Count(
                        row => row.MPUserID == m_oMPUserID && row.IATalentTypeID == MemberProtect.Utility.ValidateInteger(oItem.Value)) > 0)
                  {
                     oItem.Selected = true;
                  }
               }

               // Account Type
               m_chkIsCustomer.Checked = MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(m_oMPUserID, "IsCustomer"));
               m_chkIsStaff.Checked = MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(m_oMPUserID, "IsStaff"));
               m_chkIsTalent.Checked = MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(m_oMPUserID, "IsTalent"));
               m_chkIsAdmin.Checked = MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(m_oMPUserID, "IsAdmin"));

               if (m_oMPUserID == Guid.Empty)
               {
                  if (Request.QueryString["cid"] != null)
                  {
                     var oMPOrgID = MemberProtect.Utility.ValidateGuid(Request.QueryString["cid"]);
                     if (MemberProtect.Organization.Exists(oMPOrgID))
                     {
                        m_chkIsCustomer.Checked = true;

                        OnCustomer(this, new EventArgs());
                     }
                  }
               }
               else
               {
                  if (ApplicationContext.IsUserCustomer(m_oMPUserID))
                  {
                     m_divCompany.Visible = true;

                     var duplicateCompanies = DataAccess.fn_Admin_GetPossibleDuplicateOrganizations(ApplicationContext.GetOrgID(m_oMPUserID));
                     if (duplicateCompanies.Count() > 0)
                     {
                        m_divDuplicates.Visible = true;

                        m_oDuplicateRepeater.DataSource = duplicateCompanies.ToList();
                        m_oDuplicateRepeater.DataBind();
                     }

                     LoadCustomerCoworkers();
                  }

                  if (ApplicationContext.IsUserTalent(m_oMPUserID))
                  {
                     m_divTalentType.Visible = true;

                     if (ApplicationContext.IsAdmin || ApplicationContext.IsStaff)
                     {
                        m_divSchedule.Visible = true;
                     }
                  }
               }
            }
            else if (ApplicationContext.IsCustomer)
            {
               // Case 6068: 10/18/2011
               // Pages Affected: order-details.aspx, user-details.aspx
               // - This is being disabled on purpose to permanently hide the Credit Card Information part of a user's profile by request of SpeedySpots
               // they plan to revisit this in the future, which is why we aren't removing this completely, we'll re-enable later.

               //m_divCreditCard.Visible = true;
            }

            if (ApplicationContext.IsUserCustomer(m_oMPUserID))
            {
               m_divCompany.Visible = true;
            }

            m_divStaffDefaultTab.Visible = false;
            m_divNotes.Visible = false;
            if (ApplicationContext.IsAdmin || ApplicationContext.IsStaff)
            {
               if (ApplicationContext.IsUserCustomer(m_oMPUserID))
               {
                  m_divNotes.Visible = true;
                  m_txtNotes.Content = MemberProtect.User.GetDataItem(m_oMPUserID, "Notes");
               }

               if (ApplicationContext.IsUserStaff(m_oMPUserID))
               {
                  m_radDefaultTab.SelectedValue = MemberProtect.User.GetDataItem(m_oMPUserID, "DefaultTab");
                  m_divStaffDefaultTab.Visible = true;
               }
            }

            // Load Company Profile
            if (ApplicationContext.IsUserCustomer(m_oMPUserID))
            {
               //IACustomerCreditCard oIACustomerCreditCard = DataAccess.IACustomerCreditCards.SingleOrDefault(row => row.MPUserID == m_oMPUserID);
               //if(oIACustomerCreditCard != null)
               //{
               //    m_cboCreditCardType.SelectedValue = MemberProtect.Cryptography.Decrypt(oIACustomerCreditCard.CreditCardType);
               //    m_cboCreditCardExpireMonth.SelectedValue = MemberProtect.Cryptography.Decrypt(oIACustomerCreditCard.CreditCardExpirationMonth);
               //    m_cboCreditCardExpireYear.SelectedValue = MemberProtect.Cryptography.Decrypt(oIACustomerCreditCard.CreditCardExpirationYear);
               //    m_txtCreditCardFirstName.Text = MemberProtect.Cryptography.Decrypt(oIACustomerCreditCard.CreditCardFirstName);
               //    m_txtCreditCardLastName.Text = MemberProtect.Cryptography.Decrypt(oIACustomerCreditCard.CreditCardLastName);
               //    m_txtCreditCardZip.Text = MemberProtect.Cryptography.Decrypt(oIACustomerCreditCard.CreditCardZip);
               //}
            }

            // Load Talent Schedule
            if (ApplicationContext.IsUserTalent(MPUserID))
            {
               m_dtMondayIn.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
               m_dtMondayOut.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
               m_dtTuesdayIn.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
               m_dtTuesdayOut.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
               m_dtWednesdayIn.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
               m_dtWednesdayOut.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
               m_dtThursdayIn.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
               m_dtThursdayOut.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
               m_dtFridayIn.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
               m_dtFridayOut.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);

               var oIATalentSchedule = TalentScheduleService.GetTalentSchedule(m_oMPUserID, DataAccess);
               if (oIATalentSchedule != null)
               {
                  if (!(oIATalentSchedule.MondayInDateTime.Hour == 0 && oIATalentSchedule.MondayOutDateTime.Hour == 0))
                  {
                     m_dtMondayIn.SelectedDate = oIATalentSchedule.MondayInDateTime;
                     m_dtMondayOut.SelectedDate = oIATalentSchedule.MondayOutDateTime;
                  }

                  if (!(oIATalentSchedule.TuesdayInDateTime.Hour == 0 && oIATalentSchedule.TuesdayOutDateTime.Hour == 0))
                  {
                     m_dtTuesdayIn.SelectedDate = oIATalentSchedule.TuesdayInDateTime;
                     m_dtTuesdayOut.SelectedDate = oIATalentSchedule.TuesdayOutDateTime;
                  }

                  if (!(oIATalentSchedule.WednesdayInDateTime.Hour == 0 && oIATalentSchedule.WednesdayOutDateTime.Hour == 0))
                  {
                     m_dtWednesdayIn.SelectedDate = oIATalentSchedule.WednesdayInDateTime;
                     m_dtWednesdayOut.SelectedDate = oIATalentSchedule.WednesdayOutDateTime;
                  }

                  if (!(oIATalentSchedule.ThursdayInDateTime.Hour == 0 && oIATalentSchedule.ThursdayOutDateTime.Hour == 0))
                  {
                     m_dtThursdayIn.SelectedDate = oIATalentSchedule.ThursdayInDateTime;
                     m_dtThursdayOut.SelectedDate = oIATalentSchedule.ThursdayOutDateTime;
                  }

                  if (!(oIATalentSchedule.FridayInDateTime.Hour == 0 && oIATalentSchedule.FridayOutDateTime.Hour == 0))
                  {
                     m_dtFridayIn.SelectedDate = oIATalentSchedule.FridayInDateTime;
                     m_dtFridayOut.SelectedDate = oIATalentSchedule.FridayOutDateTime;
                  }
               }
            }
         }
      }

      protected void OnBack(object sender, EventArgs e)
      {
         if (MemberProtect.CurrentUser.UserID == m_oMPUserID)
         {
            Response.Redirect("~/Default.aspx");
         }
         else
         {
            Response.Redirect("~/admin-dashboard.aspx");
         }
      }

      protected void OnArchive(object sender, EventArgs e)
      {
         ApplicationContext.ArchiveUser(m_oMPUserID);

         m_btnArchive.Visible = false;
         m_btnReactivate.Visible = true;
      }

      protected void OnReactivate(object sender, EventArgs e)
      {
         MemberProtect.User.SetDataItem(m_oMPUserID, "IsArchived", MemberProtect.Utility.BoolToYesNo(false));
         MemberProtect.User.SetLockedStatus(MemberProtect.User.GetUsername(m_oMPUserID), false);

         m_btnArchive.Visible = true;
         m_btnReactivate.Visible = false;
      }

      protected void OnTalent(object sender, EventArgs e)
      {
         m_divTalentType.Visible = m_chkIsTalent.Checked;
         m_divSchedule.Visible = m_chkIsTalent.Checked;
      }

      protected void OnStaff(object sender, EventArgs e)
      {
         m_divStaffDefaultTab.Visible = m_chkIsStaff.Checked;
      }

      protected void OnCustomer(object sender, EventArgs e)
      {
         m_divCompany.Visible = m_chkIsCustomer.Checked;
         m_divNotes.Visible = m_chkIsCustomer.Checked;

         if (m_chkIsCustomer.Checked)
         {
            LoadCustomerCoworkers();
         }
      }

      protected void OnSaveSchedule(object sender, EventArgs e)
      {
         var oIATalentSchedule = TalentScheduleService.GetTalentSchedule(m_oMPUserID, DataAccess);
         if (oIATalentSchedule == null)
         {
            oIATalentSchedule = new IATalentSchedule();
            oIATalentSchedule.MPUserID = m_oMPUserID;
            DataAccess.IATalentSchedules.InsertOnSubmit(oIATalentSchedule);
         }

         if (m_dtMondayIn.SelectedDate.HasValue && m_dtMondayOut.SelectedDate.HasValue)
         {
            oIATalentSchedule.MondayInDateTime = m_dtMondayIn.SelectedDate.Value;
            oIATalentSchedule.MondayOutDateTime = m_dtMondayOut.SelectedDate.Value;
         }
         else
         {
            oIATalentSchedule.MondayInDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            oIATalentSchedule.MondayOutDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
         }

         if (m_dtTuesdayIn.SelectedDate.HasValue && m_dtTuesdayOut.SelectedDate.HasValue)
         {
            oIATalentSchedule.TuesdayInDateTime = m_dtTuesdayIn.SelectedDate.Value;
            oIATalentSchedule.TuesdayOutDateTime = m_dtTuesdayOut.SelectedDate.Value;
         }
         else
         {
            oIATalentSchedule.TuesdayInDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            oIATalentSchedule.TuesdayOutDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
         }

         if (m_dtWednesdayIn.SelectedDate.HasValue && m_dtWednesdayOut.SelectedDate.HasValue)
         {
            oIATalentSchedule.WednesdayInDateTime = m_dtWednesdayIn.SelectedDate.Value;
            oIATalentSchedule.WednesdayOutDateTime = m_dtWednesdayOut.SelectedDate.Value;
         }
         else
         {
            oIATalentSchedule.WednesdayInDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            oIATalentSchedule.WednesdayOutDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
         }

         if (m_dtThursdayIn.SelectedDate.HasValue && m_dtThursdayOut.SelectedDate.HasValue)
         {
            oIATalentSchedule.ThursdayInDateTime = m_dtThursdayIn.SelectedDate.Value;
            oIATalentSchedule.ThursdayOutDateTime = m_dtThursdayOut.SelectedDate.Value;
         }
         else
         {
            oIATalentSchedule.ThursdayInDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            oIATalentSchedule.ThursdayOutDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
         }

         if (m_dtFridayIn.SelectedDate.HasValue && m_dtFridayOut.SelectedDate.HasValue)
         {
            oIATalentSchedule.FridayInDateTime = m_dtFridayIn.SelectedDate.Value;
            oIATalentSchedule.FridayOutDateTime = m_dtFridayOut.SelectedDate.Value;
         }
         else
         {
            oIATalentSchedule.FridayInDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            oIATalentSchedule.FridayOutDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
         }

         DataAccess.SubmitChanges();
         TalentScheduleService.InvalidateCache();

         if (sender == m_btnSaveSchedule)
         {
            ShowMessage("Schedule saved.", MessageTone.Positive);
         }
      }

      protected void OnSubmit(object sender, EventArgs e)
      {
         // Validate
         if (m_oMPUserID == Guid.Empty)
         {
            // Password is required for a new user
            if (m_txtPassword.Text == string.Empty)
            {
               SetMessage("Please enter a password for this new user.", MessageTone.Negative);
               return;
            }

            // Company is required for a new user
            if (m_chkIsCustomer.Checked && MemberProtect.Utility.ValidateGuid(m_hdnAssignedCompanyID.Value) == Guid.Empty)
            {
               SetMessage("Please select a company for this new user.", MessageTone.Negative);
               return;
            }
         }

         if (MemberProtect.User.GetUsername(m_oMPUserID) != m_txtUsername.Text)
         {
            // Validate the username is not already in use
            if (!MemberProtect.User.Exists(m_txtUsername.Text))
            {
               MemberProtect.User.SetUsername(m_oMPUserID, m_txtUsername.Text);
            }
            else
            {
               SetMessage(string.Format("Username '{0}' is already in use.", m_txtUsername.Text));
               return;
            }
         }

         // Validate credit card information for Customers
         //if(ApplicationContext.IsUserCustomer(m_oMPUserID))
         //{
         //    if(m_cboCreditCardType.SelectedValue != string.Empty && m_txtCreditCardNumber.Text != string.Empty)
         //    {
         //        if(m_cboCreditCardType.SelectedValue == "AmericanExpress")
         //        {
         //            Regex oRegex = new Regex("^\\d{15}$");
         //            if(!oRegex.Match(m_txtCreditCardNumber.Text).Success)
         //            {
         //                SetMessage("Credit Card Number must be a valid American Express number (123123412341234).", InetSolution.Web.MessageTone.Negative);
         //                return;
         //            }
         //        }
         //        else
         //        {
         //            Regex oRegex = new Regex("^\\d{16}$");
         //            if(!oRegex.Match(m_txtCreditCardNumber.Text).Success)
         //            {
         //                SetMessage("Credit Card Number must be a valid credit card number (1234123412341234).", InetSolution.Web.MessageTone.Negative);
         //                return;
         //            }
         //        }
         //    }
         //}

         if (m_oMPUserID == Guid.Empty)
         {
            m_oMPUserID = MemberProtect.User.Add(m_txtUsername.Text, m_txtPassword.Text);
            MemberProtect.User.SetDataItem(m_oMPUserID, "IsArchived", MemberProtect.Utility.BoolToYesNo(false));

            // Only customers are associate with organizations
            if (m_chkIsCustomer.Checked)
            {
               if (ApplicationContext.IsAdmin || ApplicationContext.IsStaff)
               {
                  // Assign the new user to the existing organization
                  MemberProtect.User.AssignOrganization(m_oMPUserID, MemberProtect.Utility.ValidateGuid(m_hdnAssignedCompanyID.Value));
               }
            }
         }
         else
         {
            if (m_txtPassword.Text != string.Empty)
            {
               MemberProtect.User.ChangePassword(m_oMPUserID, m_txtPassword.Text);
            }
         }

         // Update User Profile
         MemberProtect.User.SetDataItem(m_oMPUserID, "FirstName", m_txtFirstName.Text);
         MemberProtect.User.SetDataItem(m_oMPUserID, "LastName", m_txtLastName.Text);
         MemberProtect.User.SetDataItem(m_oMPUserID, "Phone", m_txtPhone.Text);
         MemberProtect.User.SetDataItem(m_oMPUserID, "PhoneExtension", m_txtPhoneExt.Text);
         MemberProtect.User.SetDataItem(m_oMPUserID, "MobilePhone", m_txtMobilePhone.Text);
         MemberProtect.User.SetDataItem(m_oMPUserID, "Department", m_txtDepartment.Text);
         MemberProtect.User.SetDataItem(m_oMPUserID, "Comments", m_txtComments.Text);
         MemberProtect.User.SetDataItem(m_oMPUserID, "Notes", m_txtNotes.Content);
         MemberProtect.User.SetDataItem(m_oMPUserID, "GridPageSize", m_cboGridPageSize.SelectedValue);
         MemberProtect.User.SetDataItem(m_oMPUserID, "AdditionalEmails", m_txtAdditionalEmails.Text);

         if (ApplicationContext.IsStaff)
         {
            MemberProtect.User.SetDataItem(m_oMPUserID, "IsCustomer", MemberProtect.Utility.BoolToYesNo(m_chkIsCustomer.Checked));
         }

         if (ApplicationContext.IsAdmin && ApplicationContext.IsStaff)
         {
            MemberProtect.User.SetDataItem(m_oMPUserID, "IsStaff", MemberProtect.Utility.BoolToYesNo(m_chkIsStaff.Checked));
            MemberProtect.User.SetDataItem(m_oMPUserID, "IsTalent", MemberProtect.Utility.BoolToYesNo(m_chkIsTalent.Checked));
            MemberProtect.User.SetDataItem(m_oMPUserID, "IsAdmin", MemberProtect.Utility.BoolToYesNo(m_chkIsAdmin.Checked));

            if (!IsNew && Request.QueryString["mode"] == null)
            {
               if (ApplicationContext.GetOrgID(m_oMPUserID) != MemberProtect.Utility.ValidateGuid(m_hdnAssignedCompanyID.Value))
               {
                  // Remove current association
                  if (MemberProtect.User.UnassignOrganization(m_oMPUserID, ApplicationContext.GetOrgID(m_oMPUserID)))
                  {
                     // Add new association
                     MemberProtect.User.AssignOrganization(m_oMPUserID, MemberProtect.Utility.ValidateGuid(m_hdnAssignedCompanyID.Value));
                     ApplicationContext.UpdateUserOrgID(m_oMPUserID, MemberProtect.Utility.ValidateGuid(m_hdnAssignedCompanyID.Value));

                     m_hdnAssignedCompanyName.Value = MemberProtect.Organization.GetName(MemberProtect.Utility.ValidateGuid(m_hdnAssignedCompanyID.Value));
                  }
               }
            }
         }

         // Save Company Profile & Billing Information
         if (ApplicationContext.IsUserCustomer(m_oMPUserID))
         {
            //IACustomerCreditCard oIACustomerCreditCard = DataAccess.IACustomerCreditCards.SingleOrDefault(row => row.MPUserID == m_oMPUserID);
            //if(oIACustomerCreditCard == null)
            //{
            //    oIACustomerCreditCard = new IACustomerCreditCard();
            //    oIACustomerCreditCard.MPUserID = m_oMPUserID;
            //    oIACustomerCreditCard.Name = "Default";
            //    DataAccess.IACustomerCreditCards.InsertOnSubmit(oIACustomerCreditCard);
            //}

            //oIACustomerCreditCard.CreditCardType = MemberProtect.Cryptography.Encrypt(m_cboCreditCardType.SelectedValue);

            //if(m_cboCreditCardType.SelectedValue != string.Empty && m_txtCreditCardNumber.Text != string.Empty)
            //{
            //    oIACustomerCreditCard.CreditCardNumber = MemberProtect.Cryptography.Encrypt(m_txtCreditCardNumber.Text);
            //}
            //else if(m_cboCreditCardType.SelectedValue == string.Empty && m_txtCreditCardNumber.Text == string.Empty)
            //{
            //    oIACustomerCreditCard.CreditCardNumber = string.Empty;
            //}

            //oIACustomerCreditCard.CreditCardExpirationMonth = MemberProtect.Cryptography.Encrypt(m_cboCreditCardExpireMonth.SelectedValue);
            //oIACustomerCreditCard.CreditCardExpirationYear = MemberProtect.Cryptography.Encrypt(m_cboCreditCardExpireYear.SelectedValue);
            //oIACustomerCreditCard.CreditCardFirstName = MemberProtect.Cryptography.Encrypt(m_txtCreditCardFirstName.Text);
            //oIACustomerCreditCard.CreditCardLastName = MemberProtect.Cryptography.Encrypt(m_txtCreditCardLastName.Text);
            //oIACustomerCreditCard.CreditCardZip = MemberProtect.Cryptography.Encrypt(m_txtCreditCardZip.Text);
            //DataAccess.SubmitChanges();

            //If we're a staff member looking at a customer, we want to save their coworker associations
            if (ApplicationContext.IsAdmin && ApplicationContext.IsStaff)
            {
               //Clear out all associations from this customer
               DataAccess.ExecuteCommand("DELETE IACustomerCoworker WHERE MPUserID = {0}", m_oMPUserID);

               foreach (RadListBoxItem item in m_RadSelectedEmployees.Items)
               {
                  var coworker = new IACustomerCoworker();
                  coworker.IACustomerCoworkerID = Guid.NewGuid();
                  coworker.MPUserID = m_oMPUserID;
                  coworker.MPUserIDCoworker = new Guid(item.Value);
                  DataAccess.IACustomerCoworkers.InsertOnSubmit(coworker);
               }
               DataAccess.SubmitChanges();
               ApplicationContext.CustomerHasCoworkersAssociatedReset(m_oMPUserID);
            }
         }

         if (ApplicationContext.IsUserTalent(m_oMPUserID))
         {
            foreach (ListItem oItem in m_chkTalentType.Items)
            {
               var talentTypeId = MemberProtect.Utility.ValidateInteger(oItem.Value);

               if (oItem.Selected)
               {
                  if (DataAccess.IAUserTalentTypes.Count(row => row.MPUserID == m_oMPUserID && row.IATalentTypeID == talentTypeId) == 0)
                  {
                     var oIAUserTalentType = new IAUserTalentType();
                     oIAUserTalentType.MPUserID = m_oMPUserID;
                     oIAUserTalentType.IATalentTypeID = talentTypeId;
                     DataAccess.IAUserTalentTypes.InsertOnSubmit(oIAUserTalentType);
                     DataAccess.SubmitChanges();
                  }
               }
               else
               {
                  if (DataAccess.IAUserTalentTypes.Count(row => row.MPUserID == m_oMPUserID && row.IATalentTypeID == talentTypeId) > 0)
                  {
                     var oIAUserTalentType = DataAccess.IAUserTalentTypes.SingleOrDefault(row => row.MPUserID == m_oMPUserID && row.IATalentTypeID == talentTypeId);
                     if (oIAUserTalentType != null)
                     {
                        DataAccess.IAUserTalentTypes.DeleteOnSubmit(oIAUserTalentType);
                        DataAccess.SubmitChanges();
                     }
                  }
               }
            }

            OnSaveSchedule(this, new EventArgs());

            ShowMessage("User details saved.", MessageTone.Positive);
            TalentsService.InvalidateListCache();
            TalentsService.InvalidateTalentInTypeListCache();
         }

         if (ApplicationContext.IsUserStaff(m_oMPUserID))
         {
            MemberProtect.User.SetDataItem(m_oMPUserID, "DefaultTab", m_radDefaultTab.SelectedValue);
         }

         if (MemberProtect.CurrentUser.UserID == m_oMPUserID)
         {
            ShowMessage("Account information saved.", MessageTone.Positive);
         }
         else
         {
            RedirectMessage(string.Format("~/user-account.aspx?id={0}", m_oMPUserID.ToString().Replace("-", string.Empty)), "Account information saved",
                            MessageTone.Positive);
         }
      }

      private void LoadCustomerCoworkers()
      {
         var unassignedCount = LoadUnAssignedCustomerCoworkers();
         var assignedCount = LoadAssignedCustomerCoworkers();

         if (unassignedCount == 0 && assignedCount == 0)
         {
            m_divCoworkerManager.Visible = false;
         }
         else
         {
            m_divCoworkerManager.Visible = true;
         }
      }

      private int LoadUnAssignedCustomerCoworkers()
      {
         var orgId = ApplicationContext.GetOrgID(m_oMPUserID);
         var itemCount = 0;

         if (orgId != Guid.Empty)
         {
            var innerQuery = from ou in DataAccess.MPOrgUsers
                             where !(from cc in DataAccess.IACustomerCoworkers
                                     where cc.MPUserID == m_oMPUserID
                                     select cc.MPUserIDCoworker).Contains(ou.MPUserID)
                                   && ou.MPOrgID == orgId
                                   && ou.MPUserID != m_oMPUserID
                             select ou.MPUserID;

            var query = from u in DataAccess.MPUserDatas
                        where innerQuery.Contains(u.MPUserID)
                        select new
                        {
                           u.MPUserID,
                           Name = u.FirstName + " " + u.LastName
                        };

            m_RadEmployees.DataKeyField = "MPUserID";
            m_RadEmployees.DataValueField = "MPUserID";
            m_RadEmployees.DataTextField = "Name";
            m_RadEmployees.DataSource = query.OrderBy(u => u.Name).ToList();
            m_RadEmployees.DataBind();

            itemCount = m_RadEmployees.Items.Count();
         }

         return itemCount;
      }

      private int LoadAssignedCustomerCoworkers()
      {
         var itemCount = 0;

         if (ApplicationContext.GetOrgID(m_oMPUserID) != Guid.Empty)
         {
            var innerQuery = from cc in DataAccess.IACustomerCoworkers
                             where cc.MPUserID == m_oMPUserID
                             select cc.MPUserIDCoworker;

            var query = from u in DataAccess.MPUserDatas
                        where innerQuery.Contains(u.MPUserID)
                        select new
                        {
                           u.MPUserID,
                           Name = u.FirstName + " " + u.LastName
                        };

            m_RadSelectedEmployees.DataKeyField = "MPUserID";
            m_RadSelectedEmployees.DataValueField = "MPUserID";
            m_RadSelectedEmployees.DataTextField = "Name";
            m_RadSelectedEmployees.DataSource = query.OrderBy(u => u.Name).ToList();
            m_RadSelectedEmployees.DataBind();

            itemCount = m_RadSelectedEmployees.Items.Count();
         }

         return itemCount;
      }

      #region Public Methods

      public string FormatVerified(string sValue)
      {
         if (MemberProtect.Utility.YesNoToBool(sValue))
         {
            return "<span style='color: red; font-weight: bold;'>*</span>";
         }
         else
         {
            return string.Empty;
         }
      }

      #endregion

      #region Public Properties

      public Guid MPUserID
      {
         get { return m_oMPUserID; }
      }

      public Guid MPOrgID
      {
         get { return ApplicationContext.GetOrgID(m_oMPUserID); }
      }

      public bool IsNew
      {
         get
         {
            if (m_oMPUserID == Guid.Empty)
            {
               return true;
            }
            else
            {
               return false;
            }
         }
      }

      public string CompanyName
      {
         get { return MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(m_oMPUserID)); }
      }

      #endregion

      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl>
         {
            AccessControl.Admin,
            AccessControl.Staff,
            AccessControl.Talent,
            AccessControl.Customer
         };
      }
   }
}