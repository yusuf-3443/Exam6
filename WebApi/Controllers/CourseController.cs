using Domain.DTOs.CourseDTO;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.CourseService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]

public class CourseController(ICourseService courseService):ControllerBase
{
    [HttpGet("get-course-with-count-of-materials")]
    public async Task<Response<List<GetCourseWithCountOfMaterialsDto>>> GetCourseWithCountOfMaterials()
    {
        return await courseService.GetCourseWithCountOfMaterials();
    }

    [HttpGet("get-course-with-materials")]
    public async Task<Response<List<GetCourseWithMaterialsDto>>> GetCourseWithMaterials()
    {
        return await courseService.GetCourseWithMaterials();
    }

    [HttpGet("get-courses")]
    public async Task<Response<List<GetCourseDto>>> GetCoursesAsync([FromQuery]CourseFilter filter)
    {
        return await courseService.GetCourses(filter);
    }

    [HttpGet("{courseId:int}")]
    public async Task<Response<GetCourseDto>> GetCourseByIdAsync(int courseId)
    {
        return await courseService.GetCourseById(courseId);
    }

    [HttpPost("add-course")]
    public async Task<Response<string>> AddCourseAsync(AddCourseDto courseDto)
    {
        return await courseService.AddCourse(courseDto);
    }

    [HttpPut("update-course")]
    public async Task<Response<string>> UpdateCourseAsync(UpdateCourseDto courseDto)
    {
        return await courseService.UpdateCourse(courseDto);
    }

    [HttpDelete("{courseId:int}")]
    public async Task<Response<bool>> DeleteCourseAsync(int courseId)
    {
        return await courseService.DeleteCourse(courseId);
    }

} 
