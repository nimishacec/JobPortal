using System;

namespace JobPortalWebApplication.Models.Request
{
    [Serializable]
    public class SkillList
    {
        public int CandidateSkillId {  get; set; }
        public int Id { get; set; }
        public string CoreSkills { get; set; }
        public string SoftSkills { get; set; }
        public float Percentage
        {
            get; set;
        }
    }
}
