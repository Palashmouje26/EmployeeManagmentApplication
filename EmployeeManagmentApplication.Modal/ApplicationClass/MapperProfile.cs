using AutoMapper;
using EmployeeManagmentApplication.Modal.Modals;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagmentApplication.Modal.ApplicationClass
{
    public  class MapperProfile : Profile
    {
       public MapperProfile() 
       {
            CreateMap<EmployeeDetail, Employee>().ReverseMap();
            //CreateMap<Employee, SalaryModule>().ReverseMap();
            //CreateMap<SalaryModuleDetails, SalaryModule>().ReverseMap();
       }
    }
}
