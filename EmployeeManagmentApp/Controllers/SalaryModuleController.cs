using AutoMapper;
using EmployeeManagmentApplication.Data;
using EmployeeManagmentApplication.Modal.Modals;
using EmployeeManagmentApplication.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        public SalaryModuleController( ISalaryModuleRepository salaryModuleRepository)
        {
           
            _SalaryModuleRepository = salaryModuleRepository;
         

        }
        #endregion

        #region Public Methods
                /**
        * @api {get} /api/SalaryModule /:get all employee Salary information
        * @apiName GetSalaryModuleAsync
        * @apiGroup SalaryModuyle
        *    
        * @apiParam {Number} SalaryID id of the employee.
        * 
        * @apiSuccess {int} EmployeeId EmployeeId of the employee.
        * @apiSuccess {string} Basic  BasicSalary of the employee.
        * 
        * @apiSuccessExample Success-Response:
        *     {
        *       "SalaryId": "1",
        *       "EmployeeId": "1",
        *       "Basic: "2000"
        *     }
        *     
        * @apiError EmployeeIdNotFound The information of the employee was not found.
        * 
        */
        [HttpGet("GetSalaryModule")]
    
        public async Task<ActionResult<List<SalaryModuleDetails>>> GetSalaryModuleAsync()
        {
            return Ok(await _SalaryModuleRepository.GetAllSalaryModuleAsync());
   
        }
                /**
        * @api {get} /api/SalaryModule /:id get one particuler salary information
        * @apiName GetSalaryModuleByIDAsync
        * @apiGroup SalaryModuyle
        *    
        * @apiParam {Number} SalaryID id of the employee.
        * 
        * @apiSuccess {int} EmployeeId EmployeeId of the employee.
        * @apiSuccess {string} Basic  BasicSalary of the employee.
        * 
        * @apiSuccessExample Success-Response:
        *     {
        *       "SalaryId": "1",
        *       "EmployeeId": "1",
        *       "Basic: "2000"
        *     }
        *     
        * @apiError EmployeeIdNotFound The information of the employee was not found.
        * 
        */
        [HttpGet("GetSalaryModuleByID/{Id}")]

        public async Task<IActionResult> GetSalaryModuleByIDAsync(int Id)
        {
            return Ok(await _SalaryModuleRepository.GetSalaryModuleByIDAsync(Id));
        }

        /**
    * @api {post} /SalaryModule/ add or insert new salary Module 
    * @apiName AddSalaryModuleAsync
    * @apiGroup SalaryModuyle
    * @apiBody {int} salaryId             Mandatory salaryId  of the SalaryModule.
    * @apiBody {int} EmployeeId           Mandatory insert employeeId for the particular employee
    * @apiBody {double} Basic             Mandatory basic Salary for the employee.
    * @apiBody {double} HRA               Optional to insert.
    * @apiBody {double} DA                Optional to insert.
    * @apiBody {double} PT                Optional to insert.
    * @apiBody {double} Deduction         Optional to insert.
    * @apiBody {String} NetSalary         Optional to insert.
    */
        [HttpPost("AddSalaryModule")]

        public async Task<ActionResult<SalaryModule>> AddSalaryModuleAsync([FromBody] SalaryModuleDetails salaryModule)
        {
            var result = await _SalaryModuleRepository.AddSalaryAsync(salaryModule);

            if (result.SalaryId == 1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }

        /**
        * @api {put} /SalaryModule/ Modify slarymodule information
        * @apiName UpdateSalaryModuleAsync
        * @apiGroup SalaryModule
        *
        * @apiParam {int} id           Salary unique ID.
        * @apiParam {int} [EmployeeId  employeeId of the employee.
        * @apiParam {Number} Basic      new basic salary inpur
        *
        * @apiSuccessExample Success-Response:
        *  { 
        *     "SalaryId": "1",
        *       "EmployeeId": "1",
        *       "Basic: "2000"
        *     }
        *   
        *  }
        *
        * @apiUse SalaryNotFoundError
        */

        [HttpPut("UpdateSalaryModule")]
        public async Task<IActionResult> UpdateSalaryModuleAsync([FromBody] SalaryModuleDetails salaryModule)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response=await _SalaryModuleRepository.UpdateSalaryModuleAsync(salaryModule);
           
            if(response is null)
            {
                return NotFound();
            }
            return Ok("Updated Successfully");
        }

            /**
          * @api {delete} /api/SalaryModule /:id get one particuler employee information
          * @apiName DeleteSalaryAsync
          * @apiGroup employee
          *    
          * @apiRoute {Number} SalaryID id of the SalaryModule
          *
          * @apiSuccess 200 OK.
          */

        [HttpDelete("DeleteSalaryModule")]
        public async Task<ActionResult> DeleteSalaryAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value Enter Invalid");
            }

            var eemp =  await _SalaryModuleRepository.SalaryModuleRemoveAsync(id);

            return Ok("Remove Successfully");
        }

        #endregion



    }
}
