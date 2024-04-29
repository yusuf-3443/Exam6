namespace Domain.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public List<FeedBack> FeedBacks { get; set; }
    public List<Submission> Submissions { get; set; }
    public List<Assignment> Assignments { get; set; }
}
