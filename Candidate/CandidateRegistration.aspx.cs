using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;

namespace JobPortalWebApplication.Candidate
{
    public partial class CandidateRegistration : Page
    {
        private static readonly HttpClient client = new HttpClient();
        public DataAccess _dataAccess;
        private string _AESKey;
        string connectionString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            _AESKey = Global._AESKey;
            if (!IsPostBack)
            {
                 connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                _AESKey = ConfigurationManager.AppSettings["AESKey"];
            }
        }
     
        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
          
            if (string.IsNullOrEmpty(email))
            {
                lblStatus.Text = "Email is required.";
                return;
            }
           bool emailExists=_dataAccess.CheckIfEmailExists(email);
            if (emailExists)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Email already Exists .Try with another email');", true);
            }
            else
            {
                string otp = GenerateOtp();

                string otpStatus = _dataAccess.UserOTP(otp, email, "CANDIDATE");
                if (otpStatus == "Success")
                {

                    SendEmail(email, otp);
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('OTP sent successfully.');", true);
                    //lblStatus.Text = "OTP sent successfully.";
                    //lblStatus.ForeColor = System.Drawing.Color.Green;
                    registrationFields.Visible = true;
                    txtEmail.ReadOnly = true;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Error in sending OTP..');", true);
                    //lblStatus.Text = "Error in sending OTP.";
                    //lblStatus.ForeColor = System.Drawing.Color.Red;
                    //registrationFields.Visible = false; 
                }
            }
        }      
        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string AESKey= ConfigurationManager.AppSettings["AESKey"];
                // Registration logic here
                string OTP= txtOTP.Text;
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string password = txtPassword.Text;
                string confirmPassword = txtConfirmPassword.Text;
                string mobileNumber = txtMobileNumber.Text;

                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                {
                    lblStatus.Text = "Password and Confirm Password are required.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    registrationFields.Visible = true;
                    return;
                }

                if (password != confirmPassword)
                {
                    lblStatus.Text = "Passwords do not match.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (!IsPasswordStrong(password))
                {
                    lblStatus.Text = "Password is not strong enough.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    registrationFields.Visible= true;
                    return;
                }

                string encryptedPassword = EncryptString(AESKey, password);               
                var request = new CandidateRegister
                {
                    OTP= Convert.ToInt32(OTP),
                    FirstName = firstName,
                    LastName = lastName,
                    Password = password,
                    PhoneNumber = mobileNumber,
                    Email = txtEmail.Text // Use the verified email
                };

                string message = _dataAccess.CandidateRegister(request, encryptedPassword);
                Response response = new Response();
                response.Message = message;
                if (response != null && response.Message.Contains("Registered successfully"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Registration successful.'); window.location.href='CandidateLogin.aspx';", true);
                    //lblStatus.Text = "Registration successful.";
                    //lblStatus.ForeColor = System.Drawing.Color.Green;
                    //lblStatus.Visible = true;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Registration failed..');window.location.href='CandidateRegistration.aspx';", true);
                    lblStatus.Text = "Registration failed.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Visible=true;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
                lblStatus.Visible = true;
            }
        }
        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("CandidateLogin.aspx");
        }
            public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public bool IsPasswordStrong(string password)
        {
          
            int minLength = 8;

            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;

            if (password.Length < minLength)
            {
                return false;
            }

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpperCase = true;
                if (char.IsLower(c)) hasLowerCase = true;
                if (char.IsDigit(c)) hasDigit = true;
                if (Regex.IsMatch(c.ToString(), @"[\W_]")) hasSpecialChar = true;
            }

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
      
        private string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999); // Generate a 6-digit OTP
            return otp.ToString();
        }
        public string SendEmail(string email, string otp)
        {

           // email = "sreejith@blueblocks.net";
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("m.nimi12@gmail.com");
            msg.To.Add("nimisha@blueblocks.net");
            //msg.CC.Add("nimisha@blueblocks.net");
            msg.CC.Add("pradeep@blueblocks.net");
            msg.Subject = "Random Password for your Account";
            msg.Body = "One Time Password for JobPortal " + otp;
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
            string result = otp;
            return result;

        }

    }
}