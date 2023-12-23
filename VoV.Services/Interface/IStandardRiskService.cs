using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IStandardRiskService : IDisposable
    {
        Task<Guid> AddStandardRisk(StandardRiskDTO model);
        Task<Guid?> EditStandardRisk(StandardRiskDTO model);
        Task<List<StandardRiskDTO>> GetAllStandardRisks();
        Task<bool> DeleteStandardRisk(Guid id);
        Task<StandardRiskDTO> GetStandardRiskById(Guid id);
        bool IsStandardRiskExists(string name, Guid id);
        List<SelectListDTO> GetStandardRiskSelectList();
    }
}

