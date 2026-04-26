using EasyShare.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;

namespace EasyShare.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        this._environment = environment;
    }

    public async Task<string> SaveImageAsync(
        Stream fileStream, 
        string fileName, 
        CancellationToken cancellationToken)
    {
        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        var uploadsFolder = Path.Combine(this._environment.WebRootPath, "images", "items");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var outputStream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(outputStream, cancellationToken);
        }

        return $"/images/items/{uniqueFileName}";
    }
}
