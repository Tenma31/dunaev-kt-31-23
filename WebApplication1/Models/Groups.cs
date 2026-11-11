using System.Text.RegularExpressions;
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

        public bool IsValidGroupName()
        {
            return Regex.IsMatch(GroupName ?? string.Empty, @"^\d{2}\.\d{2}\.\d{2}$");
        }
    }
}
