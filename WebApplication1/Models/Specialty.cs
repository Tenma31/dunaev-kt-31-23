namespace WebApplication1.Models
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public string SpecialtyTitle { get; set; }
        public int Specialtycode { get; set; }
        public ICollection<Groups> Groups { get; set; }
    }
}
