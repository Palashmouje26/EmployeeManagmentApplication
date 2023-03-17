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
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Private Member
        private readonly IDataRepository _dataReposatory;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion


        #region Constructor
        public EmployeeRepository(IDataRepository dataReposatory, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            //_DBContext = dbcontext;
            _dataReposatory = dataReposatory;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Public Method

        public async Task<List<EmployeeDetail>> GetAllEmployeeAsync()
        {
            var employeeDetails = await _dataReposatory.Where<Employee>(a => a.Status).AsNoTracking().ToListAsync();
            return _mapper.Map<List<Employee>, List<EmployeeDetail>>(employeeDetails);
        }
        public async Task<EmployeeDetail> GetEmployeeByIdAsync(int empId)
        {
            var employeeDetail = await _dataReposatory.FirstAsync<Employee>(a => a.EmployeeId == empId);
            return _mapper.Map<EmployeeDetail>(employeeDetail);

        }

        public async Task<EmployeeDetail> AddEmployeeAsync(EmployeeDetail employee)
        {
            var newEmployee = _mapper.Map<EmployeeDetail, Employee>(employee);
            newEmployee.EmployeeId = 0;

            // upload images file //
            if (newEmployee.EmployeeId == 0)
            {
                if (employee.Image.Length <= 2097152)  // File size checking up to 2 mb  //
                {
                    var fileExtenstion = EmployeeProfiel(employee.Image); // file will be checking is extansion formate //
                    if (!fileExtenstion)
                    {
                        return null;
                    }

                    var directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath + "\\ProfileImages\\");   // receving the image path tho save //
                    var filePath = Path.Combine(directoryPath, employee.Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        employee.Image.CopyTo(stream);
                    }
                    newEmployee.EmployeeProfilePhoto = employee.Image.FileName;
                    await _dataReposatory.AddAsync(newEmployee);
                }
            }
            return _mapper.Map<Employee, EmployeeDetail>(newEmployee);

        }
        public async Task<EmployeeDetail> UpdateEmployeeAsync(EmployeeDetail employeeDetail)
        {
            
            var employeeDetails = await _dataReposatory.FirstAsync<Employee>(a => a.EmployeeId == employeeDetail.EmployeeId);

            employeeDetails.EmployeeFirstName = employeeDetail.EmployeeFirstName;
            employeeDetails.EmployeeLastName = employeeDetail.EmployeeLastName;
            employeeDetails.PhoneNumber = employeeDetail.PhoneNumber;

            await _dataReposatory.UpdateAsync(employeeDetails);
            return employeeDetail;


        }
        public async Task<EmployeeDetail> EmployeeRemoveAsync(int id)
        {
            var empoyeeDetail = _dataReposatory.FirstOrDefaultAsync<Employee>(a => a.EmployeeId == id);

            if (empoyeeDetail != null)
            {
                await _dataReposatory.RemoveAsync(empoyeeDetail);
            }


            return _mapper.Map<EmployeeDetail>(empoyeeDetail);

        }


        public async Task UpdateSoftdeleteByStatusAsync(int empId)
        {
            var employeeDetail = await _dataReposatory.FirstAsync<Employee>(a => a.EmployeeId == empId);
            employeeDetail.Status = false;
            await _dataReposatory.UpdateAsync(employeeDetail);
        }

        /**
        * @api {get} /EmployeeProfiel/:id
        * @apiPrivate
        */
        private bool EmployeeProfiel(IFormFile employeeprofilePhoto)  // checking the file is jpg ,jpeg and png formate // 
        {
            var supportedtype = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(employeeprofilePhoto.FileName);
            if (!supportedtype.Contains(fileExtension))
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
