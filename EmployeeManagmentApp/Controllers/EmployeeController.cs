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



        public EmployeeController(IDataReposatory _datarepository, IEmployeeRepository employeeRepository,IMapper mapper,IWebHostEnvironment webHostEnvironment)
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

        public async Task<ActionResult> Post([FromBody] EmployeeDetail employee)
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

        //[HttpDelete]
        ////[HttpDelete("{id}")]
        //[Route("DeleteEmployee")]
        //public JsonResult Delete(int id)
        //{
        //    _EmployeeRepository.RemoveRangeAsync(id);
        //    return new JsonResult("Deleted Successfully");
        //}

        ////Product Image Upload Process //
        //[HttpPost("[action]")]
        //public async Task<ActionResult<Employee>> UploadFiles([FromForm]  EmployeeProfiles employeeProfiles)
        //{


        //    if (employeeProfiles.EmployeeProfilePhoto.Length <= 2097152)  // File size checking up to 2 mb  //
        //    {
        //        var fileExtenstion = EmployeeProfiel(employeeProfiles.EmployeeProfilePhoto); // file will be checking is extansion formate //
        //        if (!fileExtenstion)
        //        {
        //            return Ok("The file is too large.");
        //        }

        //        var directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath + "\\ProfileImages\\");   // receving the image path tho save //

        //        var EmployeeDetail = new Employee
        //        {
        //            EmployeeFirstName = employeeProfiles.EmployeeFirstName,
        //            EmployeeLastName = employeeProfiles.EmployeeLastName,
        //            EmailId = employeeProfiles.EmailId,
        //            Address = employeeProfiles.Address,
        //            PhoneNumber = employeeProfiles.PhoneNumber,
        //            AdharCard = employeeProfiles.AdharCard,
        //            PanCard = employeeProfiles.PanCard,
        //            EmployeeProfilePhoto = employeeProfiles.EmployeeProfilePhoto.FileName,
        //            Status = employeeProfiles.Status
        //        };

        //        var filePath = Path.Combine(directoryPath, employeeProfiles.EmployeeProfilePhoto.FileName);
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            employeeProfiles.EmployeeProfilePhoto.CopyTo(stream);
        //        }
        //        await _EmployeeRepository.InsertEmployee(EmployeeDetail);

        //        return Ok("Image Save  Successfully");
        //    }
        //    return Ok("The file is too large.");
        //}

        //private bool EmployeeProfiel(IFormFile pmployeeprofilePhoto)  // checking the file is jpg ,jpeg and png formate // 
        //{
        //    var supportedtype = new[] { ".jpg", ".jpeg", ".png" };
        //    var fileExtension = Path.GetExtension(pmployeeprofilePhoto.FileName);
        //    if (!supportedtype.Contains(fileExtension))
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }





