namespace WebApplication1.Models
{
    public class Grades
    {
        public int GradeId { get; set; }
        public int GradeValue { get; set; }

        public int StudentId { get; set; }
        public int DisciplineId { get; set; }
        public Student Students { get; set; }
        public Disciplines Disciplines { get; set; }
    }
}
