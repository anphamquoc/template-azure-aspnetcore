using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagementProject.Entities;

public class FileEntity : BaseEntity
{
    public FileEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
        ModifiedBy = string.Empty;
        Name = string.Empty;
        Extension = string.Empty;
        FolderId = Guid.Empty;
        Folder = null;
    }

    [Key] public Guid Id { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Name must be at least 6 characters")]
    public string Name { get; set; }

    [Required] public string Extension { get; set; }

    public Guid FolderId { get; set; }

    [ForeignKey("FolderId")] public FolderEntity? Folder { get; set; }
}