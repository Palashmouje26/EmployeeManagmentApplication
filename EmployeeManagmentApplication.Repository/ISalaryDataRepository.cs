using EmployeeManagmentApplication.Modal.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Data
{
    public interface ISalaryDataRepository
    {
        Task<IEnumerable<SalaryModule>> GetSalaryModule();
        Task<SalaryModule> GetSalaryModuleByID(int ID);
        Task<SalaryModule> InsertSalary(SalaryModule SalaryModule);
        Task<SalaryModule> UpdateSalary(SalaryModule SalaryModule);
        bool DeleteSalaryModule(int ID);
    }
}
