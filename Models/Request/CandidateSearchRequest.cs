using System.ComponentModel.DataAnnotations;

namespace JobPortalWebApplication.Models.Request
{
    public class CandidateSearchRequest
    {
        [Required]
        public int? SkillId { get; set; }
        [Range(0, 100, ErrorMessage = "Skill percentage must be between 0 and 100")]
        public double? MinSkillPercentage { get; set; }
        [Range(0, 100, ErrorMessage = "Skill percentage must be between 0 and 100")]
        public double? MaxSkillPercentage { get; set; }
        public bool Core {  get; set; }
    }

}
