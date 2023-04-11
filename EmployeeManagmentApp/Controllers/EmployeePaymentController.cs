using EmployeeManagmentApplication.Repository.EmployeeManagmentPaymentRepository;
using Microsoft.AspNetCore.Mvc;
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
        * @api {get} /api/EmployeePayment/: All employee details with Salary information.
        * @apiName GetEmployeeWithSalaryDetail.
        * @apiGroup EmployeePaymentDetail.
        * 
        * @apiSuccessExample Success-Response:
        *     {
        *       "EmployeeId": "1",
        *       "Basic: "2000"
        *     }
        */
        [HttpGet("employeewithsalarydetail")]
        public async Task<IActionResult> GetEmployeeWithSalaryDetail()
        {
            return Ok(await _employeePaymentRepository.GetAllEmployeepaymentDetailAsync());
        }
        /**
        * @api {get} /api/EmployeePayment/:One employee details with Salary information.
        * @apiName GetEmployeepaymentDetailByIdAsync.
        * @apiGroup EmployeePaymentDetail
        * 
        * @apiParam {Number} Id  of the employee.
        * 
        * @apiSuccess : Showing user payment details.
        * 
        */
        [HttpGet("paymentdetailbyId/{Id}")]
        public async Task<IActionResult> GetEmployeepaymentDetailByIdAsync(int Id)
        {
            return Ok(await _employeePaymentRepository.GetEmployeepaymentDetailByIdAsync(Id));
        }

        #endregion





    }

}





