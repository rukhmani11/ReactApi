using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IDesignationService : IDisposable
    {
        Task<Guid> AddDesignation(DesignationDTO model);
        Task<Guid?> EditDesignation(DesignationDTO model);
        Task<List<DesignationDTO>> GetAllDesignations();
        Task<bool> DeleteDesignation(Guid id);
        Task<DesignationDTO> GetDesignationById(Guid id);
        bool IsDesignationExists(string name, Guid id);
        List<SelectListDTO> GetDesignationSelectList();

        Task<List<DesignationDTO>> GetDesignationByCompanyId(Guid CompanyId);

        Task<List<SelectListDTO>> GetParentDesignationsSelectList(Guid? id, Guid companyId);
    }
}
