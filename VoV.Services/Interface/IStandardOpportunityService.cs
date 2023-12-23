using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IStandardOpportunityService : IDisposable
    {
        Task<Guid> AddStandardOpportunity(StandardOpportunityDTO model);
        Task<Guid?> EditStandardOpportunity(StandardOpportunityDTO model);
        Task<List<StandardOpportunityDTO>> GetAllStandardOpportunities();
        Task<bool> DeleteStandardOpportunity(Guid id);
        Task<StandardOpportunityDTO> GetStandardOpportunityById(Guid id);
        bool IsStandardOpportunityExists(string name, Guid id);
        List<SelectListDTO> GetStandardOpportunitySelectList();
    }
}
