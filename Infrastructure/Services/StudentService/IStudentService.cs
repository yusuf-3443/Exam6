using Domain.DTOs.StudentDTO;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.StudentService;

public interface IStudentService
{
    Task<Response<List<GetStudentWithCountOfSubmissionsDto>>> GetStudentWithCountOfSubmissions();
    Task<PagedResponse<List<GetStudentDto>>> GetStudentsAsync(StudentFilter filter);
    Task<Response<GetStudentDto>> GetStudentByIdAsync(int id);
    Task<Response<string>> CreateStudentAsync(AddStudentDto student);
    Task<Response<string>> UpdateStudentAsync(UpdateStudentDto student);
    Task<Response<bool>> DeleteStudentAsync(int id);
}
