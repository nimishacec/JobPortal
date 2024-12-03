using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileName = Request.QueryString["file"];
            if (!string.IsNullOrEmpty(fileName))
            {
                // Assuming the files are stored in the 'uploads' folder
                string filePath = Server.MapPath($"~/uploads/{fileName}");

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Serve the file for download
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", $"attachment; filename={Path.GetFileName(filePath)}");
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    // File not found - Handle the error
                    Response.Write("<script>alert('File not found.');</script>");
                }
            }
        }
    }

   
    }
