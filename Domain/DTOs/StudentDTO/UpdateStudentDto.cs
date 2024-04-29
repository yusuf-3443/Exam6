namespace Domain.DTOs.StudentDTO;

public class UpdateStudentDto
{
        public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
