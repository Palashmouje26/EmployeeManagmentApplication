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
        * @api {get}/api/SalaryModuleController /:List of  all salary information show.
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
            return Ok(await _SalaryModuleRepository.GetAllSalaryModuleAsync());
   
        }
        /**
        * @api {get} /api/SalaryModuleController /:Id get one particuler salary information
        * @apiName GetSalaryModuleByIDAsync.
        * @apiGroup SalaryModule
        *    
        * @apiParam {Number} SalaryID id of the employee.
        * 
        * @apiSuccess : Showing salary detail of employee.
        *     
        * @apiError EmployeeIdNotFound The information of the employee was not found.
        * 
        * @apiSuccess 200 OK.
        */
        [HttpGet("salarybyID/{Id}")]

        public async Task<IActionResult> GetSalaryModuleByIDAsync(int Id)
        {
            return Ok(await _SalaryModuleRepository.GetSalaryModuleByIDAsync(Id));
        }

        /**
    * @api {post} /SalaryModule/ add or insert new salary 
    * @apiName AddSalaryModuleAsync
    * @apiGroup SalaryModuyle
    * @apiBody {int} salaryId             Mandatory to insert salaryId  of the SalaryModule.
    * @apiBody {int} EmployeeId           Mandatory to insert employeeId for the particular employee
    * @apiBody {double} Basic             Mandatory to insert basic Salary  for the employee.
    * @apiBody {double} HRA               Optional to insert.
    * @apiBody {double} DA                Optional to insert.
    * @apiBody {double} PT                Optional to insert.
    * @apiBody {double} Deduction         Optional to insert.
    * @apiBody {String} NetSalary         Optional to insert.
    * 
    * @apiSuccess 200 OK.
    */
        [HttpPost("addsalary")]

        public async Task<ActionResult<SalaryModule>> AddSalaryModuleAsync([FromBody] SalaryDetailsDTO salaryModule)
        {
            var result = await _SalaryModuleRepository.AddSalaryAsync(salaryModule);

            if (result.SalaryId == 1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }

        /**
        * @api {put} /SalaryModuleController/ Modify slarymodule information.
        * @apiName UpdateSalaryModuleAsync.
        * @apiGroup SalaryModule
        *
        * @apiParam {int} id           Salary unique ID.
        * @apiParam {int} [EmployeeId  employeeId of the employee.
        * @apiParam {Number} Basic      new basic salary inpur
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
            var response = await _SalaryModuleRepository.UpdateSalaryModuleAsync(salaryModule);
           
            if(response is null)
            {
                return NotFound();
            }
            return Ok("Updated Successfully");
        }

            /**
          * @api {delete} /api/SalaryModuleController /:Id get one particuler employee salary information to delete.
          * @apiName DeleteSalaryAsync.
          * @apiGroup SalaryModule.
          *    
          * @apiRoute {Number}  Id used for remove salary detail from storage.
          *
          * @apiSuccess 200 OK.
          */
        [HttpDelete("deletesalary")]
        public async Task<ActionResult> DeleteSalaryAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value Enter Invalid");
            }
            var result =  await _SalaryModuleRepository.SalaryModuleRemoveAsync(id);

            return Ok("Remove Successfully");
        }

        #endregion
    }
}
