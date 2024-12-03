using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Trainer
{
    public partial class TrainerReg : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        private string _AESKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            _AESKey = Global._AESKey;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            if (string.IsNullOrEmpty(email))
            {
                lblStatus.Text = "Email is required.";
                lblStatus.Visible = true;
                return;
            }
            string otp = GenerateOtp();

            string otpStatus = _dataAccess.UserOTP(otp, email, "EMPLOYER");
            if (otpStatus == "Success")
            {

                SendEmail(email, otp);
                lblStatus.Text = "OTP sent successfully.";
                lblStatus.Visible = true;
                lblStatus.ForeColor = System.Drawing.Color.Green;
                registrationFields.Visible = true; // Show registration fields
            }
            else
            {
                lblStatus.Text = "Error in sending OTP.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Visible = true;
                registrationFields.Visible = false; // do not Show registration fields
            }
        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            try
            {
             
                // Registration logic here
                string OTP = txtOTP.Text;
                string companyName = txtCompanyName.Text;
                string password = txtPassword.Text;
                string confirmPassword = txtConfirmPassword.Text;
                string mobileNumber = txtMobileNumber.Text;
                bool TermsandConditions = rbtnYes.Checked;

                if (string.IsNullOrEmpty(mobileNumber))
                {
                    lblStatus.Text = "Mobile number is required.";
                    lblStatus.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(companyName))
                {
                    lblStatus.Text = "Company Name is required.";
                    lblStatus.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                {
                    lblStatus.Text = "Password and Confirm Password are required.";
                    lblStatus.Visible = true;
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (password != confirmPassword)
                {
                    lblStatus.Text = "Passwords do not match.";

                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Visible = true;
                    return;
                }

                if (!IsPasswordStrong(password))
                {
                    lblStatus.Text = "Password is not strong enough.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Visible = true;
                    return;
                }
                if (!TermsandConditions)
                {
                    lblStatus.Text = "You must agree to the terms and conditions to register.";
                    lblStatus.Visible = true;
                    return;
                }
                string encryptedPassword = EncryptString(_AESKey, password);


                var request = new TrainerRegisterRequest
                {
                    OTP = Convert.ToInt32(OTP),
                    CompanyName = companyName,
                    Password = password,
                    PhoneNumber = mobileNumber,
                    AgreeTotermsAndConditions = TermsandConditions,
                    Email = txtEmail.Text // Use the verified email
                };

                string message = _dataAccess.TrainerRegister(request, encryptedPassword);
                Response response = new Response();
                response.Message = message;
                if (response != null && response.Message.Contains("Registered successfully"))
                {
                    lblStatus.Text = "Registration successful.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    lblStatus.Visible = true;
                }
                else
                {
                    lblStatus.Text = "Registration failed.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
                lblStatus.Visible = true;
            }
        }
        protected void TrainerLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainerLogin.aspx");
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
            // Minimum length of the password
            int minLength = 8;

            // Criteria for password strength
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