using AutoMapper;
using PhoneBookAPI.Data.Entities;
using PhoneBookAPI.Models;
using PhoneBookAPI.Models.Users;

namespace PhoneBookAPI.Common
{
    public class AutoMapperProfile:Profile
    {


        public AutoMapperProfile()
        {
            CreateMap<UserContacts, UserContactsViewModel>();
            CreateMap<UserContactInputModel, UserContacts>();
            CreateMap<UserContactUpdateModel, UserContacts>();
            CreateMap<Users, UserViewModel>();
            CreateMap<UserInputModel, Users>();
        }
    }
}
