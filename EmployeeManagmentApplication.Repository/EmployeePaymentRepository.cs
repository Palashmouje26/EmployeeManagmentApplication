using AutoMapper;
using EmployeeManagmentApplication.Data;
using EmployeeManagmentApplication.Modal.Modals;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Repository
{
    public class EmployeePaymentRepository : IEmployeePaymentRepository
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly ISalaryModuleRepository _SalaryModuleRepository;
        private readonly IDataReposatory _dataReposatory;
        private readonly IMapper _mapper;


        public EmployeePaymentRepository(IDataReposatory dataReposatory, IMapper mapper, IEmployeeRepository employeeRepository, ISalaryModuleRepository SalaryModuleRepository)
        {
            _dataReposatory = dataReposatory;
            _mapper = mapper;
            _EmployeeRepository = employeeRepository;
            _SalaryModuleRepository = SalaryModuleRepository;
        }

        public async Task<List<EmployeePaymentDetail>> GetAllEmployeepaymentDetailAsync()
        {
            var salarydeatail = await _dataReposatory.Where<Employee>(a => a.Status)
            .Include(b => b.SalaryModule).ToListAsync();

            var data = salarydeatail.Select(a => new EmployeePaymentDetail
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
            return data;

            //return new EmployeePaymentDetail();
            //return _mapper.Map<EmployeeDetail, EmployeePaymentDetail>(salarydeatail);
        }



        public async Task<EmployeePaymentDetail> GetEmployeepaymentDetailByIdAsync(int empId)
        {
            // // first 
            // var employeePaymentDetail = await GetAllEmployeepaymentDetailAsync();
            // var employee = employeePaymentDetail.First(a => a.EmployeeId == empId);
            // return employee;

            // // second
            // var salarydeatail = await _dataReposatory.Where<Employee>(a => a.Status == "Active" && a.EmployeeId == empId)
            //.Include(b => b.SalaryModule).FirstOrDefaultAsync();

            // var data = new EmployeePaymentDetail
            // {
            //     EmployeeId = salarydeatail.EmployeeId,
            //     EmployeeFirstName = salarydeatail.EmployeeFirstName,
            //     EmployeeLastName = salarydeatail.EmployeeLastName,
            //     EmailId = salarydeatail.EmailId,
            //     Address = salarydeatail.Address,
            //     PhoneNumber = salarydeatail.PhoneNumber,
            //     Basic = salarydeatail.SalaryModule.Basic,
            //     HRA = salarydeatail.SalaryModule.HRA,
            //     DA = salarydeatail.SalaryModule.DA,
            //     Deduction = salarydeatail.SalaryModule.Deduction,
            //     PT = salarydeatail.SalaryModule.PT,
            //     NetSalary = salarydeatail.SalaryModule.NetSalary
            // };
            //return data;

            // third
            var EmployeeDetail = await _EmployeeRepository.GetEmployeeByIdAsync(empId);
            var salaryDetail = await _SalaryModuleRepository.GetSalaryModuleByIDAsync(empId);

            EmployeePaymentDetail employeePaymentDetail1 = new EmployeePaymentDetail
            {
                EmployeeId = empId,
                EmployeeFirstName = EmployeeDetail.EmployeeFirstName,
                EmployeeLastName = EmployeeDetail.EmployeeLastName,
                EmailId = EmployeeDetail.EmailId,
                PhoneNumber = EmployeeDetail.PhoneNumber,
                Address = EmployeeDetail.Address,
                Basic = salaryDetail.Basic,
                HRA = salaryDetail.HRA,
                DA = salaryDetail.DA,
                Deduction= salaryDetail.Deduction,
                PT = salaryDetail.PT,
                NetSalary= salaryDetail.NetSalary
            };

            return employeePaymentDetail1;
        }

        //private SalaryModule calculate(Employee a)
        //{
        //    if (a.SalaryModule != null)
        //        return a.SalaryModule;
        //    return null;
        //}
    

    }
}
