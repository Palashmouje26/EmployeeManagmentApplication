using AutoMapper;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeeDetailsDTO;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeePaymentDetailsDTO;
using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.SalaryDetailsDTO;
using EmployeeManagmentApplication.Modal.Models.EmployeeManagment;
using EmployeeManagmentApplication.Modal.Models.Salary;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagmentApplication.Modal.ApplicationClass.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EmployeeDetailsDTO, Employee>().ReverseMap();
            CreateMap<SalaryDetailsDTO, SalaryModule>().ReverseMap();
            CreateMap<Employee, EmployeePaymentDetailsDTO>().ReverseMap();
            CreateMap<SalaryModule, SalaryModule>().ReverseMap();
            //CreateMap<Employee, SalaryModule>().ReverseMap();

        }
    }
}
