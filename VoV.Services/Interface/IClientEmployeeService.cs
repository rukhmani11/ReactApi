using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IClientEmployeeService : IDisposable
    {
        Task<Guid> AddClientEmployee(ClientEmployeeDTO model);
        Task<Guid?> EditClientEmployee(ClientEmployeeDTO model);
        Task<List<ClientEmployeeDTO>> GetAllClientEmployee();
        Task<bool> DeleteClientEmployee(Guid id);
        Task<ClientEmployeeDTO> GetClientEmployeeById(Guid id);
        bool IsClientEmployeeExists(string name, Guid id);
        List<SelectListDTO> GetClientEmployeeSelectList();

        Task<List<ClientEmployeeDTO>> GetClientEmployeeByclientId(Guid clientId);

        List<SelectListDTO> GetSelectClientEmployeeByclientId(Guid clientId);
    }
}
