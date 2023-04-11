using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.SalaryDetailsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentApplicationRepository.Salary
{
    public interface ISalaryModuleRepository
    {

        /// <summary>
        /// Fetch the All Employee Salary details.
        /// </summary>
        /// <returns>Fetch the details from the database</returns>
        Task<List<SalaryDetailsDTO>> GetAllSalaryAsync();

        /// <summary>
        /// Fetch the employee salary details with  Id.
        /// </summary>
        /// <param name="salaryId">Get perticlar employee salary deatails by Id</param>
        /// <returns> Fetch the details from the database</returns>
        Task<SalaryDetailsDTO> GetSalaryByIDAsync(int salaryId);


        /// <summary>
        /// Add new salary Details.
        /// </summary>
        /// <param name="salary">Add New salary or registration in Database</param>
        /// <returns> return object</returns>
        Task<SalaryDetailsDTO> AddSalaryAsync(SalaryDetailsDTO salary);

        /// <summary>
        ///  Updating the employee status.
        /// </summary>
        /// <param name="salarydetail">Employee detail to update.</param>
        /// <returns> Employee status update.</returns>
        Task<SalaryDetailsDTO> UpdateSalaryAsync(SalaryDetailsDTO salarydetail);

   

    }
}
