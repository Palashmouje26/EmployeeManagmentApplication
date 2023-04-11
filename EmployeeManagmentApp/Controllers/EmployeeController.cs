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

        private readonly IEmployeeRepository _employeeRepository;

        #endregion

        #region Constructor
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

        }
        #endregion

        #region Public Methods

        /**
        * @api {get} /api/employee/ employee all employee information.
        * @apiName GetEmployeeAsync
        * @apiGroup Employee
        *    
        *  @apiSuccess : List Of  employee details.
        *  
        * @apiSuccessExample Success-Response:{object[]}  :
        * 
        */

        [HttpGet("employee")]
        public async Task<ActionResult> GetEmployeeAsync()
        {
            return Ok(await _employeeRepository.GetAllEmployeeAsync());
        }

        /**
        * @api {get} /api/Employee /:id get one particuler employee information
        * @apiName GetEmployeeByIDAsync.
        * @apiGroup Employee
        *    
        * @apiParam {Number}  Id of the employee.
        * 
        * @apiSuccess : Show particuler empolyee details.

        */

        [HttpGet("employeebyId/{Id}")]
        public async Task<IActionResult> GetEmployeeByIDAsync([FromRoute] int Id)
        {
            return Ok(await _employeeRepository.GetEmployeeByIdAsync(Id));
        }

        /**
        *   @api {post} api/Employee/addemployee Method to add employee detail.
        *   
        *   @apiBody {object} Employee detail.
        */

        [HttpPost("addemployee")]
        public async Task<ActionResult> AddEmployeeAsync([FromForm] EmployeeDetailsDTO employee)
        {
            var result = await _employeeRepository.AddEmployeeAsync(employee);

            if (result.EmployeeId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Added Successfully");
        }
        /**
         * @api {put} /Employee/ Modify Employee information.
         * @apiName UpdateEmployeeAsync
         * @apiGroup Employee
         *
         * @apiParam :{object[]} 
         * 
         * @apiUse Employee ID Not Found Error
         */
        [HttpPut("updateemployee")]
        public async Task<ActionResult> UpdateEmployeeAsync([FromBody] EmployeeDetailsDTO employee)
        {
            if (employee.EmployeeId != employee.EmployeeId)
            {
                return BadRequest();
            }
            await _employeeRepository.UpdateEmployeeAsync(employee);
            return Ok("Update Successfully");
        }

       /**
       * @api {put} /Employee/ Modify Employee Active or Inactive.
       * @apiName UpdateSoftdeleteAsync
       * @apiGroup Employee

       * @apiSuccessExample Success-Response:
       */
        [HttpPut("updatesoftdelete/{Id}")]
        public async Task<ActionResult> UpdateSoftdeleteAsync(int Id)
        {
            await _employeeRepository.UpdateByStatusAsync(Id);
            return Ok("Update Successfully");
        }
      
        #endregion
    }

}