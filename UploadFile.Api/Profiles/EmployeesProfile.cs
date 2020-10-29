using AutoMapper;
using UploadFile.Models;

namespace UploadFileApi.Profiles
{
    /// <summary>
    /// Defines Automapper Profiles.
    /// </summary>
    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}