using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Candidate
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMessages();
            }
        }
        private int GetCandidateId()
        {
            int candidateId = 0;
            if (Session["CandidateID"] != null)
            {
                candidateId = (int)Session["CandidateID"];
            }
            else if (Request.QueryString["CandidateID"] != null)
            {
                int.TryParse(Request.QueryString["CandidateID"], out candidateId);
            }
            return candidateId;
        }
        private void LoadMessages()
        {
            //int currentUserId = GetCandidateId();  // Get current user's ID (candidate or employer)
            //    // Get other user's ID (employer or candidate)

            //using (SqlConnection conn = new SqlConnection(_c))
            //{
            //    string query = "SELECT u.UserName AS SenderName, m.MessageContent, m.DateSent " +
            //                   "FROM Messages m " +
            //                   "INNER JOIN Candidate u ON m.SenderID = u.CandidateID " +
            //                   "WHERE (SenderID = @CurrentUserID AND ReceiverID = @OtherUserID) " +
            //                   "OR (SenderID = @OtherUserID AND ReceiverID = @CurrentUserID) " +
            //                   "ORDER BY DateSent ASC";

            //    SqlCommand cmd = new SqlCommand(query, conn);
            //    cmd.Parameters.AddWithValue("@CurrentUserID", currentUserId);
            //    cmd.Parameters.AddWithValue("@OtherUserID", otherUserId);

               // conn.Open();
              //  SqlDataReader reader = cmd.ExecuteReader();
                //rptMessages.DataSource = reader;
                //rptMessages.DataBind();
          //  }
        }
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            int senderID = GetCandidateId();    // Current user (candidate or employer)
           // int receiverID = txtReceiver.Text;     // Receiver user ID (employer or candidate)
            string messageContent = txtMessage.Text;

            if (!string.IsNullOrEmpty(messageContent))
            {
                using (SqlConnection conn = new SqlConnection("YourConnectionString"))
                {
                    string query = "INSERT INTO Messages (SenderID, ReceiverID, MessageContent, DateSent) " +
                                   "VALUES (@SenderID, @ReceiverID, @MessageContent, GETDATE())";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SenderID", senderID);
                  //  cmd.Parameters.AddWithValue("@ReceiverID", receiverID);
                    cmd.Parameters.AddWithValue("@MessageContent", messageContent);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Clear the message box and reload messages
                txtMessage.Text = string.Empty;
                LoadMessages();
            }
        }

        protected void btnOpenMessages_Click(object sender, EventArgs e)
        {
            // Call JavaScript to open the modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal", "$('#messageModal').modal('show');", true);
        }

    }
}