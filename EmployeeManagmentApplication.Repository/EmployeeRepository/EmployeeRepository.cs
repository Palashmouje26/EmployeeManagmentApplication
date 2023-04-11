using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using EmployeeManagmentApplicationRepository.Data;
using EmployeeManagmentApplication.Modal.Models.EmployeeManagment;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeeDetailsDTO;

namespace EmployeeManagmentApplication.Repository.EmployeeManagmentRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Private Member
        private readonly IDataRepository _dataRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion


        #region Constructor
        public EmployeeRepository(IDataRepository dataRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {

            _dataRepository = dataRepository;
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
            var employeeDetails = await _dataRepository.Where<Employee>(a => a.Status).AsNoTracking().ToListAsync();
            return _mapper.Map<List<Employee>, List<EmployeeDetailsDTO>>(employeeDetails);
        }

        /// <summary>
        /// This method is used for showing one employee detail using empId.
        /// </summary>
        /// <param name="empId">empId is used for employee details.</param>
        /// <returns>return show employee details</returns>
        public async Task<EmployeeDetailsDTO> GetEmployeeByIdAsync(int empId)
        {
            var employeeDetail = await _dataRepository.FirstAsync<Employee>(a => a.EmployeeId == empId);
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

                if (employee.Image.Length <= 2097152)  // File size checking up to 2 mb  
                {
                    bool fileExtenstion = EmployeeProfielAsync(employee.Image); // file will be checking is extansion formate 
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
                    await _dataRepository.AddAsync(newEmployee);
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
            var employeeDetails = await _dataRepository.FirstAsync<Employee>(a => a.EmployeeId == employeeDetail.EmployeeId);
            var response = _mapper.Map<EmployeeDetailsDTO, Employee>(employeeDetail, employeeDetails);
            await _dataRepository.UpdateAsync(response);
            return _mapper.Map<EmployeeDetailsDTO>(employeeDetails);
       
        }

        /// <summary>
        /// This Method is used for status change.
        /// </summary>
        /// <param name="empId">empId is used for particuller employee status change.</param>
        /// <returns>return status change.</returns>
        public async Task UpdateByStatusAsync(int empId)
        {
            var employeeDetail = await _dataRepository.FirstOrDefaultAsync<Employee>(a => a.EmployeeId == empId);

            if (employeeDetail != null)
            {
                employeeDetail.Status = false;
            }
            else
            {
                throw new Exception("Employee Not Exits");
            }
            employeeDetail.Status = false;
            await _dataRepository.UpdateAsync(employeeDetail);
        }

        /// <summary>
        /// This Method is checking the file formate of image.
        /// </summary>
        /// <param name="employeeProfilePhoto">Current images fromate checking.</param>
        /// <returns>When file is validate then return true else false.</returns>
        #region Private Method
        private bool EmployeeProfielAsync(IFormFile employeeProfilePhoto)  // checking the file is jpg ,jpeg and png formate // 
        {
            var supportedtype = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(employeeProfilePhoto.FileName);
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
