namespace JobPortalWebApplication.Models.Request
{
    public class JobSearch
    {
        public string Keyword { get; set; }
        public int Location { get; set; }
        //public string Industry { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        //public string JobType { get; set; }
        public string RequiredSkills { get; set; }
    }
}
