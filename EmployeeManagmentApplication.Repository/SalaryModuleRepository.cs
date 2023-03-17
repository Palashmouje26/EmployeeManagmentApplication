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
        #region Privet Memebar
        private readonly IDataRepository _dataReposatory;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public SalaryModuleRepository(IDataRepository dataReposatory, IMapper mapper)
        {
            _dataReposatory = dataReposatory;
            _mapper = mapper;
        }
        #endregion
        /// <summary>
        /// This Method is used for showing all list of employee with details
        /// </summary>
        /// <returns></returns>
        #region Public Methods
        public async Task<List<SalaryModuleDetails>> GetAllSalaryModuleAsync()
        {
            var salarydeatails = await _dataReposatory.GetAllAsync<SalaryModule>();
            return _mapper.Map<List<SalaryModule>, List<SalaryModuleDetails>>(salarydeatails);   
        }

        /// <summary>
        /// This Method is used for showing List of employee with salary details
        /// </summary>
        /// <param name="empId"></param>
        /// <returns>>List of partucular employee with their Salary <returns>
        public async Task<SalaryModuleDetails> GetSalaryModuleByIDAsync(int empId)
        {
            var salarydeatail = await _dataReposatory.FirstAsync<SalaryModule>(a => a.SalaryId == empId);
            return _mapper.Map<SalaryModuleDetails>(salarydeatail);
        }

        /// <summary>
        /// This Methods Add salary Module
        /// </summary>
        /// <param name="salaryModule"></param>
        /// <returns></returns>

        public async Task<SalaryModuleDetails> AddSalaryAsync(SalaryModuleDetails salaryModule)
        {
            var newSalary = _mapper.Map<SalaryModule>(salaryModule);
            newSalary.SalaryId = 0;
            // Salary Calculation part //
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
        /// <summary>
        /// This Methods is Updating The ssalry Module of Particulr Employee
        /// </summary>
        /// <param name="salaryModuledetail">Currunt salary Id </param>
        /// <returns>Upadted Salary Module</returns>
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
        /// <summary>
        /// This Method is remove the Salary Module 
        /// </summary>
        /// <param name="id">with the help or salary Id </param>
        /// <returns>Remove the Salary of particular employee</returns>
        public async Task<SalaryModuleDetails> SalaryModuleRemoveAsync(int id)
        {
            var salaryModuleDetail = await _dataReposatory.FirstOrDefaultAsync<SalaryModule>(a => a.SalaryId == id);

            if (salaryModuleDetail != null)
            {
                await _dataReposatory.RemoveAsync(salaryModuleDetail);
            }

            return _mapper.Map<SalaryModuleDetails>(salaryModuleDetail);

        }
        #endregion
    }

}
