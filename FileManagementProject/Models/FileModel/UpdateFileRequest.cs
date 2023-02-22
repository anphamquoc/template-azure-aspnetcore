namespace FileManagementProject.Models.FileModel;

public class UpdateFileRequest
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public string ModifiedBy { get; set; }
}