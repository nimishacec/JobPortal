using JobPortalWebApplication.DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using JobPortalWebApplication.Models.Response;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using JobPortalWebApplication.Candidate;
using System.Timers;
using Serilog;

namespace JobPortalWebApplication
{
    public class Global : HttpApplication
    {
        public static DataAccess DataAccess;
        public static string _AESKey;
      
        void Application_Start(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            DataAccess = new DataAccess(connectionString);
            _AESKey = ConfigurationManager.AppSettings["AESKey"];
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles); 
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(@"\\26.163.23.219\projects-blueblocks\Nimisha\JobPortal-New\Logs\serilog-logfile.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
         
            //InitializeJobAlertTimer();
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");  // Specify the allowed origin
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization"); // Allowed headers
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS"); // Allowed methods

            // If it's a preflight request, return OK
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.StatusCode = 200;
                HttpContext.Current.Response.End();
            }
        }





    }
}
