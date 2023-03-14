using AutoMapper;
using EmployeeManagmentApplication.Data;
using EmployeeManagmentApplication.Modal.EmployeeProfile;
using EmployeeManagmentApplication.Modal.Modals;
using EmployeeManagmentApplication.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class EmployeePaymentController : ControllerBase
    {

        private readonly IEmployeePaymentRepository _employeePaymentRepository;
        
        public EmployeePaymentController( IEmployeePaymentRepository employeePaymentRepository)
        {
            _employeePaymentRepository = employeePaymentRepository;
        }
        
        [HttpGet("GetEmployeeWithSalaryDetail")]
        public async Task<IActionResult> GetEmployeeWithSalaryDetail()
        {
            return Ok(await _employeePaymentRepository.GetAllEmployeepaymentDetailAsync());
        }


        [HttpGet("GetEmployeeByID/{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)
        {
            return Ok(await _employeePaymentRepository.GetEmployeepaymentDetailByIdAsync(Id));
        }



       


    }

}





