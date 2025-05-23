using apihiro.Controllers;
using apihiro.Models.Entities;
using AutoMapper;

namespace apihiro.Db
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<MyModel, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MiddleName, opt => opt.Ignore())
                .ForMember(dest => dest.manualColumn, opt => opt.Ignore())
                .ReverseMap()
                ; 
            CreateProjection<Employee, MyModel>();
        }
    }
}
