namespace JobPortalWebApplication.Models.Response
{
    public class CandidateDetails
    {
        public int SLNO { get; set; }
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalOrZipCode { get; set; }
        public string Country { get; set; }
        public string HighestEducationLevel { get; set; }       
        public string CoreSkill {  get; set; }
        public string Skill { get; set; }
        public float? CoreSkillPercentage { get; set; }
        public string SoftSkill {  get; set; }
        public float? SoftSkillPercentage {  get; set; }
        public string Skills => $"{CoreSkill} ({CoreSkillPercentage}%) / {SoftSkill} ({SoftSkillPercentage}%)";



    }
}
