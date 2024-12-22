using AutoMapper;
using ObserverFire.Models;
using ObserverFire.Entities;
using ObserverFire.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserViewModel>().ForMember(dest => dest.ApiKey, opt => opt.MapFrom(src => 
            src.ApiKey != null && !string.IsNullOrEmpty(src.ApiKey) ? EncryptionHelper.Decrypt(src.ApiKey, src.ApiKeyTag) : null));
    }
}