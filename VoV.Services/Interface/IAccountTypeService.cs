using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IAccountTypeService : IDisposable
    {
        Task<Guid> AddAccountType(AccountTypeDTO model);
        Task<Guid?> EditAccountType(AccountTypeDTO model);
        Task<List<AccountTypeDTO>> GetAllAccountTypes();
        Task<bool> DeleteAccountType(Guid id);
        Task<AccountTypeDTO> GetAccountTypeById(Guid id);
        bool IsAccountTypeExists(string name, Guid id);
        List<SelectListDTO> GetAccountTypeSelectList();
    }
}
