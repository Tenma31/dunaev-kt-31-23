namespace WebApplication1.Models
{
    public class Groups
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int GroupCourse { get; set; }
        public int SpeacialtyId { get; set; }
        public bool GroupisDeleted { get; set; } 
        public Specialty Specialty { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
