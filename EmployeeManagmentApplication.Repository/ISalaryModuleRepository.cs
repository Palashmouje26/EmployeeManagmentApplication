using EmployeeManagmentApplication.Modal.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Repository
{
    public interface ISalaryModuleRepository
    {

        Task<List<SalaryModuleDetails>> GetAllSalaryModuleAsync();
        Task<SalaryModuleDetails> GetSalaryModuleByIDAsync(int salaryId);
        Task<SalaryModuleDetails> AddSalaryAsync(SalaryModuleDetails salaryModule);
        Task<SalaryModuleDetails> UpdateSalaryModuleAsync(SalaryModuleDetails salaryModuledetail);

        Task<SalaryModuleDetails> SalaryModuleRemoveAsync(int id);
        Task<SalaryModuleDetails> GetSalaryModulesByIDAsync(int salaryId);
    }
}
