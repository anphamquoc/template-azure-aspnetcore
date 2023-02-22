using FileManagementProject.Entities;
using FileManagementProject.Models.FileModel;

namespace FileManagementProject.Models.FolderModel;

public class FolderDto : BaseEntity
{
    public FolderDto(FolderEntity folderEntity)
    {
        Id = folderEntity.Id;
        Name = folderEntity.Name;
        ParentId = folderEntity.ParentId ?? Guid.Empty;
        SubFolders = folderEntity.SubFolders;
        Files = folderEntity.Files.Select(file => new FileDto(file)).ToList();
        CreatedAt = folderEntity.CreatedAt;
        ModifiedAt = folderEntity.ModifiedAt;
        ModifiedBy = folderEntity.ModifiedBy;
    }

    public Guid Id { get; }
    public string Name { get; }
    public Guid ParentId { get; }
    public ICollection<FolderEntity> SubFolders { get; }
    public ICollection<FileDto> Files { get; }
}