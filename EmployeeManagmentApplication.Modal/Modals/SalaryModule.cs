using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagmentApplication.Modal.Modals
{
    public class SalaryModule
    {
        [Key]
        public int SalaryId { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]

        //public virtual Employee Employee { get; set; }

        public double Basic { get; set; }
        [Required]
        public double HRA { get; set; }
        [Required]
        public double DA { get; set; }
        [Required]
        public double PT { get; set; }
        [Required]
        public double Deduction { get; set; }
        [Required]
        public double NetSalary { get; set; }
    }
}
