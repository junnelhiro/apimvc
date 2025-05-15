using apihiro.Controllers;
using apihiro.Models.Entities;
using AutoMapper;

namespace apihiro.Db
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<MyModel,Employee>() ;

        }
    }
}
