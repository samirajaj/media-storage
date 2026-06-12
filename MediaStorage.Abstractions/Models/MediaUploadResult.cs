using MediaStorage.Abstractions.Enums;

namespace MediaStorage.Abstractions.Models;

public sealed record MediaUploadResult
{
    public required string PublicId { get; init; }

    public required string Url { get; init; }

    public required MediaType MediaType { get; init; }
}
