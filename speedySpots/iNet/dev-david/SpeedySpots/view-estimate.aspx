<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view-estimate.aspx.cs" Inherits="SpeedySpots.view_estimate" %>

<!DOCTYPE html>
<html lang="en">
<head>
   <title>Estimate</title>
   <link rel="stylesheet" type="text/css" media="screen, projection" href="css/sssms-forms.css" />
   <link rel="stylesheet" type="text/css" media="print" href="css/sssms-print.css" />
   <style type="text/css">
      body
      {
         font-size: 100%;
         font-family: "Trebuchet MS", Arial, Verdana, Helvetica, sans-serif;
      }
   </style>
</head>
<body>
   <div class="button-row">
      <a href="javascript:window.print()" class="button primaryAction">Click to Print This Page</a>
   </div>

   <div id="header">
      <img src="img/ss-logo-print.gif" height="114" width="615" alt="Speedy Spots" />
   </div>
   <div id="body">
      <h2>Estimate:</h2>
      <p><%= IARequestEstimate.EmailBody %></p>
      <div class="form-holder">
         <fieldset>
            <legend>Details:</legend>
            <div class="group">
               <label>Staff:</label>
               <span class="output"><%= MemberProtect.User.GetDataItem(IARequestEstimate.MPUserID, "FirstName") + " " + MemberProtect.User.GetDataItem(IARequestEstimate.MPUserID, "LastName") %></span>
            </div>
            <div class="group">
               <label>Estimate Sent:</label>
               <span class="output"><%= string.Format("{0:ddd, MMMM dd, yyyy a\\t h:mm tt}", IARequestEstimate.CreatedDateTime) %></span>
            </div>
            <div class="group">
               <label>Estimate Requested:</label>
               <span class="output"><%= IARequestEstimate.IARequest.EstimateRequested %></span>
            </div>

            <% if (IARequestEstimate.IsPaymentRequired) { %>
               <div class="group">
                  <label>Requires Payment:</label>
                  <span class="output">Yes</span>
               </div>
               <div class="group">
                  <label>Amount Due:</label>
                  <span class="output"><%= string.Format("{0:c}", IARequestEstimate.Charge) %></span>
               </div>
            <% } else { %>
               <div class="group">
                  <label class="stacked">Requires Payment:</label>
                  <span class="output">No</span>
               </div>
            <% } %>
            

            <div class="group">
               <label>Estimate Approved:</label>
               <% if (IARequestEstimate.IsApproved) { %>
               <span class="output"><%= string.Format("{0:ddd, MMMM dd, yyyy a\\t h:mm tt}", IARequestEstimate.ApprovedDateTime) %></span>
               <% } else { %>
               <span class="output">Not Yet Approved</span>
               <% } %>
            </div>
            
            <div class="group">
               <label>Pre-Authorized Payment:</label>
               <span class="output"><%= IARequestEstimate.IARequest.IACustomerCreditCardID > 0 ? "Yes, " + GetCardAlias() : "No" %></span>
            </div>

            <% if (IARequestEstimate.IAOrderID > 0) { %>
               <div class="group">
                  <label>Paid:</label>
                  <span class="output"><%= string.Format("{0:ddd, MMMM dd, yyyy a\\t h:mm tt}", IARequestEstimate.IAOrder.CreatedDateTime) %></span>
               </div>
               <div class="group">
                  <label>Paid Amount:</label>
                  <span class="output"><%= IARequestEstimate.IAOrder.AuthorizeNetProcessCaptureAmount.ToString("C") %></span>
               </div>
            <% } else { %>
            <div class="group">
               <label>Paid:</label>
               <span class="group">Unpaid</span>
            </div>
            <% } %>

         </fieldset>
      </div>
   </div>
   <div id="footer">
   </div>
</body>
</html>
