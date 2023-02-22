using FileManagementProject.Models.FolderModel;

namespace FileManagementProject.Repository;

public interface IFolderRepository
{
    FolderDto? GetFolderById(Guid id);
    FolderDto? CreateFolder(CreateFolderRequest folder);
    FolderDto? UpdateFolder(Guid id, UpdateFolderRequest folder);
    FolderDto? DeleteFolder(Guid id);
}