using System.ComponentModel.DataAnnotations;

namespace FileManagementProject.Models.FileModel;

public class CreateFileRequest
{
    [Required] public string Name { get; set; }

    [Required] public string Extension { get; set; }

    [Required] public Guid FolderId { get; set; }

    [Required] public string ModifiedBy { get; set; }
}