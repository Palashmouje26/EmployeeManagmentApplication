using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeeDetailsDTO
{
    public class EmployeeDetailsDTO
    {
        public int EmployeeId { get; set; }

        public string EmployeeFirstName { get; set; }

        public string EmployeeLastName { get; set; }

        public string EmailId { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string AdharCard { get; set; }

        public string PanCard { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string EmployeeProfilePhoto { get; set; }

        public bool Status { get; set; }


    }
}
