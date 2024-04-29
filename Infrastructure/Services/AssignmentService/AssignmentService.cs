using System.Net;
using AutoMapper;
using Domain.DTOs.AssignmentDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.AssignmentService;

public class AssignmentService(DataContext context, IMapper mapper) : IAssignmentService
{
    public async Task<PagedResponse<List<GetAssignmentDto>>> GetAssignments(AssignmentFilter filter)
    {
        try
        {
            var assignments = context.Assignments.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                assignments = assignments.Where(x=>x.Title.ToLower().Contains(filter.Title.ToLower()));

            var response = await assignments
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();

            var totalRecord = assignments.Count();
            var mapped = mapper.Map<List<GetAssignmentDto>>(response);

            return new PagedResponse<List<GetAssignmentDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetAssignmentDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetAssignmentDto>> GetAssignmentById(int id)
    {
        try
        {
            var assignment = await context.Assignments.FirstOrDefaultAsync(x => x.Id == id);
            if (assignment == null) 
                return new Response<GetAssignmentDto>(HttpStatusCode.BadRequest, "Assignment not found");

            var mapped = mapper.Map<GetAssignmentDto>(assignment);
            return new Response<GetAssignmentDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetAssignmentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> AddAssignment(AddAssignmentDto assignment)
    {
        try
        {
            var mapped = mapper.Map<Assignment>(assignment);
            await context.Assignments.AddAsync(mapped);

            var save = await context.SaveChangesAsync();
            if (save > 0) 
                return new Response<string>("Assignment added successfully");

            return new Response<string>(HttpStatusCode.BadRequest, "Failed to add assignment");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateAssignment(UpdateAssignmentDto assignment)
    {
        try
        {
            var mapped = mapper.Map<Assignment>(assignment);
            context.Assignments.Update(mapped);

            var save = await context.SaveChangesAsync();
            if (save > 0) 
                return new Response<string>("Assignment updated successfully");

            return new Response<string>(HttpStatusCode.BadRequest, "Failed to update assignment");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteAssignment(int id)
    {
        try
        {
            var assignment = await context.Assignments.Where(x => x.Id == id).ExecuteDeleteAsync();
            
            if (assignment > 0) 
                return new Response<bool>(true);

            return new Response<bool>(HttpStatusCode.BadRequest, "Assignment not found");
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    }
