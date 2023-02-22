using FileManagementProject.Entities;
using FileManagementProject.Models.FileModel;
using FileManagementProject.Repository;

namespace FileManagementProject.Services;

public class FileService : IFileRepository
{
    private readonly MyDbContext _context;

    public FileService(MyDbContext context)
    {
        _context = context;
    }

    public FileDto CreateFile(CreateFileRequest fileRequest)
    {
        var file = new FileEntity
        {
            Name = fileRequest.Name,
            Extension = fileRequest.Extension,
            FolderId = fileRequest.FolderId,
            ModifiedBy = fileRequest.ModifiedBy
        };

        _context.Files.Add(file);
        _context.SaveChanges();


        var fileDto = new FileDto(file);


        return fileDto;
    }

    public FileDto? UpdateFile(Guid id, UpdateFileRequest fileRequest)
    {
        var file = _context.Files.SingleOrDefault(file => file.Id.Equals(id));

        if (file == null) return null;

        file.Name = fileRequest.Name;
        file.Extension = fileRequest.Extension;
        file.ModifiedAt = DateTime.Now;
        file.ModifiedBy = fileRequest.ModifiedBy;

        _context.Files.Update(file);
        _context.SaveChanges();

        var fileDto = new FileDto(file);

        return fileDto;
    }

    public FileDto? DeleteFile(Guid id)
    {
        var file = _context.Files.SingleOrDefault(file => file.Id.Equals(id));

        if (file == null) return null;

        _context.Files.Remove(file);
        _context.SaveChanges();

        var fileDto = new FileDto(file);

        return fileDto;
    }

    public FileDto? GetFileById(Guid id)
    {
        var file = _context.Files.SingleOrDefault(file => file.Id.Equals(id));
        if (file == null) return null;

        var fileDto = new FileDto(file);

        return fileDto;
    }

    public List<FileDto> GetAllFiles()
    {
        var files = _context.Files.ToList();
        var fileDtos = files.Select(file => new FileDto(file)).ToList();

        return fileDtos;
    }
}