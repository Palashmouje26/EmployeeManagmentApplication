using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeManagmentApplication.Modal.EmployeeProfile
{
    public class EmployeeProfiles
    {
     
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        public string EmployeeFirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string EmployeeLastName { get; set; }

        [RegularExpression(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+\.)+[a-z]{2,5}$", ErrorMessage = "Email address must be Combination email.")]
        public string EmailId { get; set; }

        public string Address { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^(\d{12}|\d{16})$", ErrorMessage = "enter Integers only")]
        public string AdharCard { get; set; }

        [RegularExpression(@"[A-Z]{5}[0-9]{4}[A-Z]{1}", ErrorMessage = "enter Valid Pan number only")]
        public string PanCard { get; set; }
        public IFormFile EmployeeProfilePhoto { get; set; }
        public string Status { get; set; }

      
    }
}
