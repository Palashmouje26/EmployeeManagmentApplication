using System.Collections.Generic;
using AutoFixture;
using Moq;
using FluentAssertions;
using Xunit;
using EmployeeManagmentApplication.Repository;
using System.Threading.Tasks;
using EmployeeManagmentApplication.Modal.Modals;
using EmployeeManagmentApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EmployeeManagmentApplication.Data;
using System;

namespace EmployeeManagmentApp.Test.Controller
{

    public class EmployeeRepositoryTest 
    {
        private readonly IFixture _fixture;
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly EmployeeController _employeeController;
        private readonly Mock<IDataRepository> _dataReposatory = new Mock<IDataRepository>();


        public EmployeeRepositoryTest()
        {
            _fixture = new Fixture();
            _employeeRepository = _fixture.Freeze<Mock<IEmployeeRepository>>();
            //var x = _employeeRepository.Object;
            _employeeController = new EmployeeController(_employeeRepository.Object);

        }

        //[Fact]
        //public async Task GetEmployeeAsync()
        //{

        //    //Arrange
        //    var employeeMock = _fixture.Create<List<EmployeeDetail>>();
        //    _employeeRepository.Setup(x => x.GetAllEmployeeAsync()).ReturnsAsync(employeeMock);
        //    //Act
        //    var result = await _employeeController.GetEmployeeAsync();
        //    //Assert

        //    result.Should().NotBeNull();
        //    result.Should().BeAssignableTo<ActionResult<List<EmployeeDetail>>>();
        //    result.Should().BeAssignableTo<OkObjectResult>();
        //    result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(employeeMock.GetType());
        //    _employeeRepository.Verify(x => x.GetAllEmployeeAsync(), Times.Once());
        //}

        [Fact]
        public async Task GetEmployeeAsync()
        {
            //Arrange
            //var employeeMock = _fixture.Create<List<EmployeeDetail>>();
            _employeeRepository.Setup(x => x.GetAllEmployeeAsync()).ReturnsAsync(new List<EmployeeDetail>() { new EmployeeDetail(), new EmployeeDetail() }); ;

            //Act
            var result = await _employeeController.GetEmployeeAsync();

            //Assert
         
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var employees = Assert.IsType<List<EmployeeDetail>>(viewResult.Value);
            Assert.Equal(2, employees.Count);
        }

        [Fact]
       public async Task GetEmployeeByIdAsync_ShouldReturnEmployee()
        {

            //Arrange
            var employeedeatil = new EmployeeDetail
           {
               EmployeeId = 1,
               EmployeeFirstName = "Test",
           };
           _employeeRepository.Setup(x => x.GetEmployeeByIdAsync(employeedeatil.EmployeeId)).ReturnsAsync(employeedeatil);
            
            //Act
            var employee = await _employeeController.GetEmployeeByIDAsync(1);
         
            //Assert
            Assert.NotNull(employee);
        }

    }
}
