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
        public async Task<List<SalaryDetailsDTO>> GetAllSalaryAsync()
        {
            var salarydetail = await _dataReposatory.GetAllAsync<EmployeeManagmentApplication.Modal.Models.Salary.EmployeeSalary>();
            return _mapper.Map<List<EmployeeSalary>, List<SalaryDetailsDTO>>(salarydetail);
        }

        /// <summary>
        /// This Method is used for showing List of employee with salary details.
        /// </summary>
        /// <param name="Id">Id is for ageting information of employee.</param>
        /// <returns>>Showing of partucular employee with their salary.<returns>
        public async Task<SalaryDetailsDTO> GetSalaryByIDAsync(int Id)
        {
            var salarydeatail = await _dataReposatory.FirstAsync<EmployeeManagmentApplication.Modal.Models.Salary.EmployeeSalary>(a => a.SalaryId == Id);
            return _mapper.Map<SalaryDetailsDTO>(salarydeatail);
        }

        /// <summary>
        /// This methods add salary module.
        /// </summary>
        /// <param name="salaryModule">Current salary details adding.</param>
        /// <returns>Adding new salary details.</returns>

        public async Task<SalaryDetailsDTO> AddSalaryAsync(SalaryDetailsDTO salary)
        {
            var newSalary = _mapper.Map<EmployeeSalary>(salary);
            newSalary.SalaryId = 0;
           
            var HRA = salary.Basic + 1000; // Salary Calculation part //
            var DA = salary.Basic * 10 / 100;
            var PT = 200;
            var Deduction = salary.Basic - PT;
            var NetSalary = salary.Basic + HRA + (DA - Deduction);
            var salaryDetal = new EmployeeManagmentApplication.Modal.Models.Salary.EmployeeSalary
            {
                EmployeeId = salary.EmployeeId,
                Basic = salary.Basic,
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
        public async Task<SalaryDetailsDTO> UpdateSalaryAsync(SalaryDetailsDTO salarydetail)
        {
            var salaryData = await _dataReposatory.FirstOrDefaultAsync<EmployeeManagmentApplication.Modal.Models.Salary.EmployeeSalary>(a => a.SalaryId == salarydetail.SalaryId);

            if (salaryData == null)
            {
                return null;
            }
            var HRA = salarydetail.Basic + 1000;
            var DA = salarydetail.Basic * 10 / 100;
            var PT = 200;
            var Deduction = salarydetail.Basic - PT;
            var NetSalary = salarydetail.Basic + HRA + (DA - Deduction);

            salaryData.Basic = salarydetail.Basic;
            salaryData.HRA = HRA;
            salaryData  .PT = PT;
            salaryData  .DA = DA;
            salaryData.Deduction = Deduction;
            salaryData.NetSalary = NetSalary;

            await _dataReposatory.UpdateAsync(salaryData);
            return _mapper.Map<SalaryDetailsDTO>(salaryData);
        }
       
        #endregion
    }

}
