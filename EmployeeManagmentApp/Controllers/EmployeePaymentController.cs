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
        #region Privet Member
        private readonly IEmployeePaymentRepository _employeePaymentRepository;
        #endregion

        #region Constructor
        public EmployeePaymentController( IEmployeePaymentRepository employeePaymentRepository)
        {
            _employeePaymentRepository = employeePaymentRepository;
        }
        #endregion

        #region Public Methods
                /**
        * @api {get} /api/SalaryModule and Employee /:get all employee details with Salary information
        * @apiName GetEmployeeWithSalaryDetail
        * @apiGroup EmployeePaymentDetail
        *    
        * @apiParam {Number} SalaryID id of the employee.
        * 
        * @apiSuccess {int} EmployeeId EmployeeId of the employee.
        * @apiSuccess {string} Basic  BasicSalary of the employee.
        * 
        * @apiSuccessExample Success-Response:
        *     {
        *   
        *       "EmployeeId": "1",
        *       "Basic: "2000"
        *     }
        *     
        * @apiError EmployeeIdNotFound The information of the employee was not found.
        * 
        */

        [HttpGet("GetEmployeeWithSalaryDetail")]
        public async Task<IActionResult> GetEmployeeWithSalaryDetail()
        {
            return Ok(await _employeePaymentRepository.GetAllEmployeepaymentDetailAsync());
        }


        /**
    * @api {get} /api/SalaryModule and Employee /:get one employee details with Salary information
    * @apiName GetEmployeepaymentDetailByIdAsync
    * @apiGroup EmployeePaymentDetail

    *    
    * @apiParam {Number} employeeID id of the employee.
    * 
    * @apiSuccess {int} EmployeeId EmployeeId of the employee.
    * @apiSuccess {string} Basic  BasicSalary of the employee.
    * 
    * @apiSuccessExample Success-Response:
    *     {
    *  
    *       "EmployeeId": "1",
    *       "Basic: "2000"
    *     }
    *     
    * @apiError EmployeeIdNotFound The information of the employee was not found.
    * 
    */

        [HttpGet("GetEmployeepaymentDetailById/{Id}")]
        public async Task<IActionResult> GetEmployeepaymentDetailByIdAsync(int Id)
        {
            return Ok(await _employeePaymentRepository.GetEmployeepaymentDetailByIdAsync(Id));
        }

        #endregion





    }

}





