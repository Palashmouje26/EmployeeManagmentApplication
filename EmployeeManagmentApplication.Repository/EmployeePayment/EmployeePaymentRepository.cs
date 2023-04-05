using AutoMapper;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeePaymentDetailsDTO;
using EmployeeManagmentApplication.Modal.Models.Employee;
using EmployeeManagmentApplication.Repository.EmployeeManagmentRepository;
using EmployeeManagmentApplicationRepository.Data;
using EmployeeManagmentApplicationRepository.Salary;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Repository.EmployeeManagmentPaymentRepository
{
    public class EmployeePaymentRepository : IEmployeePaymentRepository
    {
        #region Privet Members
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly ISalaryModuleRepository _SalaryModuleRepository;
        private readonly IDataRepository _dataReposatory;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public EmployeePaymentRepository(IDataRepository dataReposatory, IMapper mapper, 
            IEmployeeRepository employeeRepository, ISalaryModuleRepository SalaryModuleRepository)
        {
            _dataReposatory = dataReposatory;
            _mapper = mapper;
            _EmployeeRepository = employeeRepository;
            _SalaryModuleRepository = SalaryModuleRepository;
        }
        #endregion
        /// <summary>
        /// This Method Is for showing List of All employee with salary deatils.
        /// </summary>
        /// <returns>List of employee searched.</returns>
        #region Public Methods
        public async Task<List<EmployeePaymentDetailsDTO>> GetAllEmployeepaymentDetailAsync()
        {
            var salarydeatail = await _dataReposatory.Where<Employee>(a => a.Status)
            .Include(b => b.SalaryModule).ToListAsync();

            var paymentdata = salarydeatail.Select(a => new EmployeePaymentDetailsDTO
            {
                EmployeeId = a.EmployeeId,
                EmployeeFirstName = a.EmployeeFirstName,
                EmployeeLastName = a.EmployeeLastName,
                EmailId = a.EmailId,
                Address = a.Address,
                PhoneNumber = a.PhoneNumber,
                Basic = a.SalaryModule.Basic,
                HRA = a.SalaryModule.HRA,
                DA = a.SalaryModule.DA,
                Deduction = a.SalaryModule.Deduction,
                PT = a.SalaryModule.PT,
                NetSalary = a.SalaryModule.NetSalary

            }).ToList();
            return paymentdata;
        }

        /// <summary>
        /// This Method was list of particular employee paymentdetails.
        /// </summary>
        /// <param name="Id">Id used for the for the employee information.</param>
        /// <returns>Showing payment details.</returns>
        public async Task<EmployeePaymentDetailsDTO> GetEmployeepaymentDetailByIdAsync(int Id)
        {

            var EmployeeDetail = await _EmployeeRepository.GetEmployeeByIdAsync(Id);
            var salaryDetail = await _SalaryModuleRepository.GetSalaryModuleByIDAsync(Id);

            EmployeePaymentDetailsDTO employeePaymentDetail1 = new EmployeePaymentDetailsDTO
            {
                EmployeeId = Id,
                EmployeeFirstName = EmployeeDetail.EmployeeFirstName,
                EmployeeLastName = EmployeeDetail.EmployeeLastName,
                EmailId = EmployeeDetail.EmailId,
                PhoneNumber = EmployeeDetail.PhoneNumber,
                Address = EmployeeDetail.Address,
                Basic = salaryDetail.Basic,
                HRA = salaryDetail.HRA,
                DA = salaryDetail.DA,
                Deduction = salaryDetail.Deduction,
                PT = salaryDetail.PT,
                NetSalary = salaryDetail.NetSalary
            };
            return employeePaymentDetail1;
        }
        #endregion

    }
}
