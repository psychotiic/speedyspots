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
    public partial class ajax_user_lookup : AjaxBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             Response.Clear();
            string json = "[]";

            if (IsAuthenticated && ApplicationContext.IsStaff)
            {
                json = DoSimpleUserSearch();
            }

            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
        }


        private string DoSimpleUserSearch()
        {
            string searchTerm = GetUserSearchTerm();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                List<SimpleUser> users = GetListOfSimpleUsers(searchTerm);
                return GetJsonFromListOfSimpleUsers(users);
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

        private List<SimpleUser> GetListOfSimpleUsers(string searchTerm)
        {
            if (searchTerm.Length >= 2)
            {
                var query = from u in DataAccess.MPUsers
                            join d in DataAccess.MPUserDatas
                            on u.MPUserID equals d.MPUserID into JoinedUserData
                            from d in JoinedUserData.DefaultIfEmpty()
                            orderby d.FirstName, d.LastName
                            where (d.FirstName.StartsWith(searchTerm) || d.LastName.StartsWith(searchTerm) || u.Username.StartsWith(searchTerm)) && d.IsArchived == "N"
                            select new SimpleUser { id = u.MPUserID, Username = u.Username, name = d.FirstName + " " + d.LastName, FName = d.FirstName, LName = d.LastName };

                return query.Take(50).ToList();
            }
            else
            {
                return new List<SimpleUser>();
            }
        }

        private string GetJsonFromListOfSimpleUsers(List<SimpleUser> users)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(new { results = users });
        }

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);
            oAccessControl.Add(AccessControl.Staff);

            return oAccessControl;
        }
        #endregion
    }

    public class SimpleUser
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string Username { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}