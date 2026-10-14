namespace WebApplication1.Models
{
    public class Groups
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public int GroupCourse { get; set; }
        public int SpecialtyId { get; set; }        
        public bool GroupIsDeleted { get; set; }    

        public Specialty? Specialty { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
