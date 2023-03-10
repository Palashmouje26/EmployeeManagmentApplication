using AutoMapper;
using EmployeeManagmentApplication.Data;
using EmployeeManagmentApplication.Modal.Modals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeRepository _EmployeeRepository;

        private readonly EmployeeDBContext _DBContext;
        private readonly IDataReposatory _dataReposatory;
        private readonly IMapper _mapper;
        public EmployeeRepository(IDataReposatory dataReposatory, IMapper mapper)
        {
            //_DBContext = dbcontext;
            _dataReposatory = dataReposatory;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDetail>> GetAllEmployeeAsync()
        {
            var employeeDetails = await _dataReposatory.Where<Employee>(a => a.Status == "Active").ToListAsync();
            return _mapper.Map<List<Employee>, List<EmployeeDetail>>(employeeDetails);
        }
        public async Task<EmployeeDetail> GetEmployeeByIdAsync(int empId)
        {
            var employeeDetail = await _dataReposatory.FirstAsync<Employee>(a => a.EmployeeId == empId);
            return _mapper.Map<EmployeeDetail>(employeeDetail);

        }
        public async Task<EmployeeDetail> AddEmployeeAsync(EmployeeDetail employee)
        {
            var newEmployee = _mapper.Map<Employee>(employee);
            newEmployee.EmployeeId = 0;
            await _dataReposatory.AddAsync(newEmployee);
            return _mapper.Map<EmployeeDetail>(newEmployee);

        }
        public async Task<EmployeeDetail> UpdateEmployeeAsync(EmployeeDetail employeeDetail)
        {
            var employeeDetails =  _dataReposatory.Where<Employee>(a => a.EmployeeId == employeeDetail.EmployeeId).First();

            employeeDetails.PhoneNumber = employeeDetail.PhoneNumber;
            employeeDetails.EmployeeFirstName = employeeDetail.EmployeeFirstName;
            employeeDetails.EmployeeLastName = employeeDetail.EmployeeLastName;

            await _dataReposatory.UpdateAsync(employeeDetails);
            return employeeDetail;


        }
        public async Task<EmployeeDetail> EmployeeRemoveAsync(int id)
        {
            var empoyeeDetail = _dataReposatory.Where<Employee>(a => a.EmployeeId == id).First();

            await _dataReposatory.RemoveAsync(empoyeeDetail);
         
            return _mapper.Map<EmployeeDetail>(empoyeeDetail);

        }

    
    }
}
