using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeeDetailsDTO;

namespace EmployeeManagmentApplication.Repository.EmployeeManagmentRepository
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Fetch the All Employee details.
        /// </summary>
        /// <returns>Fetch the details from the database</returns>
        Task<List<EmployeeDetailsDTO>> GetAllEmployeeAsync();

        /// <summary>
        /// Fetch the employee details with  Id.
        /// </summary>
        /// <param name="empId">Get perticlar employee deatails by Id</param>
        /// <returns> Fetch the details from the database</returns>
        Task<EmployeeDetailsDTO> GetEmployeeByIdAsync(int empId);

        /// <summary>
        /// Add new employee Details.
        /// </summary>
        /// <param name="employee">Add New employee or registration in Database</param>
        /// <returns> return object</returns>
        Task<EmployeeDetailsDTO> AddEmployeeAsync(EmployeeDetailsDTO employee);

        /// <summary>
        /// Updating the employee details.
        /// </summary>
        /// <param name="employeeDetail">Employee detail to update.</param>
        /// <returns>Updated employee detail.</returns>
        Task<EmployeeDetailsDTO> UpdateEmployeeAsync(EmployeeDetailsDTO employeeDetail);


        /// <summary>
        /// Updating the employee status.
        /// </summary>
        /// <param name="empId">Id Is used for selected employee detail to update.</param>
        /// <returns>Employee status update.</returns>
        Task UpdateByStatusAsync (int empId);

    

    }

}
