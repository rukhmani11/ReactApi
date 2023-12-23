using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface ILocationService : IDisposable
    {
        Task<Guid> AddLocation(LocationDTO model);
        Task<Guid?> EditLocation(LocationDTO model);
        Task<List<LocationDTO>> GetAllLocations();
        Task<bool> DeleteLocation(Guid id);
        Task<LocationDTO> GetLocationById(Guid id);
        bool IsLocationExists(string name, Guid id);
        List<SelectListDTO> GetLocationSelectList();

        Task<List<LocationDTO>> GetLocationByCompanyId(Guid CompanyId);

        Task<List<SelectListDTO>> GetParentLocationSelectList(Guid? id, Guid CompanyId);
    }
}

