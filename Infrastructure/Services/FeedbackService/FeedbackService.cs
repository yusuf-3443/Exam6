using System.Net;
using AutoMapper;
using Domain.DTOs.FeedbackDTO;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.FeedbackService;

public class FeedbackService(DataContext context, IMapper mapper) : IFeedbackService
{   
     public async Task<Response<List<GetFeedbackForAssignmentDto>>> GetFeedbackFroAssignment()
    {
        try
        {
            string Assignment = "Exercise1";
            var query = await (from f in context.Feedbacks
            join a in context.Assignments on f.AssignmentId equals a.Id
            where a.Title == Assignment
            select new GetFeedbackForAssignmentDto 
            {
                FeedbackText = f.Text
            }).ToListAsync();

            var list = new List<GetFeedbackForAssignmentDto>();

            foreach (var f in query)
            {
                var feedback = new GetFeedbackForAssignmentDto()
                {
                    FeedbackText = f.FeedbackText
                };
                list.Add(feedback);
            }
            return new Response<List<GetFeedbackForAssignmentDto>>(list);
        }
        catch (Exception e)
        {
            return new Response<List<GetFeedbackForAssignmentDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetFeedbackDto>>> GetFeedbacks(FeedbackFilter filter)
    {
        try
        {
            var feedbacks = context.Feedbacks.AsQueryable();

            if(!string.IsNullOrEmpty(filter.Text))
                feedbacks = feedbacks.Where(x => x.Text.ToLower().Contains(filter.Text.ToLower()));

            var response = await feedbacks
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();

            var totalRecord = feedbacks.Count();
            var mapped = mapper.Map<List<GetFeedbackDto>>(response);

            return new PagedResponse<List<GetFeedbackDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetFeedbackDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetFeedbackDto>> GetFeedbackById(int id)
    {
        try
        {
            var feedback = await context.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);
            if (feedback == null)
                return new Response<GetFeedbackDto>(HttpStatusCode.BadRequest, "Not found");

            var mapped = mapper.Map<GetFeedbackDto>(feedback);
            return new Response<GetFeedbackDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetFeedbackDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> AddFeedback(AddFeedbackDto feedback)
    {
        try
        {
            var mapped = mapper.Map<FeedBack>(feedback);
            await context.Feedbacks.AddAsync(mapped);

            var save = await context.SaveChangesAsync();
            if (save > 0)
                return new Response<string>("Successfully");

            return new Response<string>(HttpStatusCode.BadRequest, "Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateFeedback(UpdateFeedbackDto feedback)
    {
        try
        {
            var mapped = mapper.Map<FeedBack>(feedback);
            context.Feedbacks.Update(mapped);

            var save = await context.SaveChangesAsync();
            if (save > 0)
                return new Response<string>("Successfully");

            return new Response<string>(HttpStatusCode.BadRequest, "Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }    
    }

    public async Task<Response<bool>> DeleteFeedback(int id)
    {
        try
        {
            var feedback = await context.Feedbacks.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (feedback > 0)
                return new Response<bool>(true);

            return new Response<bool>(HttpStatusCode.BadRequest, "Not found");
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }



}

