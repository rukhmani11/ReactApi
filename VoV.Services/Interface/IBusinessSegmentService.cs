using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IBusinessSegmentService : IDisposable
    {
        Task<Guid> AddBusinessSegment(BusinessSegmentDTO model);
        Task<Guid?> EditBusinessSegment(BusinessSegmentDTO model);
        Task<List<BusinessSegmentDTO>> GetAllBusinessSegments();
        Task<bool> DeleteBusinessSegment(Guid id);        
        Task<BusinessSegmentDTO> GetBusinessSegmentById(Guid id);
        bool IsBusinessSegmentExists(string code, Guid id);
        List<SelectListDTO> GetBusinessSegmentSelectList();
    }
}
