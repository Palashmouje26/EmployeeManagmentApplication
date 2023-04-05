using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagmentApplication.Modal.EmployeeProfile;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeeDetailsDTO;

namespace EmployeeManagmentApplication.Repository.EmployeeManagmentRepository
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<EmployeeDetailsDTO>> GetAllEmployeeAsync();

        Task<EmployeeDetailsDTO> GetEmployeeByIdAsync(int empId);

        Task<EmployeeDetailsDTO> AddEmployeeAsync(EmployeeDetailsDTO employee);

        Task<EmployeeDetailsDTO> UpdateEmployeeAsync(EmployeeDetailsDTO employeeDetail);
        Task UpdateByStatusAsync (int empId);
        Task<EmployeeDetailsDTO> EmployeeRemoveAsync(int id);

    }

}
