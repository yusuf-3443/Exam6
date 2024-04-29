using System.Net;
using AutoMapper;
using Domain.DTOs.SubmissionDTO;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.SubmissionService;

public class SubmissionService(DataContext context, IMapper mapper) : ISubmissionService
{

    public async Task<Response<List<GetSubmissionsForAssignmentDto>>> GetSubmissionsForAssignment()
    {
try
{
    string Assignment = "Fixing";
    var query = await (from s in context.Submissions
    join a in context.Assignments on s.AssignmentId equals a.Id
    where a.Title == Assignment
    select new GetSubmissionsForAssignmentDto
    {
        SubmissionContext = s.Context
    }).ToListAsync();

    var list = new List<GetSubmissionsForAssignmentDto>();
     foreach (var s in query)
        {
            var submission = new GetSubmissionsForAssignmentDto()
            {
                SubmissionContext = s.SubmissionContext
            };
            list.Add(submission);
        }
        return new Response<List<GetSubmissionsForAssignmentDto>>(list);
}
catch (Exception e)
{
    return new Response<List<GetSubmissionsForAssignmentDto>>(HttpStatusCode.InternalServerError, e.Message);
}
    }

    public async Task<PagedResponse<List<GetSubmissionDto>>> GetSubmissionsAsync(SubmissionFilter filter)
    {
        try
        {
            var submissions = context.Submissions.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Context))
                submissions = submissions.Where(x => x.Context.ToLower().Contains(filter.Context.ToLower()));

            var response = await submissions
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = submissions.Count();

            var mapped = mapper.Map<List<GetSubmissionDto>>(response);
            return new PagedResponse<List<GetSubmissionDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetSubmissionDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetSubmissionDto>> GetSubmissionByIdAsync(int id)
    {
        try
        {
            var submission = await context.Submissions.FirstOrDefaultAsync(x => x.Id == id);
            if (submission == null)
                return new Response<GetSubmissionDto>(HttpStatusCode.BadRequest, "Submission not found");
            var mapped = mapper.Map<GetSubmissionDto>(submission);
            return new Response<GetSubmissionDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetSubmissionDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> AddSubmission(AddSubmissionDto submission)
    {
        try
        {
            var mapped = mapper.Map<Submission>(submission);

            await context.Submissions.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new submission");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateSubmission(UpdateSubmissionDto submission)
    {
        try
        {
            var mappedSubmission = mapper.Map<Submission>(submission);
            context.Submissions.Update(mappedSubmission);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "Submission not found");
            return new Response<string>("Submission updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteSubmission(int id)
    {
        try
        {
            var submission = await context.Submissions.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (submission == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Submission not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }



}

