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
        private readonly IDataReposatory _dataRepository;


        public SalaryModuleController( ISalaryModuleRepository salaryModuleRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment, ISalaryDataRepository ISalaryDataRepository,IDataReposatory dataRepository)
        {
           
            _SalaryModuleRepository = salaryModuleRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
           _SalaryDataRepository = ISalaryDataRepository;
            _dataRepository = dataRepository;
        }


      
        [HttpGet("GetSalaryModule")]
    
        public async Task<ActionResult<SalaryModule>> GetSalaryModule()
        {
            return Ok(await _SalaryModuleRepository.GetAllSalaryModuleAsync());
   
        }



        [HttpGet("GetSalaryModuleByID/{Id}")]

        public async Task<IActionResult> GetDeptById(int Id)
        {
            return Ok(await _SalaryModuleRepository.GetSalaryModuleByIDAsync(Id));
        }

        [HttpPost]

        [Route("AddSalaryModule")]

        public async Task<ActionResult<SalaryModule>> Post([FromBody] SalaryModuleDetails salaryModule)
        {
            var result = await _SalaryModuleRepository.AddSalaryAsync(salaryModule);

            if (result.SalaryId == 1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }

        [HttpPut]
        [Route("UpdateSalaryModule")]
        public async Task<IActionResult> Put([FromBody] SalaryModuleDetails salaryModule)
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

       
        [HttpDelete("DeleteSalaryModule")]
        public async Task<ActionResult> DeleteSalaryAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value Enter Invalid");
            }

            var eemp =  _SalaryModuleRepository.SalaryModuleRemoveAsync(id);

            return Ok("Remove Successfully");
        }



        //[HttpGet("GetPaySlip")]
        //public async Task<ActionResult<SalaryModule>> GetAllSalaryModule()
        //{
        //    return Ok(await _SalaryModuleRepository.());

        //}


    }
}
