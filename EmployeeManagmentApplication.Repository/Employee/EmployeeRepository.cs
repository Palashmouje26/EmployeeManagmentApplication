using AutoMapper;
using EmployeeManagmentApplication.Modal.EmployeeProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using EmployeeManagmentApplicationRepository.Data;
using EmployeeManagmentApplication.Modal.Models.Employee;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeeDetailsDTO;

namespace EmployeeManagmentApplication.Repository.EmployeeManagmentRepository
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

            _dataReposatory = dataReposatory;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// This method is used for showing all list employee.
        /// </summary>
        /// <returns>returs to showing list </returns>
        public async Task<List<EmployeeDetailsDTO>> GetAllEmployeeAsync()
        {
            var employeeDetails = await _dataReposatory.Where<Employee>(a => a.Status).AsNoTracking().ToListAsync();
            return _mapper.Map<List<Employee>, List<EmployeeDetailsDTO>>(employeeDetails);
        }

        /// <summary>
        /// This method is used for showing one employee detail using empId.
        /// </summary>
        /// <param name="empId">empId is used for employee details.</param>
        /// <returns>return show employee details</returns>
        public async Task<EmployeeDetailsDTO> GetEmployeeByIdAsync(int empId)
        {
            var employeeDetail = await _dataReposatory.FirstAsync<Employee>(a => a.EmployeeId == empId);
            return _mapper.Map<EmployeeDetailsDTO>(employeeDetail);

        }
        /// <summary>
        ///This method used for to add or store Employee to the database.
        /// </summary>
        /// <param name="employee">It is current used for store the employee</param>
        /// <returns>return object</returns>
        public async Task<EmployeeDetailsDTO> AddEmployeeAsync(EmployeeDetailsDTO employee)
        {
            var newEmployee = _mapper.Map<EmployeeDetailsDTO, Employee>(employee);
            newEmployee.EmployeeId = 0;

            if (newEmployee.EmployeeId == 0)
            {
                if (employee.Image.Length <= 2097152)  // File size checking up to 2 mb  
                {
                    var fileExtenstion = EmployeeProfiel(employee.Image); // file will be checking is extansion formate 
                    if (!fileExtenstion)
                    {
                        return null;
                    }

                    var directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath + "\\ProfileImages\\"); // receving the image path tho save //
                    var filePath = Path.Combine(directoryPath, employee.Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        employee.Image.CopyTo(stream);
                    }
                    newEmployee.EmployeeProfilePhoto = employee.Image.FileName;
                    await _dataReposatory.AddAsync(newEmployee);
                }
            }
            return _mapper.Map<Employee, EmployeeDetailsDTO>(newEmployee);

        }

        /// <summary>
        /// This Method is updating employee details.
        /// </summary>
        /// <param name="employeeDetail">This details update employee details.</param>
        /// <returns>It retuns the result</returns>
        public async Task<EmployeeDetailsDTO> UpdateEmployeeAsync(EmployeeDetailsDTO employeeDetail)
        {
            var employeeDetails = await _dataReposatory.FirstAsync<Employee>(a => a.EmployeeId == employeeDetail.EmployeeId);

            employeeDetails.EmployeeFirstName = employeeDetail.EmployeeFirstName;
            employeeDetails.EmployeeLastName = employeeDetail.EmployeeLastName;
            employeeDetails.PhoneNumber = employeeDetail.PhoneNumber;

            await _dataReposatory.UpdateAsync(employeeDetails);
            return employeeDetail;
        }
        /// <summary>
        /// This Method is used for remove or inactive employee.
        /// </summary>
        /// <param name="id">Id Is used for removing the employee.</param>
        /// <returns>It returns the object</returns>
        public async Task<EmployeeDetailsDTO> EmployeeRemoveAsync(int id)
        {
            var empoyeeDetail = _dataReposatory.FirstOrDefaultAsync<Employee>(a => a.EmployeeId == id);

            if (empoyeeDetail != null)
            {
                await _dataReposatory.RemoveAsync(empoyeeDetail);
            }
            return _mapper.Map<EmployeeDetailsDTO>(empoyeeDetail);
        }

        /// <summary>
        /// This Method is used for status change.
        /// </summary>
        /// <param name="empId">empId is used for particuller employee status change.</param>
        /// <returns>return status change.</returns>
        public async Task UpdateByStatusAsync(int empId)
        {
            var employeeDetail = await _dataReposatory.FirstAsync<Employee>(a => a.EmployeeId == empId);
            employeeDetail.Status = false;
            await _dataReposatory.UpdateAsync(employeeDetail);
        }

        /// <summary>
        /// This Method is checking the file formate of image.
        /// </summary>
        /// <param name="employeeprofilePhoto">Current images fromate checking.</param>
        /// <returns>returns true or false</returns>
        #region Private Method
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
        #endregion
    }
}
