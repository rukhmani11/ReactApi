using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IBusinessSubSegmentService : IDisposable
    {
        Task<Guid> AddBusinessSubSegment(BusinessSubSegmentDTO model);

        Task<Guid?> EditBusinessSubSegment(BusinessSubSegmentDTO model);
        Task<List<BusinessSubSegmentDTO>> GetAllBusinessSubSegment();
        Task<bool> DeleteBusinessSubSegment(Guid id);

       Task<BusinessSubSegmentDTO> GetBusinessSubSegmentById(Guid id);

       // Task<List<BusinessSubSegmentDTO>> GetbusinessSubSegmentBybusinessSegmentId(Guid businessSegmentId);

        bool IsBusinessSubSegmentExists(string name, Guid id, Guid businessSegmentId);

        List<SelectListDTO> GetBusinessSubSegmentSelectList();

        List<SelectListDTO> GetbusinessSubSegmentBybusinessSegmentId(Guid businessSegmentId);


    }
}
