using Domain.DTOs.SubmissionDTO;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.SubmissionService;

public interface ISubmissionService
{
    Task<Response<List<GetSubmissionsForAssignmentDto>>> GetSubmissionsForAssignment();
    Task<PagedResponse<List<GetSubmissionDto>>> GetSubmissionsAsync(SubmissionFilter filter);
    Task<Response<GetSubmissionDto>> GetSubmissionByIdAsync(int id);
    Task<Response<string>> AddSubmission(AddSubmissionDto submission);
    Task<Response<string>> UpdateSubmission(UpdateSubmissionDto submission);
    Task<Response<bool>> DeleteSubmission(int id);
}
