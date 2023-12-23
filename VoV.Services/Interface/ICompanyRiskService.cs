using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface ICompanyRiskService : IDisposable
    {
        Task<Guid> AddCompanyRisk(CompanyRiskDTO model);
        Task<Guid?> EditCompanyRisk(CompanyRiskDTO model);
        Task<List<CompanyRiskDTO>> GetAllCompanyRisks();
        Task<bool> DeleteCompanyRisk(Guid id);
        Task<CompanyRiskDTO> GetCompanyRiskById(Guid id);
        bool IsCompanyRiskExists(string name, Guid id);
        List<SelectListDTO> GetCompanyRiskSelectList();
        Task<List<CompanyRiskDTO>> GetCompanyRisksByCompanyId(Guid CompanyId);
        Task<List<spGetCompanyRisksOfClientEmployeeResult>> GetCompanyRisksByClientEmployeeId(Guid clientEmployeeId);
    }
}
