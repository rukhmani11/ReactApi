using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;

namespace VoV.API.Controllers
{
    [Route("ClientFinancialFile")]
    public class ClientFinancialFileController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IClientFinancialFileService _clientFinancialFileService;
        #endregion

        #region Constructor
        public ClientFinancialFileController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IClientFinancialFileService ClientFinancialFileService) : base(contextAccessor)
        {
            _context = context;
            _clientFinancialFileService = ClientFinancialFileService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ClientFinancialFileDTO model)
        {
            if (_clientFinancialFileService.IsClientFinancialFileExists(model.FileName.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "ClientFinancialFile already exists." });
            }
            model.CreatedById = currentUser.Id;
            Guid id = await _clientFinancialFileService.AddClientFinancialFile(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted data.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(ClientFinancialFileDTO model)
        {
            if (_clientFinancialFileService.IsClientFinancialFileExists(model.FileName.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "ClientFinancialFile already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _clientFinancialFileService.EditClientFinancialFile(model);
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
            var res = await _clientFinancialFileService.GetAllClientFinancialFiles();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{ClientFinancialFileId}")]
        public async Task<ActionResult> Delete(Guid ClientFinancialFileId)
        {
            var isDeleted = await _clientFinancialFileService.DeleteClientFinancialFile(ClientFinancialFileId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted data." });
        }

        [Route("GetById/{ClientFinancialFileId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid ClientFinancialFileId)
        {
            var data = await _clientFinancialFileService.GetClientFinancialFileById(ClientFinancialFileId);
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
            List<SelectListDTO> res = _clientFinancialFileService.GetClientFinancialFileSelectList();
            return Ok(res);
        }
        #endregion
    }
}


