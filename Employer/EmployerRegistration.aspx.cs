using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using JobPortalWebApplication.DataBase;

namespace JobPortalWebApplication.Employer
{
    public partial class EmployerRegistration : System.Web.UI.Page
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
            bool emailExists = _dataAccess.CheckIfEmailExists(email);
            if (emailExists)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Email already Exists .Try with another email');", true);

            }
            else
            {
                string otp = GenerateOtp();

                string otpStatus = _dataAccess.UserOTP(otp, email, "EMPLOYER");
                if (otpStatus == "Success")
                {

                    SendEmail(email, otp);
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('OTP sent successfully.');", true);
                    registrationFields.Visible = true;
                    txtEmail.ReadOnly = true;

                    registrationFields.Visible = true; // Show registration fields
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Error in sending OTP..');", true);
                    registrationFields.Visible = false; // do not Show registration fields
                }
            }
        }
        protected void cvTerms_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Check if the RadioButton for "Yes" is selected
            args.IsValid = rbtnYes.Checked;
        }

        protected void cvTermsConditions_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = rbtnYes.Checked;
        }

        protected void EmployerLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployerLogin.aspx");
        }
        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            try
            {



                string AESKey = ConfigurationManager.AppSettings["AESKey"];
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
                    return;
                }
                if (string.IsNullOrEmpty(companyName))
                {
                    lblStatus.Text = "Company Name is required.";
                    return;
                }
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                {
                    lblStatus.Text = "Password and Confirm Password are required.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
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
                    return;
                }
                if (!TermsandConditions)
                {
                    lblStatus.Text = "You must agree to the terms and conditions to register.";
                    return;
                }
                string encryptedPassword = EncryptString(AESKey, password);


                var request = new EmployeeRegister
                {
                    OTP = Convert.ToInt32(OTP),
                    CompanyName = companyName,
                    Password = password,
                    PhoneNumber = mobileNumber,
                    AgreeTotermsAndConditions = TermsandConditions,
                    Email = txtEmail.Text // Use the verified email
                };

                string message = _dataAccess.EmployeeRegister(request, encryptedPassword);
                Response response = new Response();
                response.Message = message;
                if (response != null && response.Message.Contains("Registered successfully"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", "alert('Registration successful.'); window.location.href='EmployerLogin.aspx';", true);

                    //lblStatus.Text = "Registration successful.";
                    //lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", "alert('Registration Failed.'); window.location.href='EmployerRegistration.aspx';", true);

                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
            }
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
            msg.CC.Add("greeshma@blueblocks.net");
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