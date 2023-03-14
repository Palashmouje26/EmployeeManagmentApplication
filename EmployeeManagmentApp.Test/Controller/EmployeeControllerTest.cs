using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using Moq;
using FluentAssertions;
using Xunit;
using EmployeeManagmentApplication.Repository;
using System.Threading.Tasks;
using EmployeeManagmentApplication.Modal.Modals;
using EmployeeManagmentApplication.Data;
using EmployeeManagmentApplication;
using AutoMapper;
using EmployeeManagmentApplication.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentApp.Test.Controller
{
   
    public class EmployeeControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly EmployeeController _employeeController;

        public EmployeeControllerTest()
        {
            _fixture = new Fixture();
            _employeeRepository = _fixture.Freeze<Mock<IEmployeeRepository>>();
           // _employeeController = new EmployeeController(_employeeRepository.Object);

        }

        //[Fact]
        //public async Task GetEmployee_whenDataFound()
        //{
        //    //Arrange
        //    var employeeMock =  _fixture.Create<IEnumerable<Employee>>();
        //    _employeeRepository.Setup(x => x.GetAllEmployeeAsync()).ReturnsAsync(employeeMock);
        //    //Act
        //     var result = await _employeeController.Get().ConfigureAwait(false);
        //    //Assert
        //    //Assert.Null(result);
        //    result.Should().NotBeNull();
        //    result.Should().BeAssignableTo<ActionResult<IEnumerable<Employee>>>();
        //    result.Should().BeAssignableTo<OkObjectResult>();
        //    result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(employeeMock.GetType());

        //}
    }
}
