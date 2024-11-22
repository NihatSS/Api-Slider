using System.ComponentModel.DataAnnotations;

namespace Api_intro.DTOs.Student
{
    public class StudentCreateDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
