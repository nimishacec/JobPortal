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

namespace JobPortalWebApplication.Trainer
{
    public partial class TrainerLogin : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        public string _AESKey;
        string connectionString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            _AESKey = Global._AESKey;

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblStatus.Text = "Email and Password are required.";
                lblStatus.Visible = true;
                return;
            }

            LoginRequest request = new LoginRequest
            {
                Email = email,
                Password = password
            };
            EmployerRegResponse data = _dataAccess.TrainerLogin(request);

            if (data != null)
            {
                string decryptedPwd = DecryptString(_AESKey, data.Password);
                if (request.Password == decryptedPwd && data.EmailStatus == "Verified")
                {
                    lblStatus.Text = "Login Success";
                    Session["TrainerID"] = data.EmployeeId;
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    lblStatus.Visible = true;
                    Response.Redirect("TrainerViewDash.aspx");
                }
                else
                {
                    lblStatus.Text = "Invalid email or password, or email not verified.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Visible = true;
                }
            }
            else
            {
                lblStatus.Text = "Login Failed";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Visible = true;
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainerReg.aspx");
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
        protected void btnResetPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("PasswordResetRequest.aspx");
        }
    }
}