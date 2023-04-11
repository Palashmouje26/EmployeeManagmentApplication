using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EmployeeManagmentApplication.Modal.Models.EmployeeManagment;

namespace EmployeeManagmentApplication.Modal.Models.Salary
{
    public class EmployeeSalary
    {
        /// <summary>
        /// Id of the employeesalary.
        /// </summary>
        [Key]
        public int SalaryId { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

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
