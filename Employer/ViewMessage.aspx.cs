using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JobPortalWebApplication.DataBase;
using System.Configuration;

namespace JobPortalWebApplication.Employer
{
    public partial class ViewMessage : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected int SelectedCandidateId
        {
            get
            {
                return ViewState["SelectedCandidateId"] != null ? (int)ViewState["SelectedCandidateId"] : 0;
            }
            set
            {
                ViewState["SelectedCandidateId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess=Global.DataAccess;
            if (!IsPostBack)
            {
                // Bind Received and Sent messages separately on page load
                int userId = Convert.ToInt32(Session["EmployeeID"]);  // Assuming UserID is stored in Session
                                                                  // string userType = Session["UserType"].ToString(); // "Employer" or "Candidate"
                LoadMessages(userId);
                // BindSentMessages(userId);
            }
        }
        private void LoadMessages(int userid)
        {
            DataTable messagesTable = new DataTable();
            messagesTable = _dataAccess.LoadAllMessages(userid);
            MessagesListRepeater.DataSource = messagesTable;
            MessagesListRepeater.DataBind();
        }

        protected void MessageSelected(object source, RepeaterCommandEventArgs e)
        {
            int employerId = Convert.ToInt32(e.CommandArgument);
           // SelectedEmployerId = employerId;
            int userId = Convert.ToInt32(Session["UserID"]);
            // Load conversation for the selected employer
            LoadConversation(employerId, userId);
        }
        private void LoadConversation(int employerId, int userid)
        {
          //  lblSelectedEmployer.Text = "Conversation with Employer " + employerId;
            DataTable messages = _dataAccess.LoadConversation(employerId, userid);
            ConversationRepeater.DataSource = messages;
            ConversationRepeater.DataBind();

        }
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (SelectedCandidateId > 0)
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                // Get the current logged-in user ID
                int receiverId = SelectedCandidateId; // Get the ID of the person the message is being sent to
                string messageContent = txtMessageContent.Text;

                bool message = _dataAccess.SendMessages(userId, receiverId, messageContent);

                if (message)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Message sent successfully!');", true);

                    //lblStatus.Text = "Message sent successfully!";
                    LoadConversation(receiverId, userId);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Message sending failed!');", true);

                    // lblStatus.Text = "Message sending failed!";
                }
            }
        }

        //private void BindSentMessages(int userId)
        //{
        //    DataTable message = _dataAccess.GetSentMessages(userId);
        //   // return message;

        //    SentMessagesRepeater.DataSource = message;
        //            SentMessagesRepeater.DataBind();


        //}




    }
}