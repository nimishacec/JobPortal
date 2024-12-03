using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Candidate
{
    public partial class StatusPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["StatusMessage"] != null)
                {
                    lblStatusMessage.Text = Session["StatusMessage"].ToString();
                }
               else if (Session["StatusMessage1"] != null)
                {
                    Label1.Text = Session["StatusMessage1"].ToString();
                }
                string returnUrl = Request.QueryString["returnUrl"];
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    // Use a server-side redirect with a delay
                    string script = $"setTimeout(function(){{ window.location.href = '{HttpUtility.UrlDecode(returnUrl)}'; }}, 5000);";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
            }
        }
    }
}