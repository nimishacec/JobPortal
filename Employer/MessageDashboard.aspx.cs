using JobPortalWebApplication.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class MessageDashboard : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess=Global.DataAccess;
            if (Session["EmployeeID"] != null)
            {
                int EmployeeId = (int)Session["EmployeeID"];
            }

            }
        private int GetEmployeeId()
        {
            int EmployeeId = 0;
            if (Session["EmployeeID"] != null)
            {
                EmployeeId = (int)Session["EmployeeID"];
            }
            else if (Request.QueryString["EmployeeID"] != null)
            {
                int.TryParse(Request.QueryString["EmployeeID"], out EmployeeId);
            }
            return EmployeeId;
        }
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            string subject = txtSubject.Text;
            string messageBody = txtMessage.Text;
            int employerID = GetEmployeeId();

            int candidateID = Convert.ToInt32(Request.QueryString["CandidateID"]);  // Hidden field storing candidate ID
            int jobID = Convert.ToInt32(Request.QueryString["JobID"]);  // Hidden field storing job ID

            // Option 1: Send Internal Message (Database)
            if (chkSendInternalMessage.Checked)
            {
                bool message = _dataAccess.SendMessages(employerID, candidateID, messageBody);
                if (message)
                {
                    lblStatus.Text = "Message sent successfully!";
                }
                else
                {
                    lblStatus.Text = "Message sending failed!";
                }

                // Option 2: Send Email
                //if (chkSendEmail.Checked)
                //{
                //    string recipientEmail = GetCandidateEmail(candidateID);  // Fetch candidate email
                //    SendEmail(recipientEmail, subject, messageBody);
                //}

                // lblStatus.Text = "Message sent successfully!";
            }
        }

    }
}