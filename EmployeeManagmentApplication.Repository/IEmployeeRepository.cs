using EmployeeManagmentApplication.Modal.Modals;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagmentApplication.Modal.EmployeeProfile;

namespace EmployeeManagmentApplication.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDetail>> GetAllEmployeeAsync();

        Task<EmployeeDetail> GetEmployeeByIdAsync(int empId);

        Task<EmployeeDetail> AddEmployeeAsync(EmployeeDetail employee);

        Task<EmployeeDetail> UpdateEmployeeAsync(EmployeeDetail employeeDetail);
        Task UpdateSoftdeleteByStatusAsync(int empId);
        Task<EmployeeDetail> EmployeeRemoveAsync(int id);

    }

}
