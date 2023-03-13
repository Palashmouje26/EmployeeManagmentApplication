using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeManagmentApplication.Modal.Modals
{
    public class SalaryModuleDetails
    {
        public int SalaryId { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        public double Basic { get; set; }



        //public virtual ICollection<Employee> Employee { get; set; }
    }
}
