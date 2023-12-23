//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using VoV.Data.DTOs;

//namespace VoV.Services.Interface
//{
//    public interface ICompanyObservationService : IDisposable
//    {
//        Task<Guid> AddCompanyObservation(CompanyObservationDTO model);
//        Task<Guid?> EditCompanyObservation(CompanyObservationDTO model);
//        Task<List<CompanyObservationDTO>> GetAllCompanyObservations();
//        Task<bool> DeleteCompanyObservation(Guid id);
//        Task<CompanyObservationDTO> GetCompanyObservationById(Guid id);
//        bool IsCompanyObservationExists(string name, Guid id);
//        List<SelectListDTO> GetCompanyObservationSelectList();

//        Task<List<CompanyObservationDTO>> GetCompanyObservationByCompanyId(Guid CompanyId);
//    }
//}
