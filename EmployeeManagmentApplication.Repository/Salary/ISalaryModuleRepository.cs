using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.SalaryDetailsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentApplicationRepository.Salary
{
    public interface ISalaryModuleRepository
    {

        Task<List<SalaryDetailsDTO>> GetAllSalaryModuleAsync();
        Task<SalaryDetailsDTO> GetSalaryModuleByIDAsync(int salaryId);

        Task<SalaryDetailsDTO> AddSalaryAsync(SalaryDetailsDTO salaryModule);
        Task<SalaryDetailsDTO> UpdateSalaryModuleAsync(SalaryDetailsDTO salaryModuledetail);

        Task<SalaryDetailsDTO> SalaryModuleRemoveAsync(int id);

    }
}
