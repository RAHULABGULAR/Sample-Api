using Sample_Api.Models;

namespace Sample_Api.Dtos
{
    public class UpdateCharacterDto
    {
        public int Id { get; set; }   
        public string Name { get; set; }="Frodo";
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass CharacterType { get; set; }=RpgClass.Knight;
         public DateTime UpdatedDate { get; set; }=DateTime.Now;
    }
}