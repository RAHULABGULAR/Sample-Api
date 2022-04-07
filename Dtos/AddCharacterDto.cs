using Sample_Api.Models;

namespace Sample_Api.Dtos
{
    public class AddCharacterDto
    {
        public string Name { get; set; }="Frodo";
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass CharacterType { get; set; }=RpgClass.Knight;
        public int UserId{get;set;}
    }
}