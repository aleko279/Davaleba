using AutoMapper;

namespace Davaleba.Models
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UserCustomClass, User>();
            CreateMap<UserCustomClass, User>();

        }
    }
}
