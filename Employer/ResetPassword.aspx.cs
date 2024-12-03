using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JobPortalWebApplication.DataBase;

namespace JobPortalWebApplication.Employer
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        private string _AESKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            _AESKey = Global._AESKey;
            //if (!IsPostBack)
            //{
            string token = Request.QueryString["token"];
            //if (string.IsNullOrEmpty(token) || !ValidateToken(token))
            //{
            //    Response.Redirect("InvalidToken.aspx");
            ////}
            // }
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            string newPassword = "";
            if (IsPasswordStrong(txtNewPassword.Text))
            {
                newPassword = txtNewPassword.Text;


                if (ValidateToken(token))
                {
                    // Hash the new password
                    string encryptedPassword = EncryptString(_AESKey, newPassword);

                    var reset = _dataAccess.ResetPassword(encryptedPassword, token, "EMPLOYER");
                    if (reset)
                    {
                        lblMessage.Text = "Your password has been reset successfully.";
                        lblMessage.Visible = true;
                        string script = $@"
                            setTimeout(function() {{
                                window.location.href = 'EmployerLogin.aspx';
                            }}, 3000);"; 
                        ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", script, true);
                    }
                    else
                    {
                        lblMessage.Text = "Invalid or expired token.";
                        lblMessage.Visible = true;
                        string script = $@"
                            setTimeout(function() {{
                                window.location.href = 'EmployerLogin.aspx';
                            }}, 3000);"; 
                        ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", script, true);
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid or expired token.";
                    lblMessage.Visible = true;
                    string script = $@"
                    setTimeout(function() {{
                        window.location.href = 'EmployerLogin.aspx';
                    }}, 3000);";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", script, true);
                }
            }
            else
            {
                lblMessage.Text = "Password is not strong enough";
                lblMessage.Visible = true;
                string script = $@"
    setTimeout(function() {{
        window.location.href = 'EmployerLogin.aspx';
    }}, 3000);"; // 3000 milliseconds = 3 seconds

                // Register the script using ScriptManager
                ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", script, true);
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

        private bool ValidateToken(string token)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }
            else if (token.Length == 0)
            {
                throw new ArgumentException("token lenghth is 0");
            }
            else
            {
                bool IsValid = false;
                IsValid = _dataAccess.GetValidToken(token);
                if (IsValid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

    }
}