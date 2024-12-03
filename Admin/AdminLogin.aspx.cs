using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using Org.BouncyCastle.Asn1.Cmp;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class AdminLogin : System.Web.UI.Page
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
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtusername.Text.Trim();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(txtpass.Text))
            {
                lblStatus.InnerHtml = "Email and Password are required.";
                lblStatus.Attributes.Add("style", "color: red; display: block;");
                return;
            }
            LoginRequest request = new LoginRequest
            {
                Email = email,
                Password = txtpass.Text,
            };
            if (request.Password != null)
            {
                string encryptedPwd = EncryptString(_AESKey, request.Password);
                var data = _dataAccess.AdminLogin(email, encryptedPwd);
                if (data != null)
                {
                    if (data.Message != "Invalid email or password.")
                    {
                        string decryptedPwd = DecryptString(_AESKey, data.Password);
                        //string password = txtpass.Text;
                        if (request.Password == decryptedPwd)
                        {
                            lblStatus.InnerHtml = "Login Success"; // Set the error text
                            lblStatus.Attributes.Add("style", "color: Green; display: block;");
                            Log.Information("Admin logginIN");
                            Session["AdminID"] = data.AdminId;
                            Response.Redirect("AdminNewDash.aspx");
                        }
                        else
                        {
                            lblStatus.InnerHtml = "Login Failed";
                            lblStatus.Attributes.Add("style", "color: red; display: block;");
                        }

                    }
                    else
                    {
                        lblStatus.InnerHtml = "Invalid email or password .";
                        lblStatus.Attributes.Add("style", "color: red; display: block;");
                        return;
                    }

                    //string decryptedPwd = DecryptString(_AESKey, data.Password);
                    //string password = txtpass.Text;
                    //if(decryptedPwd != txtpass.Text)
                    //{
                    //    lblStatus.Text = "Invalid Password .";
                    //    lblStatus.Visible = true;
                    //    return;

                    //}





                    // string decryptedPwd = DecryptString(_AESKey, data.Password);

                    //string decryptedPwd = DecryptString(_AESKey, data.Password);
                    //if (request.Password == decryptedPwd)
                    //{
                    //    lblStatus.InnerHtml = "Login Success"; // Set the error text
                    //    lblStatus.Attributes.Add("style", "color: Green; display: block;");

                    //    Session["AdminID"] = data.AdminId;
                    //    Response.Redirect("AdminNewDash.aspx");


                    //}
                    //else
                    //{
                    //    lblStatus.InnerHtml= "Invalid email or password, or email not verified.";
                    //    lblStatus.Attributes.Add("style", "color: red; display: block;");
                    //}
                }
                else
                {
                    lblStatus.InnerHtml = "Login Failed";
                    lblStatus.Attributes.Add("style", "color: red; display: block;");
                }
            }
        }
        protected void btnResetPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("PasswordResetRequest.aspx");
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
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminReg.aspx");
        }
    }
}