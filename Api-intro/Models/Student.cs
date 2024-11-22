namespace Api_intro.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }
        public int? GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<GroupStudents> StudentGroups { get; set; }
    }
}
