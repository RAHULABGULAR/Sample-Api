using Sample_Api.Dtos;
using Sample_Api.Models;

namespace Sample_Api.Services.CharacterService
{
    public interface ICharacterService
    {
       Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters();
         Task<ServiceResponse<GetCharacterDto>> GetCharacter(int id);
         Task<ServiceResponse<List<GetCharacterDto>>> AddCharacters(AddCharacterDto objCharacter);
         Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto objUpdateCharacter);
         Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}