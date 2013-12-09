<%@ Page Title="Speedy Spots :: Create Order :: Confirmation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="create-request-confirm.aspx.cs" Inherits="SpeedySpots.create_request_confirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="full">
    <h2>Confirmation for Request #<%=IARequest.RequestIdForDisplay %></h2>
    <div id="m_divMessagePositive" runat="server" class="message positive" visible="false">
        <p><strong>We've received your request.</strong> Return to your <a href="<%=GetDashboardUrl() %>">user dashboard</a> to track the status of this request.</p>
    </div>
   <div id="paymentPreAuthorized" runat="server" class="message" Visible="False">
      <p>You have pre-authorized payment of this request, a receipt will be sent when the card has been charged.</p>
   </div>
    <div id="m_divMessageNegative" runat="server" class="message negative" visible="false">
        <p><strong>We've partially received your request.</strong><br />
        <asp:Literal ID="m_litError" runat="server" /><br />
        Please <a href="/pages/contact_us.aspx">contact us</a> with the request # below, your production notes and script.</p>
    </div>
   <div id="estimateMessage" class="message" runat="server" Visible="False">
        <p>You have preauthorized the payment of this request.  A receipt will be sent one the card when the card has been charged.</p>
    </div>

    <div class="form-holder receipt">
        <fieldset>
            <legend>Request Details:</legend>
            <div class="group">
                <label class="required">Request:</label>
                <span class="output"><%=IARequest.RequestIdForDisplay %></span>
            </div>
            <div class="group">
                <label class="required">Submitted By:</label>
                <span class="output"><%=string.Format("{0} {1}", MemberProtect.User.GetDataItem(IARequest.MPUserID, "FirstName"), MemberProtect.User.GetDataItem(IARequest.MPUserID, "LastName")) %></span>
            </div>
            <div class="group">
                <label class="required">Contact Phone:</label>
                <span class="output">
                <% if(IARequest.ContactPhoneExtension != string.Empty) %>
                <% { %>
                    <%=string.Format("{0} x{1}", IARequest.ContactPhone, IARequest.ContactPhoneExtension) %>
                <% } %>
                <% else %>
                <% { %>
                    <%=IARequest.ContactPhone %>
                <% } %>
                </span>
            </div>
            <div class="group">
                <label class="required">Company Name:</label>
                <span class="output">
                <%=MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(IARequest.MPUserID)) %>
                </span>
            </div>
            <div class="group">
                <label class="required">Spot Notify:</label>
                <span class="output">
                <%=IARequest.NotificationEmails %>
                </span>
            </div>
            <div class="hr"><hr /></div>
            <% if(IARequest.IARequestFiles.Count > 0) { %>
            <div class="group">
                <label class="required">File(s):</label>
                <div class="inputgroup">
                <span class="output">
                    <% foreach(SpeedySpots.DataAccess.IARequestFile oIARequestFile in IARequest.IARequestFiles) %> <!-- ERROR FOUND -->
                    <% { %>
                        <%=string.Format("{0} - <a href='download.aspx?id={1}&type=request'>Download</a><br />", oIARequestFile.Filename, oIARequestFile.IARequestFileID) %>
                    <% } %>
                </span>
                </div>
            </div>
            <div class="hr"><hr /></div>
            <% } %>
            <% if(IARequest.Script != string.Empty) %>
            <% { %>
            <div class="group">
                <label class="required">Script:</label>
                <div class="inputgroup">
                <span class="output">
                    <%=IARequest.ScriptForDisplay %>
                </span>
                </div>
            </div>
            <% } %>
            <div class="group">
                <label class="required">Prod. Notes:</label>
                <div class="inputgroup">
                <span class="output">
                    <%=IARequest.ProductionNotesForDisplay %>
                </span>
                </div>
            </div>
        </fieldset>
    </div>

    <div class="button-row">
        <a href="default.aspx" class="button">Return to Your Dashboard</a>
    </div>
</div>
</asp:Content>
