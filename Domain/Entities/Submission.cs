namespace Domain.Entities;

public class Submission
{
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public int StudentId { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string Context { get; set; }
    public Assignment  Assignment { get; set; }
    public Student Student { get; set; }

}
