namespace Sample_Api.Models
{
    public class Character
    {
        public int Id { get; set; } 
        public string Name { get; set; }="Frodo";
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass CharacterType { get; set; }=RpgClass.Knight;
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public DateTime UpdatedDate { get; set; }=DateTime.Now;
        public bool IsActive { get; set; }=true;
        public User User { get; set; }
    }
}