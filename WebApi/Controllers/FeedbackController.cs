using Domain.DTOs.FeedbackDTO;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.FeedbackService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("feedback")]
[ApiController]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        this.feedbackService = feedbackService;
    }

    [HttpGet("get-feedback-for-assignment")]
    public async Task<Response<List<GetFeedbackForAssignmentDto>>> GetFeedbackForAssignment()
    {
        return await feedbackService.GetFeedbackFroAssignment();
    }

    [HttpGet("get-feedbacks")]
    public async Task<Response<List<GetFeedbackDto>>> GetFeedbacksAsync([FromQuery] FeedbackFilter filter)
    {
        return await feedbackService.GetFeedbacks(filter);
    }

    [HttpGet("{feedbackId:int}")]
    public async Task<Response<GetFeedbackDto>> GetFeedbackByIdAsync(int feedbackId)
    {
        return await feedbackService.GetFeedbackById(feedbackId);
    }

    [HttpPost("add-feedback")]
    public async Task<Response<string>> AddFeedbackAsync(AddFeedbackDto feedbackDto)
    {
        return await feedbackService.AddFeedback(feedbackDto);
    }

    [HttpPut("update-feedback")]
    public async Task<Response<string>> UpdateFeedbackAsync(UpdateFeedbackDto feedbackDto)
    {
        return await feedbackService.UpdateFeedback(feedbackDto);
    }

    [HttpDelete("{feedbackId:int}")]
    public async Task<Response<bool>> DeleteFeedbackAsync(int feedbackId)
    {
        return await feedbackService.DeleteFeedback(feedbackId);
    }
}
