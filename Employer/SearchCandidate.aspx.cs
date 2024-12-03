using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;

using JobPortalWebApplication.Models.Response;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class SearchCandidate : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        private List<SkillList> skillLists = new List<SkillList>();
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess=Global.DataAccess;
            if (Session["EmployeeID"] != null)
            {
                int EmployeeId = (int)Session["EmployeeID"];
                if (!IsPostBack)
                {
                    BindCoreSkills();
                    BindSoftSkills();
                }
            }
        }
       
          private void BindCoreSkills()
        {
            var coreskill = _dataAccess.GetCoreSkills();
            ddlCoreSkill.DataSource = coreskill;
            ddlCoreSkill.DataTextField = "CoreSkills";
            ddlCoreSkill.DataValueField = "Id";
            ddlCoreSkill.DataBind();
            ddlCoreSkill.Items.Insert(0, new ListItem("Select  Core Skills", ""));
        }
        private void BindSoftSkills()
        {
            var softskill = _dataAccess.GetSoftSkills();
            ddlSoftSkill.DataSource = softskill;
            ddlSoftSkill.DataTextField = "SoftSkills";
            ddlSoftSkill.DataValueField = "Id";
            ddlSoftSkill.DataBind();
            ddlSoftSkill.Items.Insert(0, new ListItem("Select Soft Skills", ""));
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int EmployeeId = 0;
            if (Session["EmployeeID"] != null)
            {
                 EmployeeId = (int)Session["EmployeeID"];
            }
                int cskillId = 0; int sskillId = 0;
            var searchrequest = new CandidateSearchRequest();
            List<CandidateDetails> candidates = new List<CandidateDetails>();
           
            if (ddlCoreSkill.SelectedValue!=null && (int.TryParse(ddlCoreSkill.SelectedValue, out int Skillid)))
            {
                cskillId = Skillid;
            }
            if (ddlSoftSkill.SelectedValue != null && (int.TryParse(ddlSoftSkill.SelectedValue, out int SSkillid)))
            {
                sskillId = SSkillid;
            }
            int max_percent = 0;

            if (int.TryParse(txtmaxskillpercent.Text, out int Max_percent))
            {
                max_percent = Max_percent;
            }
            int min_percent = 0;

            if (int.TryParse(txtminskillpercent.Text, out int Min_percent))
            {
                min_percent = Min_percent;
            }
            searchrequest = new CandidateSearchRequest
            {
                SkillId = cskillId!=0? cskillId: sskillId,
                MaxSkillPercentage = Convert.ToDouble(max_percent),
                MinSkillPercentage = min_percent,
                Core = cskillId != 0 ? true : false,
            };
           // candidates = _dataAccess.GetCandidatesbasedonSkills(searchrequest);
            Session["CandidateSearchResults"] = searchrequest;

            Session["EmployeeID"] = EmployeeId;
            Response.Redirect("CandidateSearchResults.aspx");

        }
    }
}