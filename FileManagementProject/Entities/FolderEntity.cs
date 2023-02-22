using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FileManagementProject.Entities;

public class FolderEntity : BaseEntity
{
    public FolderEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
        Name = string.Empty;
        Files = new List<FileEntity>();
        SubFolders = new List<FolderEntity>();
    }

    [Key] public Guid Id { get; set; }

    [Required] public string Name { get; set; }

    public Guid? ParentId { get; set; }

    [JsonIgnore] public virtual FolderEntity ParentFolder { get; set; }

    public virtual ICollection<FolderEntity> SubFolders { get; set; }

    public virtual ICollection<FileEntity> Files { get; set; }
}