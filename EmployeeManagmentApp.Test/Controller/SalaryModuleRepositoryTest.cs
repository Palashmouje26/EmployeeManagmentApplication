using AutoFixture;
using EmployeeManagmentApplication.Controllers;
using EmployeeManagmentApplication.Data;
using EmployeeManagmentApplication.Modal.Modals;
using EmployeeManagmentApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagmentApp.Test.Controller
{
    public class SalaryModuleRepositoryTest 
    {
        private readonly Fixture _fixture;
        private readonly Mock<ISalaryModuleRepository> _SalaryModuleRepository;
        private readonly SalaryModuleController _SalaryModuleController;
        private readonly Mock<IDataRepository> _dataReposatory = new Mock<IDataRepository>();


        public SalaryModuleRepositoryTest()
        {
            _fixture = new Fixture();
            _SalaryModuleRepository = _fixture.Freeze<Mock<ISalaryModuleRepository>>();
           
            _SalaryModuleController = new SalaryModuleController(_SalaryModuleRepository.Object);
        }


        [Fact]
        public async Task GetEmployeeAsync()
        {
            //Arrange
            //var employeeMock = _fixture.Create<List<EmployeeDetail>>();
            _SalaryModuleRepository.Setup(x => x.GetAllSalaryModuleAsync()).ReturnsAsync(new List<SalaryModuleDetails>() { new SalaryModuleDetails(), new SalaryModuleDetails() }); ;

            //Act
            var result = await _SalaryModuleController.GetSalaryModuleAsync();

            //Assert
            var value = result.Result as OkObjectResult;
            var value2 = value.Value as List<SalaryModuleDetails>;
            Assert.Equal(2, value2.Count);
        }
        [Fact]
        public async Task GetSalaryModuleByIDAsync_ShouldReturnEmployee()
        {

            //Arrange
            var SalaryModuleDetails = new SalaryModuleDetails
            {
                SalaryId = 1,
                EmployeeId =1,
                Basic= 2000
            };
            _SalaryModuleRepository.Setup(x => x.GetSalaryModuleByIDAsync(SalaryModuleDetails.SalaryId)).ReturnsAsync(SalaryModuleDetails);

            //Act
            var result = await _SalaryModuleController.GetSalaryModuleByIDAsync(1);

            //Assert
            Assert.NotNull(result);
        }

    }

}
