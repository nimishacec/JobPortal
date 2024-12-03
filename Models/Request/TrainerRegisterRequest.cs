using System.ComponentModel.DataAnnotations;

namespace JobPortalWebApplication.Models.Request
{
    public class TrainerRegisterRequest
    {
        [Required]
        public int OTP { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool AgreeTotermsAndConditions { get; set; }
    }
}
