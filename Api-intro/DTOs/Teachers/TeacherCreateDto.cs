using FluentValidation;

namespace Api_intro.DTOs.Teachers
{
    public class TeacherCreateDto
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int GroupId { get; set; }
    }


    public class TeacherCreateDtoValidator : AbstractValidator<TeacherCreateDto>
    {
        public TeacherCreateDtoValidator()
        {
            RuleFor(x => x.FullName).NotNull().NotEmpty().WithMessage("FullName can't be null or empty");
            RuleFor(x=>x.Address).NotNull().WithMessage("Address can't be null");
            RuleFor(x=>x.Age).NotNull().WithMessage("Address can't be null");
            RuleFor(x=>x.Email).NotNull().WithMessage("Address can't be null");
        }
    }
}
