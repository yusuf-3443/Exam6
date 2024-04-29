namespace Domain.Entities;

public class Material
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContentURL { get; set; }
    public Course Course { get; set; }

}
