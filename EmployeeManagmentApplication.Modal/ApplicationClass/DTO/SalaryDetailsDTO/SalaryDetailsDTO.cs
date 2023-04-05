using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeManagmentApplication.Modal.ApplicationClass.DTO.SalaryDetailsDTO
{
    public class SalaryDetailsDTO
    {
        public int SalaryId { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        public double Basic { get; set; }

        public double HRA { get; set; }
        public double DA { get; set; }

        public double PT { get; set; }

        public double Deduction { get; set; }

        public double NetSalary { get; set; }


    }
}
