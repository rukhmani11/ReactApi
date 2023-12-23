//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using VoV.Data.DTOs;

//namespace VoV.Services.Interface
//{
//    public interface IStandardObservationService : IDisposable
//    {
//        Task<Guid> AddStandardObservation(StandardObservationDTO model);
//        Task<Guid?> EditStandardObservation(StandardObservationDTO model);
//        Task<List<StandardObservationDTO>> GetAllStandardObservations();
//        Task<bool> DeleteStandardObservation(Guid id);
//        Task<StandardObservationDTO> GetStandardObservationById(Guid id);
//        bool IsStandardObservationExists(string name, Guid id);
//        List<SelectListDTO> GetStandardObservationSelectList();
//    }
//}

