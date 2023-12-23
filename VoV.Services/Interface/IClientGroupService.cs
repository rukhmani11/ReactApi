using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IClientGroupService : IDisposable
    {
        Task<Guid> AddClientGroup(ClientGroupDTO model);
        Task<Guid?> EditClientGroup(ClientGroupDTO model);
        Task<List<ClientGroupDTO>> GetAllClientGroups();
        Task<bool> DeleteClientGroup(Guid id);
        Task<ClientGroupDTO> GetClientGroupById(Guid id);
        bool IsClientGroupExists(string name, Guid id);
        List<SelectListDTO> GetClientGroupSelectListByCompanyId(Guid companyId);

       Task<List<ClientGroupDTO>> GetClientGroupByCompanyId(Guid companyId);

        
    }
}