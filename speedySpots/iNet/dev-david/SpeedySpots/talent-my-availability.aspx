<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="talent-my-availability.aspx.cs" Inherits="SpeedySpots.talent_my_availability" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="full">
    <div class="group">
        <div class="order-list">
            <div class="tab-holder group">
                <ul id="dashboard-tabs">
                    <li><a href="talent-dashboard.aspx">Pending POs</a></li>
                    <li><a href="talent-production-orders-completed.aspx">Completed POs</a></li>
                    <li class="at"><a href="talent-my-availability.aspx">My Availability</a></li>
                    <li><a href="messages-inbox.aspx">Messages</a></li>
                </ul>
            </div>

            <h2>Your Availability</h2>
            <p>Please input the days or times you will be <strong>unavailable</strong> during normal Speedy Spots business hours.</p>
            
            <div class="form-holder">
                <fieldset>
                    <legend>Add New Unavailablity:</legend>
                    <div class="group">
                        <label>
                            <span class="required">Delivery Obligation:</span>
                            <asp:RequiredFieldValidator ID="m_reqFrom" runat="server" ControlToValidate="m_dtFrom" ErrorMessage="Please enter the date you will be unavailable from.">*</asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="m_reqTo" runat="server" ControlToValidate="m_dtTo" ErrorMessage="Please enter the date you will return.">*</asp:RequiredFieldValidator>
                        </label>
                        <div class="inputgroup">
                            I will be unavailable from <telerik:RadDateTimePicker runat="server" ID="m_dtFrom" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                                <Calendar>
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                                        <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDateTimePicker> and good for <telerik:RadDateTimePicker runat="server" ID="m_dtTo" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                                <Calendar>
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                                        <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDateTimePicker> delivery when I return.
                        </div>
                    </div>
                    <div class="group">
                        <label>Notes/Description:</label>
                        <div class="inputgroup">
                            <telerik:RadTextBox ID="m_txtNotes" runat="server" TextMode="MultiLine" Rows="4" Columns="50" MaxLength="500"></telerik:RadTextBox>
                        </div>
                    </div>
                </fieldset>
            </div>

            <div class="button-row">
                <asp:LinkButton ID="m_btnFilter" runat="server" Text="Submit Unavailability" CssClass="button primaryAction" OnClick="OnSubmit" />
            </div>

            <h3>Unavailable:</h3>
            <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" onitemcommand="OnItemCommand" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced">
                <MasterTableView NoMasterRecordsText="No unavailability submissions"></MasterTableView>
                <HeaderContextMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                </HeaderContextMenu>

                <MasterTableView>
                <RowIndicatorColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>

                <ExpandCollapseColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <SortExpressions>
                <telerik:GridSortExpression FieldName="CreatedDateTime" SortOrder="Descending" />
                </SortExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="IATalentUnavailabilityID" UniqueName="IATalentUnavailabilityID" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FromDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="From" UniqueName="From"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ToDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="To" UniqueName="To"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"></telerik:GridBoundColumn>
                        <telerik:GridButtonColumn Text="Delete" UniqueName="Delete" CommandName="Delete" ConfirmText="Are you sure you want to delete this unavailability?"></telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>

                <FilterMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                </FilterMenu>
            </telerik:RadGrid>
        </div>
    </div>
</div>
</asp:Content>