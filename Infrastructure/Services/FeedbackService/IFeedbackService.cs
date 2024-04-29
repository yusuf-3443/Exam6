using Domain.DTOs.FeedbackDTO;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.FeedbackService;

public interface IFeedbackService
{
    Task<Response<List<GetFeedbackForAssignmentDto>>> GetFeedbackFroAssignment();
Task<PagedResponse<List<GetFeedbackDto>>> GetFeedbacks(FeedbackFilter filter);
Task<Response<GetFeedbackDto>> GetFeedbackById(int id);
Task<Response<string>> AddFeedback(AddFeedbackDto feedback);
Task<Response<string>> UpdateFeedback(UpdateFeedbackDto feedback);
Task<Response<bool>> DeleteFeedback(int id);

}
