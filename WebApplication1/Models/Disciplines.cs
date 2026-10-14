namespace WebApplication1.Models
{
    public class Disciplines
    {
        public int DisciplineId { get; set; }       
        public string? DisciplineName { get; set; }
        public bool DisciplineIsDeleted { get; set; }

        public ICollection<Grades>? Grades { get; set; }
    }
}
