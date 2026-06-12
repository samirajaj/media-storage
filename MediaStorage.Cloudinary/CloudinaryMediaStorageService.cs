using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediaStorage.Abstractions.Exceptions;
using MediaStorage.Abstractions.Interfaces;
using MediaStorage.Abstractions.Models;
using Microsoft.Extensions.Options;

namespace MediaStorage.Cloudinary;

public sealed class CloudinaryMediaStorageService : IMediaStorageService
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public CloudinaryMediaStorageService(IOptions<CloudinaryOptions> options)
    {
        var config = options.Value;

        var account = new Account(
            config.CloudName,
            config.ApiKey,
            config.ApiSecret
        );

        _cloudinary = new CloudinaryDotNet.Cloudinary(account)
        {
            Api = { Secure = config.Secure }
        };
    }

    public async Task<MediaUploadResult> UploadAsync(
        MediaUploadRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request.FileStream == null || request.FileStream.Length == 0)
            throw new MediaStorageException("File stream is empty.");

        try
        {
            var uploadParams = BuildUploadParams(request);

            var result = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

            if (result.Error != null)
                throw new MediaStorageException(result.Error.Message);

            return new MediaUploadResult
            {
                PublicId = result.PublicId,
                Url = result.SecureUrl?.ToString() ?? result.Url?.ToString() ?? string.Empty,
                MediaType = request.MediaType
            };
        }
        catch (Exception ex) when (ex is not MediaStorageException)
        {
            throw new MediaStorageException("Failed to upload media to Cloudinary.", ex);
        }
    }

    private static ImageUploadParams BuildUploadParams(MediaUploadRequest request)
    {
        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(request.FileName);

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(request.FileName, request.FileStream),
            PublicId = fileNameWithoutExt,
            UseFilename = true,
            UniqueFilename = true,
            Overwrite = false
        };

        if (!string.IsNullOrWhiteSpace(request.Folder))
        {
            uploadParams.Folder = request.Folder;
        }

        return uploadParams;
    }

    public async Task DeleteAsync(
        string publicId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            throw new MediaStorageException("PublicId is required.");

        var result = await _cloudinary.DestroyAsync(new DeletionParams(publicId));

        if (result.Result != "ok")
            throw new MediaStorageException($"Failed to delete resource: {result.Result}");
    }

    public async Task<MediaUploadResult> ReplaceAsync(string publicId, MediaUploadRequest request, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(publicId, cancellationToken);
        return await UploadAsync(request, cancellationToken);
    }
}
