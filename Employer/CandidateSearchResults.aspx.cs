using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class CandidateSearchResults : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess=Global.DataAccess;

            if (Session["EmployeeID"] != null)
            {
                int EmployeeId = (int)Session["EmployeeID"];
               
                    if (Session["CandidateSearchResults"] != null)
                    {
                   
                        if (!IsPostBack)
                        {
                            BindCandidateResults();
                        }
                    }                 
                
                    
                    else
                    {
                        Session["StatusMessage"] = "No candidate found for your search";
                        Response.Redirect("StatusPage.aspx");

                    }
                
            }
            else
            {
                Response.Redirect("EmployerLogin.aspx");
            }
        }
        protected void gvCandidates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCandidates.PageIndex = e.NewPageIndex;
            BindCandidateResults(); // Re-bind your GridView after changing the page index
        }
        private void BindCandidateResults()
        {
            var candidates = Session["CandidateSearchResults"] as CandidateSearchRequest; // Method to fetch candidates

            if (candidates != null)
            {
                var result = _dataAccess.GetCandidatesbasedonSkills(candidates);
                if (result.Count() >0)
                {
                    gvCandidates.DataSource = result;
                    gvCandidates.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No candidates found for the search criteria');", true);
                   //Response.Redirect("SearchCandidate.aspx");
                }
            }
        }
    }
}