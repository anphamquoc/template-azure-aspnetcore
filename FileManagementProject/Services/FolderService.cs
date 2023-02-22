using FileManagementProject.Entities;
using FileManagementProject.Models.FolderModel;
using FileManagementProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace FileManagementProject.Services;

public class FolderService : IFolderRepository
{
    private readonly MyDbContext _context;

    public FolderService(MyDbContext context)
    {
        _context = context;
    }


    public FolderDto? GetFolderById(Guid id)
    {
        var folder = _context.Folders.AsNoTracking().Include(
                folder => folder.Files).Include(
                folder => folder.SubFolders)
            .SingleOrDefault(folder => folder.Id.Equals(id));

        if (folder == null) return null;

        var folderDto = new FolderDto(folder);

        return folderDto;
    }

    public FolderDto CreateFolder(CreateFolderRequest folderRequest)
    {
        var folder = new FolderEntity();
        folder.Name = folderRequest.Name;
        folder.ParentId = folderRequest.ParentId;
        folder.ModifiedBy = folderRequest.ModifiedBy;

        _context.Folders.Add(folder);
        _context.SaveChanges();

        var folderDto = new FolderDto(folder);

        return folderDto;
    }

    public FolderDto? UpdateFolder(Guid id, UpdateFolderRequest folderRequest)
    {
        var folder = _context.Folders.SingleOrDefault(folder => folder.Id == id);

        if (folder == null) return null;

        folder.Name = folderRequest.Name;
        folder.ModifiedBy = folderRequest.ModifiedBy;
        folder.ModifiedAt = DateTime.Now;

        _context.SaveChanges();

        var folderDto = new FolderDto(folder);

        return folderDto;
    }

    public FolderDto? DeleteFolder(Guid id)
    {
        var folder = _context.Folders.SingleOrDefault(folder => folder.Id == id);

        if (folder == null) return null;

        _context.Folders.Remove(folder);
        _context.SaveChanges();

        var folderDto = new FolderDto(folder);

        return folderDto;
    }
}