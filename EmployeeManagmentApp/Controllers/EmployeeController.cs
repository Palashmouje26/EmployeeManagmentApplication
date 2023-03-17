using AutoMapper;
using EmployeeManagmentApplication.Data;
using EmployeeManagmentApplication.Modal.Modals;
using EmployeeManagmentApplication.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Private Members
        
        private readonly IEmployeeRepository _EmployeeRepository;
       
        #endregion

        #region Constructor
        public EmployeeController( IEmployeeRepository employeeRepository)
        {         
            _EmployeeRepository = employeeRepository;

        }
        #endregion

        #region Public Methods
        /**
    * @api {get} /api/employee /:get all employee information
    * @apiName GetEmployeeAsync
    * @apiGroup Employee
    *    
    * @apiParam {Number} employeeID id of the employee.
    * 
    * @apiSuccess {String} firstname Firstname of the employee.
    * @apiSuccess {String} lastname  Lastname of the employee.
    * 
    * @apiSuccessExample Success-Response:
    *     {
    *       "firstname": "John",
    *       "lastname": "Doe",
    *       "mobile : "7894561230"
    *     }
    *     
    * @apiError EmployeeNotFound The information of the employee was not found.
    * 
    */
        [HttpGet("GetEmployee")]
        public async Task<ActionResult> GetEmployeeAsync()
        {
            return Ok(await _EmployeeRepository.GetAllEmployeeAsync());
        }

     /**
    * @api {get} /api/employee /:id get one particuler employee information
    * @apiName GetEmployeeByIDAsync
    * @apiGroup Employee
    *    
    * @apiParam {Number} employeeID id of the employee.
    * 
    * @apiSuccess {String} firstname Firstname of the employee.
    * @apiSuccess {String} lastname  Lastname of the employee.
    * 
    * @apiSuccessExample Success-Response:
    *     {
    *       "firstname": "John",
    *       "lastname": "Doe"
    *     }
    *     
    * @apiError EmployeeNotFound The id of the employee was not found.
    * 
    */
        [HttpGet("GetEmployeeByID/{Id}")]
        public async Task<IActionResult> GetEmployeeByIDAsync([FromRoute] int Id)
        {
            return Ok(await _EmployeeRepository.GetEmployeeByIdAsync(Id));
        }
                /**
         * @api {post} /Employee/
         * @apiBody {String} [firstname]       Mandatory Firstname of the employee.
         * @apiBody {String} lastname          Mandatory Lastname.
         * @apiBody {String} EmailId           Mandatory  input with small letter"pa".
         * @apiBody {String} [address]         Optional nested address object.
         * @apiBody {String} PhoneNumber       Mandatory input 10 digit or number.
         * @apiBody {String} Adharcard         Mandatory input 12 digit or number.
         * @apiBody {String} Pancard           Mandatory input 10 digit or number with combination with aplphabets.
         * @apiBody {String} EmployeeProfile   upload only jpg,png,jpeg file.
         * @apiBody {bool} Status              employee is Active or Not.
         */

        [HttpPost("AddEmployee")]

        public async Task<ActionResult> AddEmployeeAsync([FromForm] EmployeeDetail employee)
        {
            var result = await _EmployeeRepository.AddEmployeeAsync(employee);

            if (result.EmployeeId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }


        /**
         * @api {put} /employee/ Modify Employee information
         * @apiName UpdateEmployeeAsync
         * @apiGroup Employee
         *
         * @apiParam {Number} id          employee unique ID.
         * @apiParam {String} [firstname] Firstname of the employee.
         * @apiParam {String} [lastname]  Lastname of the employee.
         *
         * @apiSuccessExample Success-Response:
         *  { 
         *      firstname = "John",
         *      Lasttname = "Mathev",
         *      PhoneNo = " "7894561230"
         *   
         *  }
         *
         * @apiUse EmployeeIDNotFoundError
         */
        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult> UpdateEmployeeAsync([FromBody] EmployeeDetail employee)
        {
            if (employee.EmployeeId != employee.EmployeeId)
            {
                return BadRequest();
            }
            await _EmployeeRepository.UpdateEmployeeAsync(employee);
            return Ok("Update Successfully");
        }

        /**
       * @api {put} /employee/ Modify Employee Active or Inactive
       * @apiName UpdateSoftdeleteAsync
       * @apiGroup Employee
       *
       * @apiParam {Number} id          employee unique ID.
       
       *
       * @apiSuccessExample Success-Response:
       *  { 
       *      employeeId = 1,
       *  }
       *
       */


        [HttpPut("UpdateSoftDeleteById/{Id}")]
        public async Task<ActionResult> UpdateSoftdeleteAsync(int Id)
        {
            await _EmployeeRepository.UpdateSoftdeleteByStatusAsync(Id);
            return Ok("Update Successfully");
        }

        /**
    * @api {delete} /api/employee /:id get one particuler employee information
    * @apiName DeleteEmployeeAsync
    * @apiGroup employee
    *    
    * @apiParam {Number} employeeID id of the employee.
    * 
    * @apiSuccess {String} firstname Firstname of the employee.
    * @apiSuccess {String} lastname  Lastname of the employee.
    * 
    * @apiSuccessExample Success-Response:
    *     {
    *       Remove Successfully
    *     }
    *     
    * @apiError EmployeeNotFound The id of the employee was not found.
    * 
    */

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value Enter Invalid");
            }

            var eemp = await _EmployeeRepository.EmployeeRemoveAsync(id);

            return Ok("Remove Successfully");
        }

      
        #endregion
    }

}