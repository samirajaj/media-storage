namespace MediaStorage.Abstractions.Exceptions;

public class MediaStorageException : Exception
{
    public MediaStorageException(string message)
        : base(message)
    {
    }

    public MediaStorageException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
