using System.Net;
using AutoMapper;
using Domain.DTOs.CourseDTO;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CourseService;

public class CourseService(DataContext context, IMapper mapper) : ICourseService
{

    public async Task<Response<List<GetCourseWithMaterialsDto>>> GetCourseWithMaterials()
    {
        try
        {
        var query = await (from m in context.Materials 
        join c in context.Courses on m.CourseId equals c.Id
        select new GetCourseWithMaterialsDto 
        {
            CourseTitle = c.Title,
            MaterialTitle = m.Title
        }).ToListAsync();
        var list = new List<GetCourseWithMaterialsDto>();
        foreach (var g in query)
        {
            var course = new GetCourseWithMaterialsDto()
            {
                CourseTitle = g.CourseTitle,
                MaterialTitle = g.MaterialTitle
            };
            list.Add(course);
        }
        return new Response<List<GetCourseWithMaterialsDto>>(list);

            
        }
        catch (Exception e)
        {
            return new Response<List<GetCourseWithMaterialsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetCourseDto>>> GetCourses(CourseFilter filter)
    {
        try
        {
            var courses = context.Courses.AsQueryable();

            if(!string.IsNullOrEmpty(filter.Instructor))
            courses = courses.Where(x=>x.Instructor.ToLower().Contains(filter.Instructor.ToLower()));
            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();
            var mapped = mapper.Map<List<GetCourseDto>>(response);
            return new PagedResponse<List<GetCourseDto>>(mapped,filter.PageNumber,filter.PageSize,totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetCourseDto>> GetCourseById(int id)
    {
        try
        {
            var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null) return new Response<GetCourseDto>(HttpStatusCode.BadRequest, "Not found");
            var mapped = mapper.Map<GetCourseDto>(course);
            return new Response<GetCourseDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetCourseDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> AddCourse(AddCourseDto course)
    {
        try
        {
            var mapped = mapper.Map<Course>(course);
            await context.Courses.AddAsync(mapped);
            var save = await context.SaveChangesAsync();
            if (save > 0) return new Response<string>("Successfully");
            return new Response<string>(HttpStatusCode.BadRequest, "Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateCourse(UpdateCourseDto course)
    {
        try
        {
            var mapped = mapper.Map<Course>(course);
            context.Courses.Update(mapped);
            var save = await context.SaveChangesAsync();
            if (save > 0) return new Response<string>("Successfully");
            return new Response<string>(HttpStatusCode.BadRequest,"Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }    
    }

    public async Task<Response<bool>> DeleteCourse(int id)
    {
        try
        {

        var course = await context.Courses.Where(x => x.Id == id).ExecuteDeleteAsync();
        if (course > 0) return new Response<bool>(true);
        return new Response<bool>(HttpStatusCode.BadRequest, "Not found");
        
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
        }

    public async Task<Response<List<GetCourseWithCountOfMaterialsDto>>> GetCourseWithCountOfMaterials()
    {
        try
        {
        var query = await (from m in context.Materials
        join c in context.Courses on m.CourseId equals c.Id
        let count = context.Materials.Count(x=>x.CourseId>0)
        select new GetCourseWithCountOfMaterialsDto
        {
            CourseTitle = c.Title,
            CountMaterial = count
        }).ToListAsync();
   var list = new List<GetCourseWithCountOfMaterialsDto>();
        foreach (var g in query)
        {
            var course = new GetCourseWithCountOfMaterialsDto()
            {
                CourseTitle = g.CourseTitle,
                CountMaterial = g.CountMaterial
            };
            list.Add(course);
        }
        return new Response<List<GetCourseWithCountOfMaterialsDto>>(list);


            
        }
        catch (Exception e)
        {
            return new Response<List<GetCourseWithCountOfMaterialsDto>>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

}
