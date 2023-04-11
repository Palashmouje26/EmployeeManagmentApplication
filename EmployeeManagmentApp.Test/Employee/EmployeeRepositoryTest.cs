using System.Collections.Generic;
using AutoFixture;
using Moq;
using Xunit;
using System.Threading.Tasks;
using EmployeeManagmentApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagmentApplication.Repository.EmployeeManagmentRepository;
using EmployeeManagmentApplicationRepository.Data;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeeDetailsDTO;

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
            _employeeController = new EmployeeController(_employeeRepository.Object);
        }


        [Fact]
        public async Task GetEmployeeAsync()
        {
            //Arrange
            //var employeeMock = _fixture.Create<List<EmployeeDetail>>();
            _employeeRepository.Setup(x => x.GetAllEmployeeAsync()).ReturnsAsync(new List<EmployeeDetailsDTO>() { new EmployeeDetailsDTO(), new EmployeeDetailsDTO() });

            //Act
            var result = await _employeeController.GetEmployeeAsync();

            //Assert
         
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var employees = Assert.IsType<List<EmployeeDetailsDTO>>(viewResult.Value);
            Assert.Equal(2, employees.Count);
        }

        [Fact]
       public async Task GetEmployeeByIdAsync_ShouldReturnEmployee()
        {

            //Arrange
            var employeedeatil = new EmployeeDetailsDTO
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
        [Fact]
        public async Task DeleteEmployeeAsync_RemoveSoft()
        {
            // Arrange
            int testId = 2;

            // Act
            var result = await _employeeController.RemoveEmployeeById(testId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Read", redirectToActionResult.ActionName);
            _employeeRepository.Verify();
        }
    }
}
