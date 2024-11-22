using FluentValidation;

namespace Api_intro.DTOs.Teachers
{
    public class TeacherEditDto
    {
        public string FullName { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

}
