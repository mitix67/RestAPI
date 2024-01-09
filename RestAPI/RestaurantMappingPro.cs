using AutoMapper;
using RestAPI.Entities;
using RestAPI.Models;


namespace RestAPI
{
    public class RestaurantMappingPro : Profile
    {
        public RestaurantMappingPro()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.ZipCode, c => c.MapFrom(s => s.Address.ZipCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address()
                {
                    City = dto.City,
                    Street = dto.Street,
                    ZipCode = dto.ZipCode
                }));
        }
    }
}
