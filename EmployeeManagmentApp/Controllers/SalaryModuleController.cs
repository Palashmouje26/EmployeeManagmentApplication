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
       
        private readonly ISalaryDataRepository _SalaryDataRepository;
        private readonly ISalaryModuleRepository _SalaryModuleRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SalaryModuleController( ISalaryModuleRepository salaryModuleRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment, ISalaryDataRepository ISalaryDataRepository)
        {
           
            _SalaryModuleRepository = salaryModuleRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
           _SalaryDataRepository = ISalaryDataRepository;
        }


        [HttpGet]
        [Route("GetSalaryModule")]
        //public async Task<ActionResult<IEnumerable<SalaryModule>>> GetSalaryModule()
        //{
        //    return await _SalaryDataRepository.GetSalaryModule();
        //}
        //public async Task<IActionResult> GetSalaryModule()
        public async Task<ActionResult<IEnumerable<SalaryModule>>> GetSalaryModule()
        {
            return Ok(await _SalaryDataRepository.GetSalaryModule());
        }

        [HttpGet]

        [Route("GetSalaryModuoleByID/{Id}")]

        public async Task<IActionResult> GetDeptById(int Id)
        {
            return Ok(await _SalaryDataRepository.GetSalaryModuleByID(Id));
        }

        [HttpPost]

        [Route("AddSalaryModule")]

        public async Task<ActionResult<SalaryModule>> Post([FromForm] SalaryModuleDetails salaryModule)
        {
            var HRA = salaryModule.Basic + 1000;
            var DA = salaryModule.Basic * 10 / 100;
            var PT = 200;
            var Deduction = salaryModule.Basic - PT;
            var NetSalary = (salaryModule.Basic + HRA) + (DA - Deduction);
            var salaryDetal = new SalaryModule
            {
                EmployeeId = salaryModule.EmployeeId,
                Basic = salaryModule.Basic,
                HRA = HRA,
                PT = PT,
                DA = DA,
                Deduction = Deduction,
                NetSalary = NetSalary,
            };
            //var printf= ("Net Salary is:", NetSalary)

            var result = await _SalaryDataRepository.InsertSalary(salaryDetal);

            if (result.SalaryId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }

        [HttpPut]
        [Route("UpdateSalaryModuole")]
        public async Task<IActionResult> Put(SalaryModule salaryModule)
        {
            var HRA = salaryModule.Basic + 1000;
            var DA = salaryModule.Basic * 10 / 100;
            var PT = 200;
            var Deduction = salaryModule.Basic - PT;
            var NetSalary = (salaryModule.Basic + HRA) + (DA - Deduction);
            var salaryDetal = new SalaryModule
            {
                EmployeeId = salaryModule.EmployeeId,
                Basic = salaryModule.Basic,
                HRA = HRA,
                PT = PT,
                DA = DA,
                Deduction = Deduction,
                NetSalary = NetSalary,
            };
            await _SalaryDataRepository.UpdateSalary(salaryDetal);
            return Ok("Updated Successfully");
        }

        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteSalaryModuole")]
        public JsonResult Delete(int id)
        {
            _SalaryDataRepository.DeleteSalaryModule(id);
            return new JsonResult("Deleted Successfully");
        }

        //[HttpGet]

        //[Route("GetPaySlip")]
        //public async Task<ActionResult<IEnumerable<SalaryModule>>> GetSalaryModule()

        //{
        //    var x = await _SalaryDataRepository.GetSalaryModule();
        //    return Ok(x.ToList());
        //}

    }
}
