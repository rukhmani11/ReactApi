using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface ICompanyService : IDisposable
    {
        Task<Guid> AddCompany(CompanyDTO model, IFormFileCollection httpRequestFiles);
        Task<Guid?> EditCompany(CompanyDTO model, IFormFileCollection httpRequestFiles);
        Task<List<CompanyDTO>> GetAllCompany();
        Task<bool> DeleteCompany(Guid id);
        Task<CompanyDTO> GetCompanyById(Guid id);
        Task<Tuple<bool, string>> ActivateOrDeActivate(Guid id, Guid currentUserId);
        bool IsCompanyExists(string name, Guid id);
        List<SelectListDTO> GetCompanySelectList();
    }
}
