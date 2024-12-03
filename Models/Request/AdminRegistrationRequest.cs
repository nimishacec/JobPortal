using System;

namespace JobPortalWebApplication.Models.Request
{
    public class AdminRegistrationRequest
    {
        public int AdminId { get; set; }
        public string Name {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string AdminRole {  get; set; }
        public string Username { get; set; } 
        public DateTime  CreatedDate { get; set; }
        public string Message {  get; set; }
       

    }
}
