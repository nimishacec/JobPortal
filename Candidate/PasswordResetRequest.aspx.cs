using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JobPortalWebApplication.DataBase;

namespace JobPortalWebApplication.Candidate
{
    public partial class PasswordResetRequest : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
        }
        protected void btnRequestReset_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                //  string userType = GetUserTypeByEmail(email);

                //if (userType != null)
                //{
                // Generate a unique token
                string token = Guid.NewGuid().ToString();
                DateTime expiryTime = DateTime.Now.AddHours(1); // Token valid for 1 hour

                var pwdreset = _dataAccess.RequestResetPassword(token, email, expiryTime, "CANDIDATE");
                // Save token, email, and expiration in the database
                // Example query (use parameterized queries to prevent SQL injection)
                // INSERT INTO PasswordResetRequests (Email, Token, ExpirationTime) VALUES (@Email, @Token, @ExpiryTime)

                // Send the reset email
                string resetLink = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/Candidate/ResetPassword.aspx?token={token}";
                SendResetEmail(email, resetLink);

                // Notify the user
                lblMessage.Text = "A password reset link has been sent to your email.";
                lblMessage.Visible = true;
                string absoluteUrl = ResolveUrl("~/HomePage.aspx");
                string script = $@"
    setTimeout(function() {{
        window.location.href = '{absoluteUrl}';
    }}, 3000);"; // 3000 milliseconds = 3 seconds

                // Register the script using ScriptManager
                ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", script, true);

            }
        }

        private void SendResetEmail(string email, string resetLink)
        {
            string subject = "Password Reset Request";
            string body = $"Please click the following link to reset your password: <a href='{resetLink}'>Reset Password</a>";
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("m.nimi12@gmail.com");
            msg.To.Add("nimisha@blueblocks.net");
            // msg.CC.Add("nimisha@blueblocks.net");
            //msg.CC.Add("pradeep@blueblocks.net");
            msg.Subject = subject;
            msg.Body = body;
            ;
            msg.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smt = new System.Net.Mail.SmtpClient();
            smt.Host = "smtp.gmail.com";
            System.Net.NetworkCredential ntwd = new NetworkCredential();
            ntwd.UserName = "m.nimi12@gmail.com"; //Your Email ID  
            ntwd.Password = "qzwopxjewipmjweq"; // Your Password  
            smt.UseDefaultCredentials = false;
            smt.Credentials = ntwd;
            smt.Port = 587;
            smt.EnableSsl = true;
            smt.Send(msg);

            //MailMessage mail = new MailMessage("m.nimi12@gmail.com", "nimisha@blueblocks.net");
            //mail.Subject = subject;
            //mail.Body = body;
            //mail.IsBodyHtml = true;

            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            //smtpClient.Credentials = new NetworkCredential("m.nimi12@gmail.com", "qzwopxjewipmjweq");
            //smtpClient.UseDefaultCredentials = false;            
            //smtpClient.Port = 587;
            //smtpClient.EnableSsl = true;
            //smtpClient.Send(mail);
        }


    }
}