using System;
using AutoMapper;
namespace CRUD_Functionality
{
	public class MappingConfig : Profile
    {
		public MappingConfig()
		{
			CreateMap<Employee, EmployeeDTO>().ReverseMap();
			CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();
			CreateMap<Employee, EmployeeAddDTO>().ReverseMap();
		}
	}
}

