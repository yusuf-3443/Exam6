using Domain.DTOs.AssignmentDTO;
using Domain.Responses;
using Infrastructure.Services.AssignmentService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class AssignmentController : ControllerBase
{
    private readonly IAssignmentService assignmentService;

    public AssignmentController(IAssignmentService assignmentService)
    {
        this.assignmentService = assignmentService;
    }

    [HttpGet("get-assignments")]
    public async Task<Response<List<GetAssignmentDto>>> GetAssignmentsAsync([FromQuery] AssignmentFilter filter)
    {
        return await assignmentService.GetAssignments(filter);
    }

    [HttpGet("{assignmentId:int}")]
    public async Task<Response<GetAssignmentDto>> GetAssignmentByIdAsync(int assignmentId)
    {
        return await assignmentService.GetAssignmentById(assignmentId);
    }

    [HttpPost("add-assignment")]
    public async Task<Response<string>> AddAssignmentAsync(AddAssignmentDto assignmentDto)
    {
        return await assignmentService.AddAssignment(assignmentDto);
    }

    [HttpPut("update-assignment")]
    public async Task<Response<string>> UpdateAssignmentAsync(UpdateAssignmentDto assignmentDto)
    {
        return await assignmentService.UpdateAssignment(assignmentDto);
    }

    [HttpDelete("{assignmentId:int}")]
    public async Task<Response<bool>> DeleteAssignmentAsync(int assignmentId)
    {
        return await assignmentService.DeleteAssignment(assignmentId);
    }
}

