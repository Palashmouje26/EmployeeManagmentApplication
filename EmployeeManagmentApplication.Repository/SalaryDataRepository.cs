using EmployeeManagmentApplication.Modal.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Data
{
    public class SalaryDataRepository : ISalaryDataRepository
    {
        private readonly EmployeeDBContext _DBContext;
        public SalaryDataRepository(EmployeeDBContext dbContext)
        {
            _DBContext = dbContext;
        }
        public bool DeleteSalaryModule(int ID)
        {
            bool result = false;
            var department = _DBContext.SalaryModule.Find(ID);
            if (department != null)
            {
                _DBContext.Entry(department).State = EntityState.Deleted;
                _DBContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public async Task<IEnumerable<SalaryModule>> GetSalaryModule()
        {
            return await _DBContext.SalaryModule.ToListAsync();
        }

        public async Task<SalaryModule> GetSalaryModuleByID(int ID)
        {
            return await _DBContext.SalaryModule.FindAsync(ID);
           // return _DBContext.SalaryModule.Where(x => x.SalaryId == ID).FirstOrDefault();
        }



        public async Task<SalaryModule> InsertSalary(SalaryModule SalaryModule)
        {
            _DBContext.SalaryModule.Add(SalaryModule);
            await _DBContext.SaveChangesAsync();
            return SalaryModule;
        }


        public async Task<SalaryModule> UpdateSalary( SalaryModule salaryModule)
        {
            _DBContext.Entry(salaryModule).State = EntityState.Modified;
            await _DBContext.SaveChangesAsync();
            return salaryModule;
        }

    }
}
