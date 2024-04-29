using System.Net;
using AutoMapper;
using Domain.DTOs.StudentDTO;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StudentService;


public class StudentService(DataContext context, IMapper mapper) : IStudentService
{

        public async Task<Response<List<GetStudentWithCountOfSubmissionsDto>>> GetStudentWithCountOfSubmissions()
    {
        try
        {
            var query =await (from s in context.Submissions
            join st in context.Students on s.StudentId equals st.Id
            group st by st.Name into student
            let countS = context.Submissions.Count(x=>x.Id>0)
            orderby countS  
            select new GetStudentWithCountOfSubmissionsDto
            {
                Name = student.Key,
                Count = countS

            }).ToListAsync();
 var list = new List<GetStudentWithCountOfSubmissionsDto>();
        foreach (var q in query)
        {
            var course = new GetStudentWithCountOfSubmissionsDto()
            {
                Name = q.Name,
                Count = q.Count
            };
            list.Add(course);
        }
        return new Response<List<GetStudentWithCountOfSubmissionsDto>>(list);

        }
        catch (Exception e)
        {
            return new Response<List<GetStudentWithCountOfSubmissionsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    public async Task<PagedResponse<List<GetStudentDto>>> GetStudentsAsync(StudentFilter filter)
    {
        try
        {
            var students = context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetStudentDto>>(response);
            return new PagedResponse<List<GetStudentDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetStudentDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }



    public async Task<Response<GetStudentDto>> GetStudentByIdAsync(int id)
    {
        try
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
                return new Response<GetStudentDto>(HttpStatusCode.BadRequest, "Student not found");
            var mapped = mapper.Map<GetStudentDto>(student);
            return new Response<GetStudentDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetStudentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }



    public async Task<Response<string>> CreateStudentAsync(AddStudentDto student)
    {
        try
        {
            var existingStudent = await context.Students.FirstOrDefaultAsync(x => x.Email == student.Email);
            if (existingStudent != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Student already exists");
            var mapped = mapper.Map<Student>(student);

            await context.Students.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new student");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> UpdateStudentAsync(UpdateStudentDto student)
    {
        try
        {
            var mappedStudent = mapper.Map<Student>(student);
            context.Students.Update(mappedStudent);
            var update= await context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "Student not found");
            return new Response<string>("Student updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    

    public async Task<Response<bool>> DeleteStudentAsync(int id)
    {
        try
        {
            var student = await context.Students.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (student == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Student not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    

}