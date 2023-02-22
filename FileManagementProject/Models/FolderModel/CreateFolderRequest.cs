using System.ComponentModel.DataAnnotations;

namespace FileManagementProject.Models.FolderModel;

public class CreateFolderRequest
{
    [Required] public string Name { get; set; }

    [Required] public string ModifiedBy { get; set; }

    public Guid? ParentId { get; set; }
}