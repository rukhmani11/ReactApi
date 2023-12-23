using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IClientFinancialFileService : IDisposable
    {
        Task<Guid> AddClientFinancialFile(ClientFinancialFileDTO model);
        Task<Guid?> EditClientFinancialFile(ClientFinancialFileDTO model);
        Task<List<ClientFinancialFileDTO>> GetAllClientFinancialFiles();
        Task<bool> DeleteClientFinancialFile(Guid id);
        Task<ClientFinancialFileDTO> GetClientFinancialFileById(Guid id);
        bool IsClientFinancialFileExists(string name, Guid id);
        List<SelectListDTO> GetClientFinancialFileSelectList();
    }
}
