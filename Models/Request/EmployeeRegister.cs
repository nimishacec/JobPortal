﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortalWebApplication.Models.Request
{
    public class EmployeeRegister
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
        public bool AgreeTotermsAndConditions { get; set; } = true;
    }
}