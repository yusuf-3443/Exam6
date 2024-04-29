namespace Domain.DTOs.FeedbackDTO;

public class UpdateFeedbackDto
{
     public int Id { get; set; }
    public int AssignmentId { get; set; }
    public int StudentId { get; set; }
    public string Text { get; set; }
    public DateTime FeedbackDate { get; set; }
}
