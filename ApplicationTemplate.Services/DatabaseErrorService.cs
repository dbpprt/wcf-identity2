using System;
using ApplicationTemplate.Infrastructure.Common;
using ApplicationTemplate.Infrastructure.Contracts;
using ApplicationTemplate.Models.Entities.Common;
using ApplicationTemplate.ServiceContracts;

namespace ApplicationTemplate.Services
{
    public class DatabaseErrorService : ServiceBase<DatabaseErrorService>, IErrorService
    {
        private readonly Lazy<IRepository<Error>> _errors;

        private IRepository<Error> Errors { get { return _errors.Value; } } 

        public DatabaseErrorService(
            IUnitOfWork context, 
            Lazy<IRepository<Error>> errors)
            : base(context)
        {
            _errors = errors;
        }

        public void LogError(Guid id, string hostName, string typeName, string source, string message, string user, int statusCode,
            DateTime time, string xml)
        {
            Errors.Insert(new Error
            {
                Id = id,
                HostName = hostName,
                TypeName = typeName,
                Source = source,
                Message = message,
                User = user,
                StatusCode = statusCode,
                TimeWritten = time,
                ErrorXml = xml
            });
        }
    }
}
