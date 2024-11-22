using System.ComponentModel.DataAnnotations;

namespace Api_intro.DTOs.Groups
{
    public class GroupCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
