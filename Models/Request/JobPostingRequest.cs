using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortalWebApplication.Models.Request
{
    public class JobPostingRequest
    {
        //[Required]
        public int EmployeeId { get; set; }
        [Required]
        public int JobId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Job Title cannot be more than 100 characters.")]
        public string JobTitle { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Vacancy must be greater than 0.")]
        public int Vacancy { get; set; }

        [StringLength(250, ErrorMessage = "Job Description cannot be more than 250 characters.")]
        public string JobDescription { get; set; }

        [StringLength(250, ErrorMessage = "Qualifications cannot be more than 250 characters.")]
        public string Qualifications { get; set; }

        [StringLength(250, ErrorMessage = "Experience cannot be more than 250 characters.")]
        public string Experience { get; set; }

        [StringLength(100, ErrorMessage = "Specialization cannot be more than 100 characters.")]
        public string RequiredSkills { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "JobLocationId must be greater than 0.")]
        public int JobLocationId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal? Salary { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Company Name cannot be more than 200 characters.")]
        public string CompanyName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "JobType must be greater than 0.")]
        public int JobTypeId { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot be more than 250 characters.")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ApplicationDeadline { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string ContactEmail { get; set; }

        [StringLength(100, ErrorMessage = "Website URL cannot be more than 100 characters.")]
        public string Website { get; set; }
        [StringLength(100, ErrorMessage = "Website URL cannot be more than 100 characters.")]
        public string Industry { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ApplicationStartDate { get; set; }
    }
}
