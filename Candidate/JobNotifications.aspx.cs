using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Candidate
{
    public partial class JobNotifications : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int candidateId = 0;
            if (Session["CandidateID"] != null)
            {
                candidateId = (int)Session["CandidateID"];

                if (!IsPostBack)
                {
                    LoadNotifications(candidateId);
                }
            }

            else
            {
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        //public  void ProcessJobAlerts()
        //{

        //    int candidateId = 0;
        //    if (Session["CandidateID"] != null)
        //    {
        //        candidateId = (int)Session["CandidateID"];
        //    }

        //        List<ViewJobs> viewJobs = _dataAccess.GetJobNotifications(candidateId);

        //    if (viewJobs != null && viewJobs.Count > 0)
        //    {
        //        string emailBody = "<h2>New Job Alerts Based on Your Preferences</h2><p>Dear Candidate,</p><p>You have new job alerts based on your preferences. Here are the details:</p><table><thead><tr><th>Job Title</th><th>Company Name</th><th>Location</th><th>Salary</th></tr></thead><tbody>";

        //        foreach (var job in viewJobs)
        //        {
        //            emailBody += $"<tr><td>{job.JobTitle}</td><td>{job.CompanyName}</td><td>{job.JobLocation}</td><td>{job.Salary:C}</td></tr>";
        //        }

        //        emailBody += "</tbody></table><p>Please check your dashboard for more details.</p>";

        //        // Send the email
        //        string candidateEmail = "candidate@example.com"; // Replace with actual candidate email
        //        string emailSubject = "New Job Alerts";
        //        SendJobAlertEmail(candidateEmail, emailSubject, emailBody);
        //    }
        //}
        private void LoadNotifications(int candidateId)
        {
            List<ViewJobs> viewJobs = _dataAccess.GetJobNotifications(candidateId);

            notificationsRepeater.DataSource = viewJobs;
            notificationsRepeater.DataBind();
        }
        public void SendJobAlertEmail(string candidateEmail, string emailSubject, string emailBody)
        {

            candidateEmail = "nimisha@blueblocks.net";
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("m.nimi12@gmail.com");
            msg.To.Add(candidateEmail);
            //msg.CC.Add("nimisha@blueblocks.net");
            //msg.CC.Add("pradeep@blueblocks.net");
            msg.Subject =emailSubject;
            msg.Body = emailBody;
            msg.IsBodyHtml = true;

            SmtpClient smt = new SmtpClient();
            smt.Host = "smtp.gmail.com";
            System.Net.NetworkCredential ntwd = new NetworkCredential();
            ntwd.UserName = "m.nimi12@gmail.com"; //Your Email ID  
            ntwd.Password = "qzwopxjewipmjweq"; // Your Password  
            smt.UseDefaultCredentials = false;
            smt.Credentials = ntwd;
            smt.Port = 587;
            smt.EnableSsl = true;
            smt.Send(msg);
            //string result = otp;
            //return result;

        }
    }
}
