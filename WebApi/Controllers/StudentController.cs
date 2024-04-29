using Domain.DTOs.StudentDTO;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.StudentService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService studentService;

    public StudentController(IStudentService studentService)
    {
        this.studentService = studentService;
    }

    [HttpGet("get-student-with-count-of-submissions")]
    public async Task<Response<List<GetStudentWithCountOfSubmissionsDto>>> GetStudentWithCountOfSubmissions()
    {
        return await studentService.GetStudentWithCountOfSubmissions();
    }

    [HttpGet("get-students")]
    public async Task<Response<List<GetStudentDto>>> GetStudentsAsync([FromQuery] StudentFilter filter)
    {
        return await studentService.GetStudentsAsync(filter);
    }

    [HttpGet("{studentId:int}")]
    public async Task<Response<GetStudentDto>> GetStudentByIdAsync(int studentId)
    {
        return await studentService.GetStudentByIdAsync(studentId);
    }

    [HttpPost("add-student")]
    public async Task<Response<string>> AddStudentAsync(AddStudentDto studentDto)
    {
        return await studentService.CreateStudentAsync(studentDto);
    }

    [HttpPut("update-student")]
    public async Task<Response<string>> UpdateStudentAsync(UpdateStudentDto studentDto)
    {
        return await studentService.UpdateStudentAsync(studentDto);
    }

    [HttpDelete("{studentId:int}")]
    public async Task<Response<bool>> DeleteStudentAsync(int studentId)
    {
        return await studentService.DeleteStudentAsync(studentId);
    }
}

