namespace Sample_Api.Controllers
{
    using Sample_Api.Models;

    using Microsoft.AspNetCore.Mvc;
    using Sample_Api.Services.CharacterService;
    using Sample_Api.Dtos;

    [ApiController]
    [Route("[controller]")]
    public class CharacterController: ControllerBase
    {
        private readonly ICharacterService _characterService;
      public CharacterController(ICharacterService characterService)
      {
          _characterService=characterService;
      }

        [HttpGet("Character")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingleCharacter(int id)
        {
            return Ok(await _characterService.GetCharacter(id:id));
        }

        [HttpGet("Characters")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAllCharacters(){
            return Ok(await _characterService.GetCharacters());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto objCharacter){
            return Ok(await _characterService.AddCharacters(objCharacter:objCharacter));
        }
        
          [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto objUpdateCharacter){
            return Ok(await _characterService.UpdateCharacter(objUpdateCharacter:objUpdateCharacter));
        }

         [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id){
            return Ok(await _characterService.DeleteCharacter(id:id));
        }
    }
}