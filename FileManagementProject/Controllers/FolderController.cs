using FileManagementProject.Models.FolderModel;
using FileManagementProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileManagementProject.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FolderController : ControllerBase
{
    private readonly IFolderRepository _folderRepository;

    public FolderController(IFolderRepository folderRepository)
    {
        _folderRepository = folderRepository;
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetFolderById(Guid id)
    {
        try
        {
            var response = _folderRepository.GetFolderById(id);

            if (response == null) return NotFound();

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateFolder([FromBody] CreateFolderRequest createFolderRequest)
    {
        try
        {
            var name = User.FindFirst("name")?.Value;

            createFolderRequest.ModifiedBy = name ?? User.Identity.Name ?? "Unknown";

            var folder = _folderRepository.CreateFolder(createFolderRequest);

            return StatusCode(StatusCodes.Status201Created, folder);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateFolder(Guid id, [FromBody] UpdateFolderRequest updateFolderRequest)
    {
        try
        {
            var name = User.FindFirst("name")?.Value;

            updateFolderRequest.ModifiedBy = name ?? User.Identity.Name ?? "Unknown";

            var folder = _folderRepository.UpdateFolder(id, updateFolderRequest);

            if (folder == null) return NotFound();

            return Ok(folder);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteFolder(Guid id)
    {
        try
        {
            var folder = _folderRepository.DeleteFolder(id);

            if (folder == null) return NotFound();

            return Ok(folder);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}