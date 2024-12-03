using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Employer;
using Org.BouncyCastle.Asn1.Cmp;

namespace JobPortalWebApplication.Candidate
{
    public partial class ViewMessage : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected int SelectedEmployerId
        {
            get
            {
                return ViewState["SelectedEmployerId"] != null ? (int)ViewState["SelectedEmployerId"] : 0;
            }
            set
            {
                ViewState["SelectedEmployerId"] = value;
            }
        }

       
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;

            if (!IsPostBack)
            {
                // Determine if the user is an employer or candidate
                int userId = Convert.ToInt32(Session["UserID"]);  // Assuming UserID is stored in Session
               // string userType = Session["UserType"].ToString(); // "Employer" or "Candidate"
                LoadMessages(userId);
                //if (userType == "Employer")
                //{
                   //LoadMessagesForEmployer(userId);
                //}
                //else if (userType == "Candidate")
                //{
                //    LoadMessagesForCandidate(userId);
                //}
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
            SelectedEmployerId = employerId;
            int userId = Convert.ToInt32(Session["UserID"]);
            // Load conversation for the selected employer
            LoadConversation(employerId,userId);
        }
        private void LoadConversation(int employerId,int userid)
        {
            lblSelectedEmployer.Text = "Conversation with Employer " + employerId;
            DataTable messages=_dataAccess.LoadConversation(employerId,userid);
            ConversationRepeater.DataSource = messages;
            ConversationRepeater.DataBind();

        }
        //    private void LoadMessagesForEmployer(int employerId)
        //{
        //    // Fetch both sent and received messages for the employer
        //    DataTable receivedMessages = GetReceivedMessages(employerId, "Employer");
        //    //DataTable sentMessages = GetSentMessages(employerId, "Employer");

        //    // Bind messages to the respective GridViews
        //    MessagesRepeater.DataSource = receivedMessages;
        //    MessagesRepeater.DataBind();

        //    //gvSentMessages.DataSource = sentMessages;
        //    //gvSentMessages.DataBind();
        //}

        //private void LoadMessagesForCandidate(int candidateId)
        //{
        //    // Fetch both sent and received messages for the candidate
        //    DataTable receivedMessages = GetReceivedMessages(candidateId, "Candidate");
        //    DataTable sentMessages = GetSentMessages(candidateId, "Candidate");

        //    // Bind messages to the respective GridViews
        //    gvReceivedMessages.DataSource = receivedMessages;
        //    gvReceivedMessages.DataBind();

        //    gvSentMessages.DataSource = sentMessages;
        //    gvSentMessages.DataBind();
        //}

        private DataTable GetReceivedMessages(int userId, string userType)
        {
            DataTable message=_dataAccess.GetReceivedMessages(userId);
            return message;
           
        }

        private DataTable GetSentMessages(int userId, string userType)
        {
            DataTable message = _dataAccess.GetSentMessages(userId);
            return message;
        }
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (SelectedEmployerId > 0)
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                // Get the current logged-in user ID
                int receiverId = SelectedEmployerId; // Get the ID of the person the message is being sent to
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

    }
}