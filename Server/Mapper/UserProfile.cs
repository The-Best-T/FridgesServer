using Entities.Models;
using Entities.Dto.User;
namespace AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForRegistrationDto,User>();
        }
    }
}
