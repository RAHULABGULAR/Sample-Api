using AutoMapper;
using Sample_Api.Dtos;
using Sample_Api.Models;

namespace Sample_Api
{
    public class AutoMapperProfile:Profile
    {
      public AutoMapperProfile()
      {
          CreateMap<Character,GetCharacterDto>();
          CreateMap<AddCharacterDto,Character>();
      }
    }
}