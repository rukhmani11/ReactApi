using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IClientFinancialService : IDisposable
    {
        Task<Guid> AddClientFinancial(ClientFinancialDTO model);
        Task<Guid?> EditClientFinancial(ClientFinancialDTO model);
        Task<List<ClientFinancialDTO>> GetAllClientFinancials();
        Task<bool> DeleteClientFinancial(Guid id);
        Task<ClientFinancialDTO> GetClientFinancialById(Guid id);
        bool IsClientFinancialExists(Guid ClientId, Guid FinancialYearId, Guid id);
        List<SelectListDTO> GetClientFinancialSelectList();

        Task<List<ClientFinancialDTO>> GetClientFinancialByclientId(Guid clientId);
    }
}
