using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample_Api.Data;
using Sample_Api.Dtos;
using Sample_Api.Models;

namespace Sample_Api.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacters(AddCharacterDto objCharacter)
        {
            var objServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character chars = _mapper.Map<Character>(objCharacter);
            _context.Characters.Add(chars);
            await _context.SaveChangesAsync();
            objServiceResponse.data =await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return objServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacter(int id)
        {
            var objServiceResponse = new ServiceResponse<GetCharacterDto>();
            var characters =await _context.Characters.FirstOrDefaultAsync(c=>c.Id==id);
            objServiceResponse.data = _mapper.Map<GetCharacterDto>(characters);
            return objServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters()
        {
            var objServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var characters=await _context.Characters.ToListAsync();
            objServiceResponse.data =await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return objServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto objUpdateCharacter)
        {
            var objServiceResponse = new ServiceResponse<GetCharacterDto>();
            Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == objUpdateCharacter.Id);
            if(character!=null){
            character.Name = objUpdateCharacter.Name;
            character.Defense = objUpdateCharacter.Defense;
            character.HitPoints = objUpdateCharacter.HitPoints;
            character.Strength = objUpdateCharacter.Strength;
            character.Intelligence = objUpdateCharacter.Intelligence;
            character.CharacterType = objUpdateCharacter.CharacterType;
            
            await _context.SaveChangesAsync();
            objServiceResponse.data = _mapper.Map<GetCharacterDto>(character);
            }
            else
            {
                objServiceResponse.data = _mapper.Map<GetCharacterDto>(character);
                objServiceResponse.success=false;
                objServiceResponse.message="Id: "+objUpdateCharacter.Id+" Does Not Exists";
            }
            return objServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            Character character =await _context.Characters.FirstAsync(c => c.Id == id);
            character.IsActive = false;
            await _context.SaveChangesAsync();
            var objServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            objServiceResponse.data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return objServiceResponse;
        }
    }
}