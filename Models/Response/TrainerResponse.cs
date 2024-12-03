namespace JobPortalWebApplication.Models.Response
{
    public class TrainerResponse
    {
        public int SLNO {  get; set; }
        public int TrainerId { get; set; }
        public int CompanyID { get; set; }
        public string EmailStatus { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyWebsiteUrl { get; set; }
        public string PhysicalAddress { get; set; }
        public string CompanyDescription { get; set; }
        public string IndustryType { get; set; }
        public string CompanySize { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhoneNumber { get; set; }
        public string CompanyLogoUrl { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string AgreementToTerms { get; set; }
        public  string AreaOfSpecialization { get; set; }
        public string PlanType { get; set; }
    }
}
