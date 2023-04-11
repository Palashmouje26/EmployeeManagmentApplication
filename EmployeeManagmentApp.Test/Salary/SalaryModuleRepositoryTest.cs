using AutoFixture;
using EmployeeManagmentApplication.Controllers;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.SalaryDetailsDTO;
using EmployeeManagmentApplicationRepository.Data;
using EmployeeManagmentApplicationRepository.Salary;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagmentApp.Test.NewFolder
{
    public class SalaryModuleRepositoryTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<ISalaryModuleRepository> _salaryModuleRepository;
        private readonly SalaryModuleController _salaryModuleController;
        private readonly Mock<IDataRepository> _dataReposatory = new Mock<IDataRepository>();


        public SalaryModuleRepositoryTest()
        {
            _fixture = new Fixture();
            _salaryModuleRepository = _fixture.Freeze<Mock<ISalaryModuleRepository>>();

            _salaryModuleController = new SalaryModuleController(_salaryModuleRepository.Object);
        }


        [Fact]
        public async Task GetEmployeeAsync()
        {
            //Arrange
            //var employeeMock = _fixture.Create<List<EmployeeDetail>>();
            _salaryModuleRepository.Setup(x => x.GetAllSalaryAsync()).ReturnsAsync(new List<SalaryDetailsDTO>() { new SalaryDetailsDTO(), new SalaryDetailsDTO() }); ;

            //Act
            var result = await _salaryModuleController.GetSalaryModuleAsync();

            //Assert
            var value = result.Result as OkObjectResult;
            var value2 = value.Value as List<SalaryDetailsDTO>;
            Assert.Equal(2, value2.Count);
        }
        [Fact]
        public async Task GetSalaryModuleByIDAsync_ShouldReturnEmployee()
        {

            //Arrange
            var SalaryModuleDetails = new SalaryDetailsDTO
            {
                SalaryId = 1,
                EmployeeId = 1,
                Basic = 2000
            };
            _salaryModuleRepository.Setup(x => x.GetSalaryByIDAsync(SalaryModuleDetails.SalaryId)).ReturnsAsync(SalaryModuleDetails);

            //Act
            var result = await _salaryModuleController.GetSalaryModuleByIDAsync(1);

            //Assert
            Assert.NotNull(result);
        }

    }

}
