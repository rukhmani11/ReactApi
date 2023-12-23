using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IClientBusinessUnitService : IDisposable
    {
        Task<Guid> AddClientBusinessUnit(ClientBusinessUnitDTO model);
        Task<Guid?> EditClientBusinessUnit(ClientBusinessUnitDTO model);
        Task<List<ClientBusinessUnitDTO>> GetAllClientBusinessUnits();
        Task<bool> DeleteClientBusinessUnit(Guid id);
        Task<ClientBusinessUnitDTO> GetClientBusinessUnitById(Guid id);
        bool IsClientBusinessUnitExists(string name, Guid id);
        List<SelectListDTO> GetClientBusinessUnitSelectList();

        List<SelectListDTO> GetSelectListByClientId(Guid clientId);

        Task<List<ClientBusinessUnitDTO>> GetClientBusinessUnitByclientId(Guid clientId);
    }
}
