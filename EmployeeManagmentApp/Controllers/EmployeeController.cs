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
    public class EmployeeController : ControllerBase
    {

        private readonly IDataReposatory _DataReposatory;
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public EmployeeController(IDataReposatory _datarepository, IEmployeeRepository employeeRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {

            _DataReposatory = _datarepository;
            _EmployeeRepository = employeeRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;

        }


        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _EmployeeRepository.GetAllEmployeeAsync());
        }


        [HttpGet("GetEmployeeByID/{Id}")]

        public async Task<IActionResult> GetDeptById(int Id)
        {
            return Ok(await _EmployeeRepository.GetEmployeeByIdAsync(Id));
        }


        [HttpPost("AddEmployee")]

        public async Task<ActionResult> Post([FromForm] EmployeeDetail employee)
        {
            var result = await _EmployeeRepository.AddEmployeeAsync(employee);

            if (result.EmployeeId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }


        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult> Put([FromBody] EmployeeDetail employee)
        {
            if (employee.EmployeeId != employee.EmployeeId)
            {
                return BadRequest();
            }
            await _EmployeeRepository.UpdateEmployeeAsync(employee);
            return Ok("Update Successfully");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value Enter Invalid");
            }

            var eemp = await _EmployeeRepository.EmployeeRemoveAsync(id);

            return Ok("Remove Successfully");
        }



    }

}





