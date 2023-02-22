using FileManagementProject.Models.FileModel;
using FileManagementProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileManagementProject.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FileController : Controller
{
    private readonly IFileRepository _fileRepository;

    public FileController(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    [HttpPost]
    public IActionResult CreateFile([FromBody] CreateFileRequest createFileRequest)
    {
        try
        {
            var name = User.FindFirst("name")?.Value;

            createFileRequest.ModifiedBy = name ?? User.Identity.Name ?? "Unknown";

            var file = _fileRepository.CreateFile(createFileRequest);

            return StatusCode(StatusCodes.Status201Created, file);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateFile(Guid id, [FromBody] UpdateFileRequest updateFileRequest)
    {
        try
        {
            var name = User.FindFirst("name")?.Value;

            updateFileRequest.ModifiedBy = name ?? User.Identity.Name ?? "Unknown";

            var file = _fileRepository.UpdateFile(id, updateFileRequest);

            if (file == null) return NotFound();

            return Ok(file);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteFile(Guid id)
    {
        try
        {
            var file = _fileRepository.DeleteFile(id);

            if (file == null) return NotFound();

            return Ok(file);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}