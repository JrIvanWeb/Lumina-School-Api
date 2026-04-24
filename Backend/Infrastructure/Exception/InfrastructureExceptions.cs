namespace LuminiSchool.Infrastructure.Exception
{
    public class DatabaseException : System.Exception
    {
        public DatabaseException(string message) : base(message) { }
        public DatabaseException(string message, System.Exception inner) : base(message, inner) { }
    }

    public class FileStorageException : System.Exception
    {
        public FileStorageException(string message) : base(message) { }
    }

    public class ExternalServiceException : System.Exception
    {
        public string ServiceName { get; }
        public ExternalServiceException(string serviceName, string message) : base(message) { ServiceName = serviceName; }
    }
}
