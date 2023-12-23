using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface ICurrencyService : IDisposable
    {
        Task<string> AddCurrency(CurrencyDTO model);
        Task<string> EditCurrency(CurrencyDTO model);
        Task<List<CurrencyDTO>> GetAllCurrencys();
        Task<bool> DeleteCurrency(string CurrencyCode);
        Task<CurrencyDTO> GetCurrencyBycurrencyCode(string Code);
        bool IsCurrencyExists(string CurrencyName, string CurrencyCode);
        List<SelectListDTO> GetCurrencySelectList();
    }
}


