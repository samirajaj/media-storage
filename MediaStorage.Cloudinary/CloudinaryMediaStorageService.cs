using MediaStorage.Abstractions.Interfaces;
using MediaStorage.Abstractions.Models;

namespace MediaStorage.Cloudinary;

public sealed class CloudinaryMediaStorageService : IMediaStorageService
{
    public Task<MediaUploadResult> UploadAsync(MediaUploadRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string publicId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<MediaUploadResult> ReplaceAsync(string publicId, MediaUploadRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
