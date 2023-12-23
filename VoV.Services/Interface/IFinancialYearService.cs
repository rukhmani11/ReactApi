using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IFinancialYearService : IDisposable
    {
        Task<Guid> AddFinancialYear(FinancialYearDTO model);
        Task<Guid?> EditFinancialYear(FinancialYearDTO model);
        Task<List<FinancialYearDTO>> GetAllFinancialYears();
        Task<bool> DeleteFinancialYear(Guid id);
        Task<FinancialYearDTO> GetFinancialYearById(Guid id);
        bool IsFinancialYearExists(string name, Guid id);
        List<SelectListDTO> GetFinancialYearSelectList();
        DateTime? GetFinancialYearFromToDate();
    }
}

