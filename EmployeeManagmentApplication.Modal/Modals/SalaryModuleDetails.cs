using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeManagmentApplication.Modal.Modals
{
    public class SalaryModuleDetails
    {

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        public double Basic { get; set; }


        //public virtual ICollection<Employee> Employee { get; set; }
    }
}
