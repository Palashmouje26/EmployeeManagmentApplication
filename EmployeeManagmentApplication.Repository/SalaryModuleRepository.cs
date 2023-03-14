using AutoMapper;
using EmployeeManagmentApplication.Data;
using EmployeeManagmentApplication.Modal.Modals;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Repository
{
    public class SalaryModuleRepository : ISalaryModuleRepository
    {
        private readonly IDataReposatory _dataReposatory;
        private readonly IMapper _mapper;
        public SalaryModuleRepository(IDataReposatory dataReposatory, IMapper mapper)
        {
            _dataReposatory = dataReposatory;
            _mapper = mapper;
        }

        public async Task<List<SalaryModuleDetails>> GetAllSalaryModuleAsync()
        {
            var salarydeatails = await _dataReposatory.GetAllAsync<SalaryModule>();
            return _mapper.Map<List<SalaryModule>, List<SalaryModuleDetails>>(salarydeatails);   
        }

        public async Task<SalaryModuleDetails> GetSalaryModuleByIDAsync(int empId)
        {
            var salarydeatail = await _dataReposatory.FirstAsync<SalaryModule>(a => a.SalaryId == empId);
            return _mapper.Map<SalaryModuleDetails>(salarydeatail);
        }
        public async Task<SalaryModuleDetails> GetSalaryModulesByIDAsync(int salaryId)
        {
            var salarydeatail = await _dataReposatory.FirstAsync<SalaryModule>(a => a.EmployeeId == salaryId);
            return _mapper.Map<SalaryModuleDetails>(salarydeatail);
        }

        public async Task<SalaryModuleDetails> AddSalaryAsync(SalaryModuleDetails salaryModule)
        {
            var newSalary = _mapper.Map<SalaryModule>(salaryModule);
            newSalary.SalaryId = 0;

            var HRA = salaryModule.Basic + 1000;
            var DA = salaryModule.Basic * 10 / 100;
            var PT = 200;
            var Deduction = salaryModule.Basic - PT;
            var NetSalary = (salaryModule.Basic + HRA) + (DA - Deduction);
            var salaryDetal = new SalaryModule
            {
                EmployeeId = salaryModule.EmployeeId,
                Basic = salaryModule.Basic,
                HRA = HRA,
                PT = PT,
                DA = DA,
                Deduction = Deduction,
                NetSalary = NetSalary,
            };

            await _dataReposatory.AddAsync(salaryDetal);
            return _mapper.Map<SalaryModuleDetails>(newSalary);
        }
        
        public async Task<SalaryModuleDetails> UpdateSalaryModuleAsync(SalaryModuleDetails salaryModuledetail )
        {
            var salaryModuleDetails = await _dataReposatory.FirstOrDefaultAsync<SalaryModule>(a => a.SalaryId == salaryModuledetail.SalaryId);

            if(salaryModuleDetails == null)
            {
                return null;
            }
            var HRA = salaryModuledetail.Basic + 1000;
            var DA = salaryModuledetail.Basic * 10 / 100;
            var PT = 200;
            var Deduction = salaryModuledetail.Basic - PT;
            var NetSalary = (salaryModuledetail.Basic + HRA) + (DA - Deduction);
          
            salaryModuleDetails.Basic = salaryModuledetail.Basic;
            salaryModuleDetails.HRA = HRA;
            salaryModuleDetails.PT = PT;
            salaryModuleDetails.DA = DA;
            salaryModuleDetails.Deduction = Deduction;
            salaryModuleDetails.NetSalary = NetSalary;

            await _dataReposatory.UpdateAsync(salaryModuleDetails);
            return _mapper.Map<SalaryModuleDetails>(salaryModuleDetails);
        }
        public async Task<SalaryModuleDetails> SalaryModuleRemoveAsync(int id)
        {
            var salaryModuleDetail = await _dataReposatory.FirstOrDefaultAsync<SalaryModule>(a => a.SalaryId == id);

            if (salaryModuleDetail != null)
            {
                await _dataReposatory.RemoveAsync(salaryModuleDetail);
            }

            return _mapper.Map<SalaryModuleDetails>(salaryModuleDetail);

        }
        
    }
       
}
