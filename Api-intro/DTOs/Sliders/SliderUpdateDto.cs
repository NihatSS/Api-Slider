using FluentValidation;

namespace Api_intro.DTOs.Sliders
{
    public class SliderUpdateDto
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public IFormFile Image { get; set; }
    }

    public class SliderUpdateDtoValidator : AbstractValidator<SliderCreateDto>
    {
        public SliderUpdateDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("Title can't be null");
            RuleFor(x => x.Desc).NotNull().WithMessage("Description can't be null");
            RuleFor(x => x.Image).NotNull().WithMessage("Image can't be null");
        }
    }
}
