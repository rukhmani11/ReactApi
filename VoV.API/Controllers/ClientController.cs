using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{

    [Route("Client")]
    public class ClientController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IClientService _clientService;
        #endregion

        #region Constructor
        public ClientController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IClientService clientService) : base(contextAccessor)
        {
            _context = context;
            _clientService = clientService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ClientDTO model)
        {
            if (_clientService.IsClientExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "GetClientSelectList already exists." });
            }
           model.CreatedById = currentUser.Id;
           // model.CreatedById = Guid.Empty;
            Guid id = await _clientService.AddClient(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(ClientDTO model)
        {
            if (_clientService.IsClientExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "GetClientSelectList already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _clientService.EditClient(model);
            if (id == null || id == Guid.Empty)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated record." });
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _clientService.GetAllClient();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{clientId}")]
        public async Task<ActionResult> Delete(Guid clientId)
        {
            var isDeleted = await _clientService.DeleteClient(clientId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetById/{clientId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid clientId)
        {
            var data = await _clientService.GetClientbyId(clientId);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }


        [Route("GetClientbyIdCIF/{CIFNo}")]
        [HttpGet]
        public async Task<IActionResult> GetClientbyIdCIF(string CIFNo)
        {
            var data = await _clientService.GetClientbyIdCIF(CIFNo);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }

        [Route("GetSelectList")]
        [HttpGet]
        public IActionResult GetSelectList()
        {
            List<SelectListDTO> res = _clientService.GetClientSelectList();
            return Ok(res);
        }
        [Route("GetClientsByCompanyId/{companyId}")]
        [HttpGet]
        public async Task<IActionResult> GetClientsByCompanyId(Guid companyId)
        {
            var res = await _clientService.GetClientsByCompanyId(companyId);
            return Ok(new { isSuccess = true, list = res });
        }

        [Route("GetPageTitle")]
        [HttpPost]
        public async Task<IActionResult> GetPageTitle(ClientsTitleDTO model)
        {
            var res = await _clientService.GetPageTitle(model);
            return Ok(res);
        }
        #endregion
    }
}

