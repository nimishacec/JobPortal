using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class EmployerLogin : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        public string _AESKey;
        string connectionString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            _AESKey = Global._AESKey;
            if (!IsPostBack)
            {


            }

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployerRegistration.aspx");
        }
        protected void btnResetPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("PasswordResetRequest.aspx");
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblStatus.Text = "Email and Password are required.";
                return;
            }

            LoginRequest request = new LoginRequest
            {
                Email = email,
                Password = password
            };
            EmployerRegResponse data = _dataAccess.EmployeeLogin(request);

            if (data != null)
            {
                string decryptedPwd = DecryptString(_AESKey, data.Password);
                if (decryptedPwd != password)
                {
                    lblStatus.Text = "Invalid Password .";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Visible = true;
                    return;
                }

                


                if (request.Password == decryptedPwd && data.EmailStatus == "Verified")
                {
                    lblStatus.Text = "Login Success";
                    Session["EmployeeID"] = data.EmployeeId;
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("EmployerNewDash.aspx");

                    // Register JavaScript to redirect
                    //  ClientScript.RegisterStartupScript(this.GetType(), "redirect", "redirectToDashboard();", true);

                }
                else
                {
                    lblStatus.Text = "Invalid email or password, or email not verified.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblStatus.Text = "Login Failed";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}