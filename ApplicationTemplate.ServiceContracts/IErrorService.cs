using System;

namespace ApplicationTemplate.ServiceContracts
{
    public interface IErrorService
    {
        void LogError(Guid id, string hostName, string typeName, string source, string message, string user,
            int statusCode, DateTime time, string xml);
    }
}
