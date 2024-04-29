namespace Domain.DTOs.SubmissionDTO;

public class AddSubmissionDto
{
    public int AssignmentId { get; set; }
    public int StudentId { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string Context { get; set; }
}
