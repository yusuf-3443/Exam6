namespace Infrastructure.AutoMapper;
using Domain.Entities;
using Domain.DTOs.StudentDTO;
using Domain.DTOs.CourseDTO;
using global::AutoMapper;
using Domain.DTOs.AssignmentDTO;
using Domain.DTOs.MaterialDTO;
using Domain.DTOs.FeedbackDTO;
using Domain.DTOs.SubmissionDTO;


public class MapperProfile : Profile
{
    
    public MapperProfile()
    {
        CreateMap<Student, AddStudentDto>().ReverseMap();
        CreateMap<Student, GetStudentDto>().ReverseMap();
        CreateMap<Student, UpdateStudentDto>().ReverseMap();
                CreateMap<Course, AddCourseDto>().ReverseMap();
                CreateMap<Course, GetCourseDto>().ReverseMap();
                CreateMap<Course, UpdateCourseDto>().ReverseMap();
        CreateMap<Assignment, AddAssignmentDto>().ReverseMap();
        CreateMap<Assignment, GetAssignmentDto>().ReverseMap();
        CreateMap<Assignment, UpdateAssignmentDto>().ReverseMap();
        CreateMap<Material, AddMaterialDto>().ReverseMap();
        CreateMap<Material, GetMaterialDto>().ReverseMap();
        CreateMap<Material, UpdateMaterialDto>().ReverseMap();
                CreateMap<FeedBack, AddFeedbackDto>().ReverseMap();
                CreateMap<FeedBack, GetFeedbackDto>().ReverseMap();
                CreateMap<FeedBack, UpdateFeedbackDto>().ReverseMap();
                CreateMap<Submission, AddSubmissionDto>().ReverseMap();
                CreateMap<Submission, GetSubmissionDto>().ReverseMap();
                CreateMap<Submission, UpdateSubmissionDto>().ReverseMap();
                



    }
}

