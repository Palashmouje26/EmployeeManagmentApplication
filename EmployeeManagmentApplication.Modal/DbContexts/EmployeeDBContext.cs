using EmployeeManagmentApplication.Modal.Models.EmployeeManagment;
using EmployeeManagmentApplication.Modal.Models.Salary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagmentApplication.Modal.DbContexts
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<SalaryModule> SalaryModule { get; set; }
        public string ContentRootPath { get; internal set; }
    }
}
