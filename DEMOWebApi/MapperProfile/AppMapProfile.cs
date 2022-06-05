using AutoMapper;
using Demo.Dtos.Common;
using Demo.Dtos.Request;
using Demo.Dtos.Response;
using Demo.Models;
using Demo.ViewModels.Common;
using Demo.ViewModels.Request;
using Demo.ViewModels.Response;

namespace DEMOWebApi.MapperProfile
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            
            CreateMap<AddStudentViewModel, AddStudentDto>();
            CreateMap<AddStudentDto, Student>();

            CreateMap<EditStudentViewModel, EditStudentDto>().ReverseMap();
            CreateMap<Student, EditStudentDto>();

            CreateMap<StudentMarks, ReturnStudentMarksDto>();
            CreateMap<ReturnStudentMarksDto, ReturnStudentMarksViewModel>();

            CreateMap<AddStudentMarksViewModel, AddStudentMarksDto>();
            CreateMap<AddStudentMarksDto, StudentMarks>();

            CreateMap<EditStudentMarksViewModel, EditStudentMarksDto>();
            CreateMap<StudentMarks, EditStudentMarksDto>();





            CreateMap<GetFilteredRecordsViewModel, GetFilteredRecordsDto>();
            CreateMap<SearchFilterViewModel, SearchFilterDto>();

            CreateMap<StudentMarks, ReturnStudentMarksDto>();
            CreateMap<ReturnStudentMarksDto, ReturnStudentMarksViewModel>();

            
            CreateMap<Student, EditStudentDto>();
            CreateMap<AddStudentAndMarksViewModel, AddStudentAndMarksDto>();
            CreateMap<AddStudentAndMarksDto, StudentMarks>();
            CreateMap<AddStudentAndMarksViewModel, AddStudentDto>();

            CreateMap<ReturnStudentAndMarksDto, ReturnStudentAndMarksViewModel>();
            CreateMap<Student, ReturnStudentAndMarksDto>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id)); ;
            CreateMap<StudentMarks, ReturnStudentAndMarksDto>();
            



        }
    }
}
