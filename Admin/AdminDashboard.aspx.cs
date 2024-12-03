using JobPortalWebApplication.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        public DataAccess dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            dataAccess = Global.DataAccess;
            if (Session["AdminID"] != null)
            {
                int adminId = Convert.ToInt32(Session["AdminID"]);
                AdminDetails(adminId);
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }

        }
        public void AdminDetails(int adminId)
        {
            var data = dataAccess.LoadAdmin(adminId);
            if (data != null)
            {
                txtname.Text = data.Name != null ? data.Name : null;
                txtusername.Text = data.Username != null ? data.Username : null;
                txtemail.Text = data.Email != null ? data.Email : null;
            }
        }
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx");
        }
    }
}