namespace FileManagementProject.Entities;

public class BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; }
}