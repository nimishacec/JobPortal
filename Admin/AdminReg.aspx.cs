using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class AdminReg : System.Web.UI.Page
    {
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
        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string AESKey = ConfigurationManager.AppSettings["AESKey"];
                // Registration logic here
                
                string firstName = txtFirstName.Text;
                string userName = txtUserName.Text;
                string password = txtPassword.Text;
                string email = txtEmail.Text;
                string confirmPassword = txtConfirmPassword.Text;
                string mobileNumber = txtMobileNumber.Text;

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

                string encryptedPassword = EncryptString(AESKey, password);

                // Call the registration API or method here
                var request = new AdminRegistrationRequest
                {
                    
                    Name= firstName,
                    Username=userName,
                    Password = password,                   
                    AdminRole="Admin",
                    Email = txtEmail.Text // Use the verified email
                };

                string message = _dataAccess.AdminRegister(request, encryptedPassword);
                Response response = new Response();
                response.Message = message;
                if (response != null && response.Message.Contains("Registered successfully"))
                {
                    lblStatus.Text = "Registration successful.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatus.Text = "Registration failed.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
            }
        }
        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminLogin.aspx");
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
    }
}