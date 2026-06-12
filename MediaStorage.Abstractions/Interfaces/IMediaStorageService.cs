using MediaStorage.Abstractions.Models;

namespace MediaStorage.Abstractions.Interfaces;

public interface IMediaStorageService
{
    Task<MediaUploadResult> UploadAsync(
        MediaUploadRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string publicId,
        CancellationToken cancellationToken = default);

    Task<MediaUploadResult> ReplaceAsync(
        string publicId,
        MediaUploadRequest request,
        CancellationToken cancellationToken = default);
}
