using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IRoleService : IDisposable
    {
        Task<Guid> AddRole(RoleDTO model);
        Task<Guid?> EditRole(RoleDTO model);
        Task<List<RoleDTO>> GetAllRoles();
        Task<bool> DeleteRole(Guid id);
        Task<RoleDTO> GetRoleById(Guid id);
        bool IsRoleExists(string name, Guid id);
        List<SelectListDTO> GetRoleSelectList(string currentUserRoles);
    }
}

