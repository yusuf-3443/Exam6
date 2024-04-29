using Domain.DTOs.SubmissionDTO;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.SubmissionService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("submission")]
[ApiController]
public class SubmissionController : ControllerBase
{
    private readonly ISubmissionService submissionService;

    public SubmissionController(ISubmissionService submissionService)
    {
        this.submissionService = submissionService;
    }

    [HttpGet("get-submission-of-one-assignment")]
    public async Task<Response<List<GetSubmissionsForAssignmentDto>>> GetSubmissionsForAssignment()
    {
        return await submissionService.GetSubmissionsForAssignment();
    }

    [HttpGet("get-submissions")]
    public async Task<Response<List<GetSubmissionDto>>> GetSubmissionsAsync([FromQuery] SubmissionFilter filter)
    {
        return await submissionService.GetSubmissionsAsync(filter);
    }

    [HttpGet("{submissionId:int}")]
    public async Task<Response<GetSubmissionDto>> GetSubmissionByIdAsync(int submissionId)
    {
        return await submissionService.GetSubmissionByIdAsync(submissionId);
    }

    [HttpPost("add-submission")]
    public async Task<Response<string>> AddSubmissionAsync(AddSubmissionDto submissionDto)
    {
        return await submissionService.AddSubmission(submissionDto);
    }

    [HttpPut("update-submission")]
    public async Task<Response<string>> UpdateSubmissionAsync(UpdateSubmissionDto submissionDto)
    {
        return await submissionService.UpdateSubmission(submissionDto);
    }

    [HttpDelete("{submissionId:int}")]
    public async Task<Response<bool>> DeleteSubmissionAsync(int submissionId)
    {
        return await submissionService.DeleteSubmission(submissionId);
    }
}

