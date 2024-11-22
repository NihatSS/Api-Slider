using Microsoft.AspNetCore.Http;

namespace Api_intro.DTOs.Sliders
{
    public class SliderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
    }
}
