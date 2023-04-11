using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.SalaryDetailsDTO;
using EmployeeManagmentApplication.Modal.Models.Salary;
using EmployeeManagmentApplicationRepository.Salary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryModuleController : ControllerBase
    {

        #region Private Member
        private readonly ISalaryModuleRepository _SalaryModuleRepository;
        #endregion

        #region Constructor
        public SalaryModuleController(ISalaryModuleRepository salaryModuleRepository)
        {
            _SalaryModuleRepository = salaryModuleRepository;
        }
        #endregion

        #region Public Methods
        /**
        * @api {get}/api/Salary/ salary of all employee information.
        * @apiName GetSalaryModuleAsync
        * @apiGroup SalaryModule
        *    
        *  @apiSuccess : List Of salary details.
        *  
          * @apiSuccessExample Success-Response:Ok{object[]}:
        *     
        */
        [HttpGet("salary")]
        public async Task<ActionResult<List<SalaryDetailsDTO>>> GetSalaryModuleAsync()
        {
            return Ok(await _SalaryModuleRepository.GetAllSalaryAsync());
   
        }
        /**
        * @api {get} /api/Salary/ Id get one particuler salary information
        * @apiName GetSalaryModuleByIDAsync.
        * @apiGroup SalaryModule
        *    
        * @apiParam {Number} SalaryID id of the employee.
        * 
        * @apiSuccess : Showing salary detail of employee.
        *     
        * 
        * @apiSuccess 200 OK.
        */
        [HttpGet("salarybyID/{Id}")]

        public async Task<IActionResult> GetSalaryModuleByIDAsync(int Id)
        {
            return Ok(await _SalaryModuleRepository.GetSalaryByIDAsync(Id));
        }

        /**
        * @api {post} api/Salary/add salary Method to add employee salary detail.
        * 
        * @apiSuccess 200 OK.

        */
        [HttpPost("addsalary")]

        public async Task<ActionResult<EmployeeSalary>> AddSalaryModuleAsync([FromBody] SalaryDetailsDTO salaryModule)
        {
            var result = await _SalaryModuleRepository.AddSalaryAsync(salaryModule);

            if (result.SalaryId == 1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }

        /**
        * @api {put} /Salary/ Modify slarymodule information.
        * @apiName UpdateSalaryModuleAsync.
        * @apiGroup SalaryModule
        *
        * @apiParam :{object[]} 
        *
        *  }
        * @apiSuccess 200 OK.
        */

        [HttpPut("updatesalary")]
        public async Task<IActionResult> UpdateSalaryModuleAsync([FromBody] SalaryDetailsDTO salaryModule)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _SalaryModuleRepository.UpdateSalaryAsync(salaryModule);
           
            if(response is null)
            {
                return NotFound();
            }
            return Ok("Updated Successfully");
        }


        #endregion
    }
}
