namespace EasyShare.Application.Common.Interfaces.Services;

public interface IFileService
{
    Task<string> SaveImageAsync(
        Stream fileStream, 
        string fileName, 
        CancellationToken cancellationToken);

    Task DeleteImageAsync(
        string imageUrl, 
        CancellationToken cancellationToken);
}
