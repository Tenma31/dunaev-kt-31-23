namespace WebApplication1.Models
{
    public class Grades
    {
        public int GradeId { get; set; }
        public int GradeValue { get; set; }

        public int StudentId { get; set; }
        public int DisciplineId { get; set; }
        public Student? Student { get; set; }
        public Disciplines? Discipline { get; set; }
    }
}
