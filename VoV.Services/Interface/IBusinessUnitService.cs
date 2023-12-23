using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IBusinessUnitService : IDisposable
    {
        Task<Guid> AddBusinessUnit(BusinessUnitDTO model);
        Task<Guid?> EditBusinessUnit(BusinessUnitDTO model);
        Task<List<BusinessUnitDTO>> GetAllBusinessUnit();
        Task<bool> DeleteBusinessUnit(Guid id);
        Task<BusinessUnitDTO> GetBusinessUnitById(Guid id);
        bool IsBusinessUnitExists(string name, Guid id);
        List<SelectListDTO> GetBusinessUnitSelectList();

        Task<List<BusinessUnitDTO>> GetBusinessUnitByCompanyId(Guid CompanyId);

        Task<List<SelectListDTO>> GetParentBusinessUnitSelectList(Guid? id, Guid companyId);
    }
}
