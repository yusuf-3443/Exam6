using Domain.DTOs.AssignmentDTO;
using Domain.Responses;

namespace Infrastructure.Services.AssignmentService;

public interface IAssignmentService
{
    Task<PagedResponse<List<GetAssignmentDto>>> GetAssignments(AssignmentFilter filter);
    Task<Response<GetAssignmentDto>> GetAssignmentById(int id);
    Task<Response<string>> AddAssignment(AddAssignmentDto assignment);
    Task<Response<string>> UpdateAssignment(UpdateAssignmentDto assignment);
    Task<Response<bool>> DeleteAssignment(int id);
}
