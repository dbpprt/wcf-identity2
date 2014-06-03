using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using ApplicationTemplate.Models.DataTransfer;

namespace ApplicationTemplate.ServiceContracts
{
    [ServiceContract]
    public interface IApplicationRoleManager : IDisposable
    {
        [OperationContract]
        Task<ApplicationIdentityResult> CreateAsync(ApplicationRole role);

        [OperationContract]
        Task<ApplicationIdentityResult> DeleteAsync(int roleId);

        [OperationContract]
        Task<ApplicationRole> FindByIdAsync(int roleId);

        [OperationContract]
        Task<ApplicationRole> FindByNameAsync(string roleName);

        [OperationContract]
        Task<IEnumerable<ApplicationRole>> GetRolesAsync();

        [OperationContract]
        Task<bool> RoleExistsAsync(string roleName);

        [OperationContract]
        Task<ApplicationIdentityResult> UpdateAsync(int roleId, string roleName);
    }
}