using AutoMapper;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.SalaryDetailsDTO;
using EmployeeManagmentApplication.Modal.Models.Salary;
using EmployeeManagmentApplicationRepository.Data;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentApplicationRepository.Salary
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

        #region Public Methods
        /// <summary>
        /// This Method is used for showing all list of employee with details.
        /// </summary>
        /// <returns>List Of Employee Show.</returns>
        public async Task<List<SalaryDetailsDTO>> GetAllSalaryModuleAsync()
        {
            var salarydeatails = await _dataReposatory.GetAllAsync<SalaryModule>();
            return _mapper.Map<List<SalaryModule>, List<SalaryDetailsDTO>>(salarydeatails);
        }

        /// <summary>
        /// This Method is used for showing List of employee with salary details.
        /// </summary>
        /// <param name="Id">Id is for ageting information of employee.</param>
        /// <returns>>Showing of partucular employee with their salary.<returns>
        public async Task<SalaryDetailsDTO> GetSalaryModuleByIDAsync(int Id)
        {
            var salarydeatail = await _dataReposatory.FirstAsync<SalaryModule>(a => a.SalaryId == Id);
            return _mapper.Map<SalaryDetailsDTO>(salarydeatail);
        }

        /// <summary>
        /// This methods add salary module.
        /// </summary>
        /// <param name="salaryModule">Current salary details adding.</param>
        /// <returns>Adding new salary details.</returns>

        public async Task<SalaryDetailsDTO> AddSalaryAsync(SalaryDetailsDTO salaryModule)
        {
            var newSalary = _mapper.Map<SalaryModule>(salaryModule);
            newSalary.SalaryId = 0;
           
            var HRA = salaryModule.Basic + 1000; // Salary Calculation part //
            var DA = salaryModule.Basic * 10 / 100;
            var PT = 200;
            var Deduction = salaryModule.Basic - PT;
            var NetSalary = salaryModule.Basic + HRA + (DA - Deduction);
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
            return _mapper.Map<SalaryDetailsDTO>(newSalary);
        }
        /// <summary>
        /// This methods is updating The salry Module of particulr employee.
        /// </summary>
        /// <param name="salaryModuledetail">Currunt salary Id.</param>
        /// <returns>Upadted Salary Module.</returns>
        public async Task<SalaryDetailsDTO> UpdateSalaryModuleAsync(SalaryDetailsDTO salaryModuledetail)
        {
            var salaryModuleDetails = await _dataReposatory.FirstOrDefaultAsync<SalaryModule>(a => a.SalaryId == salaryModuledetail.SalaryId);

            if (salaryModuleDetails == null)
            {
                return null;
            }
            var HRA = salaryModuledetail.Basic + 1000;
            var DA = salaryModuledetail.Basic * 10 / 100;
            var PT = 200;
            var Deduction = salaryModuledetail.Basic - PT;
            var NetSalary = salaryModuledetail.Basic + HRA + (DA - Deduction);

            salaryModuleDetails.Basic = salaryModuledetail.Basic;
            salaryModuleDetails.HRA = HRA;
            salaryModuleDetails.PT = PT;
            salaryModuleDetails.DA = DA;
            salaryModuleDetails.Deduction = Deduction;
            salaryModuleDetails.NetSalary = NetSalary;

            await _dataReposatory.UpdateAsync(salaryModuleDetails);
            return _mapper.Map<SalaryDetailsDTO>(salaryModuleDetails);
        }
        /// <summary>
        /// This Method is remove the Salary Module.
        /// </summary>
        /// <param name="Id">Id is used for getting employee salary information.</param>
        /// <returns>Remove the Salary of particular employee.</returns>
        public async Task<SalaryDetailsDTO> SalaryModuleRemoveAsync(int Id)
        {
            var salaryDetail = await _dataReposatory.FirstOrDefaultAsync<SalaryModule>(a => a.SalaryId == Id);

            if (salaryDetail != null)
            {
                await _dataReposatory.RemoveAsync(salaryDetail);
            }

            return _mapper.Map<SalaryDetailsDTO>(salaryDetail);

        }
        #endregion
    }

}
