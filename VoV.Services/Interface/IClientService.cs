using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IClientService : IDisposable
    {
        Task<Guid> AddClient(ClientDTO model);
        Task<Guid?> EditClient(ClientDTO model);
        Task<List<ClientDTO>> GetAllClient();
        Task<bool> DeleteClient(Guid id);
        Task<ClientDTO> GetClientbyId(Guid id);

        Task<ClientDTO> GetClientbyIdCIF(string CIFNo);
        bool IsClientExists(string name, Guid id);
        List<SelectListDTO> GetClientSelectList();

        Task<List<ClientDTO>> GetClientsByCompanyId(Guid CompanyId);

        Task<ClientsTitleDTO> GetPageTitle(ClientsTitleDTO model);


    }
}
