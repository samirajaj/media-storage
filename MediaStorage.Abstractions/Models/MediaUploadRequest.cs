using MediaStorage.Abstractions.Enums;

namespace MediaStorage.Abstractions.Models;

public sealed record MediaUploadRequest
{
    public required Stream FileStream { get; init; }

    public required string FileName { get; init; }

    public string? Folder { get; init; }

    public MediaType MediaType { get; init; }
}
