namespace WebApplication1.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int GroupId { get; set; }
        public bool StudentIsDeleted { get; set; }
        public Groups Groups { get; set; }
        public ICollection<Grades> Grades { get; set; }
    }
}
