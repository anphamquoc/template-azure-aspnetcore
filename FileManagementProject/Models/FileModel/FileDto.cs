using FileManagementProject.Entities;

namespace FileManagementProject.Models.FileModel;

public class FileDto : BaseEntity
{
    public FileDto(FileEntity file)
    {
        Id = file.Id;
        Name = file.Name;
        Extension = file.Extension;
        FolderId = file.FolderId;
        CreatedAt = file.CreatedAt;
        ModifiedAt = file.ModifiedAt;
        ModifiedBy = file.ModifiedBy;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public Guid FolderId { get; set; }
}