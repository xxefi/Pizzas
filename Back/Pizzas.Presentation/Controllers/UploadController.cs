using Microsoft.AspNetCore.Mvc;
using Pizzas.Core.Abstractions.Services.Main;

namespace Pizzas.Presentation.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly IAzureBlobStorageService _blobService;

    public UploadController(IAzureBlobStorageService blobService)
        => _blobService = blobService;
    
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл пустой");

        await using var stream = file.OpenReadStream();
        var fileUrl = await _blobService.UploadFileAsync(stream, file.FileName, file.ContentType);

        return Ok(new { url = fileUrl });
    }
}