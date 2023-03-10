using EmployeeManagmentApplication.Modal.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Repository
{
    public class SalaryModuleRepository : ISalaryModuleRepository
    {
        
       
        public bool DeleteSalaryModule(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SalaryModule>> GetSalaryModule()
        {
            throw new NotImplementedException();
        }

        public Task<SalaryModule> GetSalaryModuleByID(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<SalaryModule> InsertSalary(SalaryModule SalaryModule)
        {
            throw new NotImplementedException();
        }

        public Task<SalaryModule> UpdateSalary(SalaryModule SalaryModule)
        {
            throw new NotImplementedException();
        }
    }
}
