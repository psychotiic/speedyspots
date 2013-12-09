using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.DataAccess;
using SpeedySpots.Objects;
using System.Web.Script.Serialization;

namespace SpeedySpots
{
    public partial class ajax_company_lookup : AjaxBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string json = "[]";

            if (IsAuthenticated && ApplicationContext.IsStaff)
            {
                if (GetSearchMode() == "F")
                    json = DoFullCompanyLookup();
                else
                    json = DoSimpleCompanySearch();

            }

            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
        }

        private string GetSearchMode()
        {
            string mode = "S";

            if (!string.IsNullOrEmpty(Request.QueryString["t"]))
                mode = (Request.QueryString["t"].ToString().ToUpper() == "F") ? "F" : "S";

            return mode;
        }

        

        private string DoSimpleCompanySearch()
        {
            string searchTerm = GetUserSearchTerm();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                List<SimpleOrg> orgs = GetListOfSimpleOrgs(searchTerm);
                return GetJsonFromListOfSimpleOrgs(orgs);
            }

            return "{\"results\":[]}";
        }

        private string GetUserSearchTerm()
        {
            string userSearchTerm = string.Empty;

            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                userSearchTerm = Request.QueryString["q"];
            }

            return userSearchTerm;
        }

        private List<SimpleOrg> GetListOfSimpleOrgs(string searchTerm)
        {
            if (searchTerm.Length >= 2)
            {
                var query = from o in DataAccess.MPOrgs
                            join d in DataAccess.MPOrgDatas
                            on o.MPOrgID equals d.MPOrgID into JoinedOrgData
                            from d in JoinedOrgData.DefaultIfEmpty()
                            orderby o.Name
                            where o.Name.StartsWith(searchTerm) && d.IsArchived != "Y"
                            select new SimpleOrg { id = o.MPOrgID, name = o.Name, City = d.City, State = d.State, IsVerified = d.IsVerified };

                return query.Take(50).ToList();
            }
            else
            {
                return new List<SimpleOrg>();
            }
        }

        private string GetJsonFromListOfSimpleOrgs(List<SimpleOrg> orgs)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(new { results = orgs });
        }

        private string DoFullCompanyLookup()
        {
            Guid companyID = GetCompanyID();

            if (companyID != Guid.Empty)
            {
                FullOrg org = GetFullOrg(companyID);
                return GetJsonFromFromOrg(org);
            }

            return "[]";
        }

        private Guid GetCompanyID()
        {
            Guid companyID = Guid.Empty;

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                companyID = new Guid(Request.QueryString["id"]);
            }

            return companyID;
        }

        private FullOrg GetFullOrg(Guid companyID)
        {
            if (companyID != Guid.Empty)
            {
                var query = from o in DataAccess.MPOrgs
                            join d in DataAccess.MPOrgDatas
                            on o.MPOrgID equals d.MPOrgID into JoinedOrgData
                            from d in JoinedOrgData.DefaultIfEmpty()
                            orderby o.Name
                            where o.MPOrgID.Equals(companyID)                    
                            select new FullOrg { ID = o.MPOrgID, 
                                Name = o.Name, 
                                Address1 = d.Address1,
                                Address2 = d.Address2,
                                City = d.City, 
                                Country = d.Country,
                                State = d.State, 
                                Zip = d.Zip,
                                Phone = d.Phone,
                                BillingAddress1 = d.BillingAddress1,
                                BillingAddress2 = d.BillingAddress2,
                                BillingCity = d.BillingCity, 
                                BillingState = d.BillingState,
                                BillingCountry = d.BillingCountry,
                                BillingZip = d.BillingZip,
                                BillingEmail = d.EmailInvoice,
                                IsVerified = d.IsVerified };

                return query.SingleOrDefault();
            }
            
            return null;    
        }

        private string GetJsonFromFromOrg(FullOrg org)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(org);
        }

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Staff);
            oAccessControl.Add(AccessControl.Admin);

            return oAccessControl;
        }
        #endregion
    }

    public class SimpleOrg
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string IsVerified { get; set; }
    }

    public class FullOrg
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }

        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingCountry { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string BillingEmail { get; set; }

        public string IsVerified { get; set; }
    }
}