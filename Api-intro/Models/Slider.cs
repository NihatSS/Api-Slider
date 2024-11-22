using System.ComponentModel.DataAnnotations.Schema;

namespace Api_intro.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
