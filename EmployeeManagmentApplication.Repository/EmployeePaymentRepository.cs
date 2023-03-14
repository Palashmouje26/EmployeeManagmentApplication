using AutoMapper;
using EmployeeManagmentApplication.Data;
using EmployeeManagmentApplication.Modal.EmployeeProfile;
using EmployeeManagmentApplication.Modal.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace EmployeeManagmentApplication.Repository
{
    public class EmployeePaymentRepository : IEmployeePaymentRepository
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly ISalaryModuleRepository _SalaryModuleRepository;
        private readonly IDataReposatory _dataReposatory;
        private readonly IMapper _mapper;


        public EmployeePaymentRepository(IDataReposatory dataReposatory, IMapper mapper, IEmployeeRepository employeeRepository)
        {

            _dataReposatory = dataReposatory;
            _mapper = mapper;
            _EmployeeRepository = employeeRepository;

        }

        public async Task<List<EmployeePaymentDetail>> GetAllEmployeepaymentDetailAsync()
        {
            var salarydeatail = await _dataReposatory.Where<Employee>(a => a.Status == "Active")
            .Include(a => a.SalaryModule).ToListAsync();

            var data = salarydeatail.Select(a => new EmployeePaymentDetail
            {
                EmployeeId = a.EmployeeId,
                EmployeeFirstName = a.EmployeeFirstName,
                EmployeeLastName = a.EmployeeLastName,
                EmailId = a.EmailId,
                Address = a.Address,
                PhoneNumber = a.PhoneNumber,
                Basic = calculate(a),
               

            }).ToList();
            return data;
            //return new EmployeePaymentDetail();
            //return _mapper.Map<EmployeeDetail, EmployeePaymentDetail>(salarydeatail);
        }

        private double calculate(Employee a)
        {
            if (a.SalaryModule != null)
                return a.SalaryModule.Basic;
               
           

            return 0;
        }

       
        public async Task<EmployeePaymentDetail> GetEmployeepaymentDetailByIdAsync(int empId)
        {
            var EmployeeDetail = await _EmployeeRepository.GetEmployeeByIdAsync(empId);
            var salaryDetail = await _SalaryModuleRepository.GetSalaryModulesByIDAsync(empId);
            EmployeePaymentDetail employeePaymentDetail = new EmployeePaymentDetail
            {
                EmployeeId = empId,
                EmployeeFirstName = EmployeeDetail.EmployeeFirstName,
                EmployeeLastName = EmployeeDetail.EmployeeLastName,
                EmailId = EmployeeDetail.EmailId,
                Basic= salaryDetail.Basic
                
                
            };
            return employeePaymentDetail;
        }
    }
}
