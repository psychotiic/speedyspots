using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetSolution.Web;
using SpeedySpots.InetActive.Objects;
using SpeedySpots.DataAccess;

namespace SpeedySpots.InetActive
{
    public partial class MemberProtectParams : InetActiveBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                m_txtTimespanBeforePasswordsExpire.Text = MemberProtect.Utility.FormatInteger(MemberProtect.Application.GetTimespanBeforePasswordsExpire().Days);
                m_txtTimespanBeforePasswordsExpireToNotifyUsers.Text = MemberProtect.Utility.FormatInteger(MemberProtect.Application.GetTimespanBeforePasswordsExpireToNotifyUsers().Days);
                m_txtTimespanBeforeTempPasswordsExpire.Text = MemberProtect.Utility.FormatInteger(MemberProtect.Application.GetTimespanBeforeTemporaryPasswordsExpire().Days);
                m_txtTimespanSinceLastLoginBeforeLockout.Text = MemberProtect.Utility.FormatInteger(MemberProtect.Application.GetTimespanSinceLastLoginBeforeLockout().Days);
                m_txtTimespanToKeepLogs.Text = MemberProtect.Utility.FormatInteger(MemberProtect.Application.GetTimespanToKeepLogs().Days);
                m_txtMaxPasswordHistoryCount.Text = MemberProtect.Utility.FormatInteger(MemberProtect.Application.GetMaxPasswordHistoryCount());
                m_txtMaxFailedLoginAttemptsBeforeLockout.Text = MemberProtect.Utility.FormatInteger(MemberProtect.Application.GetMaxFailedLoginAttemptsBeforeLockout());
                m_txtRBASystemTrackCount.Text  = MemberProtect.Utility.FormatInteger(MemberProtect.Application.GetRBASystemTrackCount());
            }
        }

        protected void OnSave(object sender, EventArgs e)
        {
            MemberProtect.Application.SetTimespanBeforePasswordsExpire(new TimeSpan(MemberProtect.Utility.ValidateInteger(m_txtTimespanBeforePasswordsExpire.Text), 0, 0, 0, 0));
            MemberProtect.Application.SetTimespanBeforePasswordsExpireToNotifyUsers(new TimeSpan(MemberProtect.Utility.ValidateInteger(m_txtTimespanBeforePasswordsExpireToNotifyUsers.Text), 0, 0, 0, 0));
            MemberProtect.Application.SetTimespanBeforeTemporaryPasswordsExpire(new TimeSpan(MemberProtect.Utility.ValidateInteger(m_txtTimespanBeforeTempPasswordsExpire.Text), 0, 0, 0, 0));
            MemberProtect.Application.SetTimespanSinceLastLoginBeforeLockout(new TimeSpan(MemberProtect.Utility.ValidateInteger(m_txtTimespanSinceLastLoginBeforeLockout.Text), 0, 0, 0, 0));
            MemberProtect.Application.SetTimespanToKeepLogs(new TimeSpan(MemberProtect.Utility.ValidateInteger(m_txtTimespanToKeepLogs.Text), 0, 0, 0, 0));
            MemberProtect.Application.SetMaxPasswordHistoryCount(MemberProtect.Utility.ValidateInteger(m_txtMaxPasswordHistoryCount.Text));
            MemberProtect.Application.SetMaxFailedLoginAttemptsBeforeLockout(MemberProtect.Utility.ValidateInteger(m_txtMaxFailedLoginAttemptsBeforeLockout.Text));
            MemberProtect.Application.SetRBASystemTrackCount(MemberProtect.Utility.ValidateInteger(m_txtRBASystemTrackCount.Text));

            ShowMessage("MemberProtect properties have been saved.", MessageTone.Positive);
        }
    }
}