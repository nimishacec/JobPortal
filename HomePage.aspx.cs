using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // PopulateDropDownList();
            }
        }

        private void PopulateDropDownList()
        {
            DropDownList1.Items.Add(new ListItem("Candidate", "1"));
            DropDownList1.Items.Add(new ListItem("Employer", "2"));
            DropDownList1.Items.Add(new ListItem("Trainer", "3"));
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = DropDownList1.SelectedValue;
            if (!string.IsNullOrEmpty(selectedValue))
            {
                Response.Redirect(selectedValue);
            }
        }
    }
}