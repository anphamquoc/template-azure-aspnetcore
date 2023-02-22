using FileManagementProject.Models.FileModel;

namespace FileManagementProject.Repository;

public interface IFileRepository
{
    FileDto CreateFile(CreateFileRequest fileRequest);
    FileDto? UpdateFile(Guid id, UpdateFileRequest fileRequest);
    FileDto? DeleteFile(Guid id);
}