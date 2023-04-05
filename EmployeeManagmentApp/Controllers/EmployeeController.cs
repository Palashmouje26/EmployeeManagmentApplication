using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeeDetailsDTO;
using EmployeeManagmentApplication.Repository.EmployeeManagmentRepository;
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
        * @api {get} /api/EmployeeController /:get all employee information
        * @apiName GetEmployeeAsync
        * @apiGroup Employee
        *    
        *  @apiSuccess : List Of  employee details.
        *  
          * @apiSuccessExample Success-Response:{object[]}  :
        *     
        * @apiError EmployeeNotFound the information of the employee was not found.
        * 
        */
        [HttpGet("employee")]
        public async Task<ActionResult> GetEmployeeAsync()
        {
            return Ok(await _EmployeeRepository.GetAllEmployeeAsync());
        }

         /**
        * @api {get} /api/EmployeeController /:id get one particuler employee information
        * @apiName GetEmployeeByIDAsync
        * @apiGroup Employee
        *    
        * @apiParam {Number}  Id of the employee.
        * 
        * @apiSuccess : Show particuler empolyee details.

        *    
        * @apiError EmployeeNotFound The id of the employee was not found.
        */
        [HttpGet("employeebyId/{Id}")]
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

        [HttpPost("addemployee")]

        public async Task<ActionResult> AddEmployeeAsync([FromForm] EmployeeDetailsDTO employee)
        {
            var result = await _EmployeeRepository.AddEmployeeAsync(employee);

            if (result.EmployeeId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        /**
         * @api {put} /EmployeeController/ Modify Employee information
         * @apiName UpdateEmployeeAsync
         * @apiGroup Employee
         *
         * @apiParam :{object[]} 
         * 
         * @apiUse EmployeeIDNotFoundError
         */
        [HttpPut("updateemployee")]
        public async Task<ActionResult> UpdateEmployeeAsync([FromBody] EmployeeDetailsDTO employee)
        {
            if (employee.EmployeeId != employee.EmployeeId)
            {
                return BadRequest();
            }
            await _EmployeeRepository.UpdateEmployeeAsync(employee);
            return Ok("Update Successfully");
        }

        /**
       * @api {put} /EmployeeController/ Modify Employee Active or Inactive
       * @apiName UpdateSoftdeleteAsync
       * @apiGroup Employee

       * @apiSuccessExample Success-Response:
       *  { 
       *      employeeId = 1,
       *  }
       */
        [HttpPut("removebyId/{Id}")]
        public async Task<ActionResult> UpdateSoftdeleteAsync(int Id)
        {
            await _EmployeeRepository.UpdateByStatusAsync(Id);
            return Ok("Update Successfully");
        }

        /**
        * @api {delete} /api/EmployeeController /:id get one particuler employee information
        * @apiName DeleteEmployeeAsync
        * @apiGroup employee
        *    
        * @apiParam: Id of the employee.
        * 
        * @apiSuccess {String} firstname Firstname of the employee.
        * @apiSuccess {String} lastname  Lastname of the employee.
        * 
        
        * @apiError EmployeeNotFound The id of the employee was not found.
        * 
        */
        [HttpDelete("remove/{id}")]
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