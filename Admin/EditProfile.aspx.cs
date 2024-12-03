using JobPortalWebApplication.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class EditProfile : System.Web.UI.Page
    {
        public DataAccess dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            dataAccess = Global.DataAccess;
            if (Session["AdminID"] != null)
            {
                int adminId = 0;
                adminId = (int)Session["AdminID"];

                if (!IsPostBack)
                {
                    LoadUserProfile(adminId);
                }
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        private void LoadUserProfile(int adminId)
        {
            var data = dataAccess.LoadAdmin(adminId);
            txtEmail.Text = data.Email;
            txtuseranme.Text = data.Username;

        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int adminId = 0;
                adminId = (int)Session["AdminID"];
                string email = txtEmail.Text;
                string name = txtName.Text;
                string username = txtuseranme.Text;
                var edit = dataAccess.UpdateAdmin(email, username,name, adminId);
                if (edit == "success")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Profile updated successfully'); window.location='AdminDashboard.aspx'", true);

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Profile updation failed.'); window.location='AdminDashboard.aspx' ", true);

                }
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDashboard.aspx"); // Redirect to the previous page or wherever appropriate
        }
    }
}
