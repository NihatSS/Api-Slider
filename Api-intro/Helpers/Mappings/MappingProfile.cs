using Api_intro.DTOs.City;
using Api_intro.DTOs.Countries;
using Api_intro.DTOs.Groups;
using Api_intro.DTOs.Sliders;
using Api_intro.DTOs.Student;
using Api_intro.DTOs.Teachers;
using Api_intro.Models;
using AutoMapper;

namespace Api_intro.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<CountryCreateDto, Country>();
            CreateMap<CountryEditDto, Country>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null );
                });


            CreateMap<City, CityDto>();
            CreateMap<CityCreateDto, City>();
            CreateMap<CityEditDto, City>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<Group, GroupDto>();
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupEditDto, Group>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });


            CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name));

            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentEditDto, Student>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherCreateDto, Teacher>();
            CreateMap<TeacherEditDto, Teacher>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            CreateMap<TeacherAddToGroupDto, TeacherGroup>();

            CreateMap<Slider, SliderDto>();
            CreateMap<SliderCreateDto, Slider>()
               .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<SliderUpdateDto, Slider>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.FileName))
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
        }
    }
}
