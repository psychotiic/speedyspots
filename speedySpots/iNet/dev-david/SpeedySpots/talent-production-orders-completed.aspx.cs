using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;
using SmartXLS;

namespace SpeedySpots
{
    public partial class talent_production_orders_completed : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                m_grdList.PageSize = ApplicationContext.GetGridPageSize();

                m_dtFrom.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0, 0);
                m_dtTo.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month), 23, 59, 59, 999);
            }
        }

        protected void OnFilter(object sender, EventArgs e)
        {
            if(m_dtFrom.SelectedDate == null)
            {
                m_dtFrom.SelectedDate = m_dtFrom.MinDate;
            }

            if(m_dtTo.SelectedDate == null)
            {
                m_dtTo.SelectedDate = m_dtFrom.MaxDate;
            }

            m_grdList.Rebind();
        }

        protected void OnDownload(object sender, EventArgs e)
        {
            WorkBook oWorkbook = new WorkBook();
            oWorkbook.setSheetName(0, string.Format("{0} {1} Report", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName")));

            // Merge the title rows for better formatting
            RangeStyle rangeStyle = oWorkbook.getRangeStyle(0, 0, 0, 2);
            rangeStyle.MergeCells = true;
            oWorkbook.setRangeStyle(rangeStyle, 0, 0, 0, 2);

            rangeStyle = oWorkbook.getRangeStyle(1, 0, 1, 2);
            rangeStyle.MergeCells = true;
            oWorkbook.setRangeStyle(rangeStyle, 1, 0, 1, 2);

            oWorkbook.setText(0, 0, string.Format("{0} {1} Report", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName")));
            oWorkbook.setText(1, 0, string.Format("Reporting: {0:d} - {1:d}", m_dtFrom.SelectedDate, m_dtTo.SelectedDate));
            oWorkbook.setText(3, 0, "Job #");
            oWorkbook.setText(3, 1, "Production Order");
            oWorkbook.setText(3, 2, "Spot Title");
            oWorkbook.setText(3, 3, "Due Date");
            oWorkbook.setText(3, 4, "Length");
            oWorkbook.setText(3, 5, "Actual Time");
            oWorkbook.setText(3, 6, "Fee");
            oWorkbook.setText(3, 7, "Fee Type");
            oWorkbook.setText(3, 8, "Sent to QC");
            oWorkbook.setText(3, 9, "QC Marked Complete");

            // Format the title
            RangeStyle oRangeStyle = oWorkbook.getRangeStyle();
            oRangeStyle = oWorkbook.getRangeStyle();
            oRangeStyle.FontSize = 320; // 20 * [actual pixel size]
            oRangeStyle.FontBold = true;
            oWorkbook.setRangeStyle(oRangeStyle, 0, 0, 0, 0);

            // Format the header
            oRangeStyle = oWorkbook.getRangeStyle();
            oRangeStyle.FontSize = 240;
            oRangeStyle.FontBold = true;
            oRangeStyle.BottomBorder = 1;
            oWorkbook.setRangeStyle(oRangeStyle, 3, 0, 3, 9);

            int iRow = 4;
            IQueryable<fn_Talent_GetProductionOrdersResult> oResults = GetCompletedJobs();

            oResults = oResults.OrderBy(row => row.QCJobMarkedCompletDateTime).ThenBy(row => row.IAProductionOrderID);

            foreach(fn_Talent_GetProductionOrdersResult oResult in oResults)
            {
                IAProductionOrder oIAProductionOrder = DataAccess.IAProductionOrders.SingleOrDefault(row => row.IAProductionOrderID == oResult.IAProductionOrderID);
                if(oIAProductionOrder != null)
                {
                    foreach(IASpot oIASPot in oIAProductionOrder.IASpots)
                    {
                        oWorkbook.setText(iRow, 0, oResult.JobNumber);
                        oWorkbook.setText(iRow, 1, oIAProductionOrder.IAJob.Name);
                        oWorkbook.setText(iRow, 2, oIASPot.Title);
                        oWorkbook.setText(iRow, 3, oIASPot.DueDateTime.ToString("g"));
                        oWorkbook.setText(iRow, 4, oIASPot.Length);
                        //oWorkbook.setText(iRow, 5, oIASPot.LengthActual);
                        
                        oWorkbook.setText(iRow, 8, oIASPot.CompletedDateTime.ToString("g"));
                        oWorkbook.setText(iRow, 9, (oResult.QCJobMarkedCompletDateTime != new DateTime(1950, 1, 1)) ? oResult.QCJobMarkedCompletDateTime.ToString("g") : "");

                        foreach(IASpotFee oIASpotFee in oIASPot.IASpotFees)
                        {
                            oWorkbook.setText(iRow, 5, oIASpotFee.LengthActual);
                            oWorkbook.setNumber(iRow, 6, (double)oIASpotFee.Fee);
                            oWorkbook.setText(iRow, 7, oIASpotFee.IASpotFeeType.Name);
                            iRow++;
                        }
                    }

                    iRow++;
                }
            }

            iRow--;
            
            // Add in the total fees value
            oWorkbook.setText(iRow, 5, "Total Fees:");
            string sumFunction = string.Format("SUM(G5:G{0})", iRow);
            oWorkbook.setFormula(iRow, 6, sumFunction);

            // Set the fee column to format as money
            oRangeStyle = oWorkbook.getRangeStyle();
            oRangeStyle.FontSize = 200;
            oRangeStyle.FontBold = false;
            oRangeStyle.CustomFormat = "$#,##0.00";
            oWorkbook.setRangeStyle(oRangeStyle, 4, 6, iRow, 6);

            // Format the total fees section
            oRangeStyle.FontBold = true;
            oRangeStyle.TopBorder = 2;
            oWorkbook.setRangeStyle(oRangeStyle, iRow, 5, iRow, 6);
            

            // Auto-size the columns so everything looks nice
            oWorkbook.setColWidth(0, 2820);
            oWorkbook.setColWidthAutoSize(1, true);
            oWorkbook.setColWidthAutoSize(2, true);
            oWorkbook.setColWidthAutoSize(3, true);
            oWorkbook.setColWidthAutoSize(4, true);
            oWorkbook.setColWidthAutoSize(5, true);
            oWorkbook.setColWidthAutoSize(6, true);
            oWorkbook.setColWidthAutoSize(7, true);
            oWorkbook.setColWidthAutoSize(8, true);
            oWorkbook.setColWidthAutoSize(9, true);

            // Send to client
            MemoryStream oMemoryStream = new MemoryStream();
            oWorkbook.write(oMemoryStream);

            byte[] oExcelBuffer = oMemoryStream.ToArray();

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=TalentReport.xls");
            Response.BinaryWrite(oExcelBuffer);
            Response.End();
        }

        private IQueryable<fn_Talent_GetProductionOrdersResult> GetCompletedJobs()
        {
            DateTime defaultCompletedDate = new DateTime(1950, 1, 1);
            IQueryable<fn_Talent_GetProductionOrdersResult> oResults = DataAccess.fn_Talent_GetProductionOrders(MemberProtect.CurrentUser.UserID).Where(row => row.QCJobMarkedCompletDateTime > defaultCompletedDate);

            // Apply filter
            if (m_dtFrom.SelectedDate != null && m_dtTo.SelectedDate != null)
            {
                oResults = oResults.Where(row => row.QCJobMarkedCompletDateTime.CompareTo(m_dtFrom.SelectedDate) >= 0 && row.QCJobMarkedCompletDateTime.CompareTo(m_dtTo.SelectedDate) <= 0);
            }

            return oResults;
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            m_grdList.DataSource = GetCompletedJobs();
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;
            }
        }

        #endregion

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Talent);

            return oAccessControl;
        }
        #endregion
    }
}