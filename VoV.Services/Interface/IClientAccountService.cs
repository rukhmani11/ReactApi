using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IClientAccountService : IDisposable
    {
        Task<Guid> AddClientAccount(ClientAccountDTO model);
        Task<Guid?> EditClientAccount(ClientAccountDTO model);
        Task<List<ClientAccountDTO>> GetAllClientAccount();
        Task<bool> DeleteClientAccount(Guid id);
        Task<ClientAccountDTO> GetClientAccountById(Guid id);
        bool IsClientAccountExists(string name, Guid id);
        List<SelectListDTO> GetClientAccountSelectList();

        Task<List<ClientAccountDTO>> GetClientAccountByClientId(Guid clientId);

       
    }
}
