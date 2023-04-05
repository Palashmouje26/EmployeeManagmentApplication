using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeePaymentDetailsDTO
{
    public class EmployeePaymentDetailsDTO
    {
        public int EmployeeId { get; set; }

        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmailId { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }


        public double? Basic { get; set; }
        public double HRA { get; set; }
        public double DA { get; set; }
        public double PT { get; set; }
        public double Deduction { get; set; }
        public double NetSalary { get; set; }

    }
}
