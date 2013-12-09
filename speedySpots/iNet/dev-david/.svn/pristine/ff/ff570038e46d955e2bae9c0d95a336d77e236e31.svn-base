using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SpeedySpots.Controls
{
    /// <summary>
    /// This is a simple enhancement of the existing LinkButton that allows it to work as a Form Default Button in both IE and Firefox (the normal one does
    /// not work in Firefox, but this does).
    /// </summary>
    public class InetLinkButton : LinkButton
    {
        protected override void OnLoad(System.EventArgs e)
        {
            string script = String.Format(m_sAddClickScript, ClientID);
            Page.ClientScript.RegisterStartupScript(GetType(), "addClickFunctionScript", m_sAddClickFunctionScript, true);
            Page.ClientScript.RegisterStartupScript(GetType(), "click_" + ClientID, script, true);

            base.OnLoad(e);
        }

        private const string m_sAddClickScript = "addClickFunction('{0}');";

        private const string m_sAddClickFunctionScript =
            @"  function addClickFunction(id) {{
            var b = document.getElementById(id);
            if (b && typeof(b.click) == 'undefined') b.click = function() {{
                var result = true; if (b.onclick) result = b.onclick();
                if (typeof(result) == 'undefined' || result) {{ eval(b.getAttribute('href')); }}
            }}}};";
    }
}
