using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface ICompanyOpportunityService : IDisposable
    {
        Task<Guid> AddCompanyOpportunity(CompanyOpportunityDTO model);
        Task<Guid?> EditCompanyOpportunity(CompanyOpportunityDTO model);
        Task<List<CompanyOpportunityDTO>> GetAllCompanyOpportunitys();
        Task<bool> DeleteCompanyOpportunity(Guid id);
        Task<CompanyOpportunityDTO> GetCompanyOpportunityById(Guid id);
        bool IsCompanyOpportunityExists(string name, Guid id);
        List<SelectListDTO> GetCompanyOpportunitySelectList();
        Task<List<CompanyOpportunityDTO>> GetCompanyOpportunitiesByCompanyId(Guid CompanyId);
        Task<List<spGetCompanyOpportunitiesOfClientEmployeeResult>> GetCompanyOpportunitiesByClientEmployeeId(Guid clientEmployeeId);
    }
}

