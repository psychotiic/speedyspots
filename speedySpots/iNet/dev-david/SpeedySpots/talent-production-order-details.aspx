<%@ Page Title="Speedy Spots :: Spot to Record" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="talent-production-order-details.aspx.cs" Inherits="SpeedySpots.talent_production_order_details" %>
<%@ Import Namespace="SpeedySpots.Business" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<link rel="stylesheet" type="text/css" media="print" href="css/reset.css" />
<style type="text/css" media="screen">
    .pagebreak hr 
    {
    	display: none;
    }
    
    h3#script 
    {
    	margin-top: 18px;
    	padding-top: 18px;
    	border-top: 1px dashed #ccc;
    }
    table.actualFees thead tr td {
        text-align: left;
        width: 33%;
    }
</style>
<link rel="stylesheet" type="text/css" media="print" href="css/sssms-print.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="full">
    <p class="breadcrumb"><asp:HyperLink ID="hlBreadcrumb" runat="server" /> &raquo; <strong>Record a Spot</strong></p>
    <h2>Production Order</h2>
    <div class="button-row action">
        <a href="javascript:window.print()" class="button primaryAction">Click to Print This Page</a>
    </div>
    
    <asp:Repeater ID="m_oRepeater" runat="server">
        <ItemTemplate>
            <div class="hr"><hr /></div>

            <div class="form-holder group details">
            <fieldset class="half">
                <legend>Details:</legend>
                <div class="group">
                    <label>Job Title:</label>
                    <span class="output"><%#DataBinder.Eval(Container.DataItem, "IAProductionOrder.IAJob.Name") %></span>
                </div>
                <div class="group">
                    <label>Spot:</label>
                    <span class="output"><%#DataBinder.Eval(Container.DataItem, "Title") %> <span id="m_spanRecord" runat="server" style="color: Red;">(Re-Record)</span></span>
                </div>
                <% if(IAProductionOrder.IAProductionOrderStatusID == ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Complete)) %>
                <% { %>
                <div class="group">
                    <label>Completed:</label>
                    <span class="output"><%=string.Format("{0:dddd, dd a\\t h:mm tt}", IAProductionOrder.CompletedDateTime)%></span>
                </div>
                <% } %>
                <div class="group">
                    <label>Length:</label>
                    <div class="inputgroup">
                        <div class="inputgroup">
                            <span class="output"><%#DataBinder.Eval(Container.DataItem, "Length") %></span>
                        </div>
                    </div>
                </div>
                <div id="m_divFees" runat="server" class="group">
                    <label>Fees:</label>
                    <div class="inputgroup">
                        <span class="output">
                        <asp:Repeater ID="m_oRepeaterFees" runat="server">
                           <HeaderTemplate>
                              <table class="actualFees" border="0">
                                  <tbody>
                           </HeaderTemplate>
                            <ItemTemplate>
                                 <tr>
                                    <td nowrap><%#DataBinder.Eval(Container.DataItem, "IASpotFeeType.Name") %>:&nbsp;</td>
                                    <% if(IAProductionOrder.IAProductionOrderStatusID == ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Complete)) %>
                                    <% { %>
                                    <td><%#DataBinder.Eval(Container.DataItem, "LengthActual") %></td>
                                    <td>&nbsp;@&nbsp;</td>
                                    <% } %>
                                    <td><%#DataBinder.Eval(Container.DataItem, "Fee", "{0:c}") %></td>
                                 </tr>
                            </ItemTemplate>
                           <FooterTemplate>
                                    </tbody>
                               </table>
                           </FooterTemplate>
                        </asp:Repeater>
                        </span>
                    </div>
                </div>
            </fieldset>

            <fieldset class="half">
                <legend>&nbsp;</legend>
                 <div class="group">
                    <label>Job #:</label>
                    <span class="output"><%#DataBinder.Eval(Container.DataItem, "IAProductionOrder.IAJob.JobIDForDisplay")%></span>
                </div>
                 <div class="group">
                    <label>Producer(s):</label>
                    <span class="output"><%#GetProducers((int)DataBinder.Eval(Container.DataItem, "IAProductionOrder.IAJob.IAJobID"))%></span>
                </div>
                <div class="group">
                    <label>Sent:</label>
                    <span class="output"><%#string.Format("{0:dddd, dd a\\t h:mm tt}", DataBinder.Eval(Container.DataItem, "IAProductionOrder.IAJob.ProductionDateTime"))%></span>
                </div>
                <div class="group">
                    <label>Due:</label>
                    <span class="output"><%#string.Format("{0:dddd, dd a\\t h:mm tt}", DataBinder.Eval(Container.DataItem, "DueDateTime")) %> <%# (bool)DataBinder.Eval(Container.DataItem, "IsAsap") == true ? "<span class='red'>ASAP</span>" : "" %></span>
                </div>
            </fieldset>
            </div>

            <h3>Production Notes:</h3>
            <div id="prodnotes">
            <p><%# DataBinder.Eval(Container.DataItem, "ProductionNotes") %></p>

            <div id="m_divProductionFiles" runat="server" class="noprint">
                <div class="message">
                    <p>It is requested that you listen to the following file(s):</p>
                </div>
                <div class="form-holder">
                    <fieldset>
                        <div class="group">
                            <label>Requested listenting:</label>
                            <div class="inputgroup">
                                <asp:Repeater ID="m_oRepeaterProductionFiles" runat="server">
                                    <ItemTemplate>
                                        <a href='download.aspx?id=<%#DataBinder.Eval(Container.DataItem, "IASpotFileID") %>&type=spot'><%#DataBinder.Eval(Container.DataItem, "Filename") %></a>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>

             </div> <!-- /prodnotes -->

            <h3 id="script">Script:</h3>
            <div class="script">
                <%#DataBinder.Eval(Container.DataItem, "Script") %>
            </div>

            <% if(IAProductionOrder.IAProductionOrderStatusID == ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Incomplete)) %>
            <% { %>
            <div id="m_divUpload" runat="server" class="form-holder noprint">
                <fieldset>
                    <legend>Send us your finished recording(s):</legend>
                    <div class="group">
                        <asp:Label ID="m_lblFinishedFile" runat="server" Text="Recording:" AssociatedControlID="m_oUpload" CssClass="required"></asp:Label>
                        <div class="inputgroup">
                            <telerik:RadUpload ID="m_oUpload" runat="server" ControlObjectsVisibility="AddButton,RemoveButtons" Index='<%# MemberProtect.Cryptography.Encrypt(MemberProtect.Utility.FormatInteger((int)DataBinder.Eval(Container.DataItem, "IASpotID"))) %>'></telerik:RadUpload>
                        </div>
                    </div>
                </fieldset>
            </div>
            <% } %>
            <div id="m_divTalentFiles" runat="server" class="noprint">
                <div class="form-holder">
                    <fieldset>
                        <legend>Files:</legend>
                        <div class="group">
                            <label>Uploaded Files:</label>
                            <div class="inputgroup">
                                <asp:Repeater ID="m_oRepeaterTalentFiles" runat="server" OnItemCommand="OnTalentFilesCommand">
                                    <ItemTemplate>
                                        <a href='download.aspx?id=<%#DataBinder.Eval(Container.DataItem, "IASpotFileID") %>&type=spot'><%#DataBinder.Eval(Container.DataItem, "Filename") %></a>
                                        <span id="m_spanDelete" runat="server">
                                            -
                                            <asp:LinkButton ID="m_btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "IASpotFileID") %>' OnClientClick="return ConfirmUser('Are you sure you want to delete this file?')"></asp:LinkButton>
                                        </span>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            <%# ProcessPageBreak((int)(DataBinder.Eval(Container, "ItemIndex")), (int)(DataBinder.Eval(Container.DataItem, "IAProductionOrder.IASpots.Count"))) %>
        </ItemTemplate>
    </asp:Repeater>

    <div class="hr"><hr /></div>

    <div class="form-holder noprint">
        <fieldset>
            <legend>Notes for QC:</legend>
            <div class="group">
            <% if(IAProductionOrder.IAProductionOrderStatusID == ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Incomplete)) %>
            <% { %>
                <asp:Label ID="m_lblNotes" runat="server" Text="Notes:" AssociatedControlID="m_txtNotes" CssClass="required"></asp:Label>
                <telerik:RadTextBox ID="m_txtNotes" runat="server" Columns="50" TextMode="MultiLine" Rows="5" MaxLength="200"></telerik:RadTextBox>
            <% } %>
            <% else %>
            <% { %>
                <label>Notes:</label>
                <span class="output"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(IAProductionOrder.Notes) %></span>
            <% } %>
            </div>
        </fieldset>
    </div>
    <div class="button-row">
        <% if(IAProductionOrder.IAProductionOrderStatusID == ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Incomplete)) %>
        <% { %>
        <asp:LinkButton ID="m_btnFinished" runat="server" Text="Finished, mark as complete" CssClass="button primaryAction" OnClick="OnFinished" />
        <asp:LinkButton ID="m_btnSave" runat="server" Text="Save Changes" CssClass="button" OnClick="OnSave" />
        <% } %>
        <asp:Hyperlink ID="hlBack" runat="server" Text="Back" CssClass="button" />
    </div>
</div>
</asp:Content>