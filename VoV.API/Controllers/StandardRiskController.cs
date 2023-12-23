using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;

namespace VoV.API.Controllers
{

    [Route("StandardRisk")]
    public class StandardRiskController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IStandardRiskService _standardRiskService;
        #endregion

        #region Constructor
        public StandardRiskController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IStandardRiskService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _standardRiskService = businessSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(StandardRiskDTO model)
        {
            if (_standardRiskService.IsStandardRiskExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "StandardRisk already exists." });
            }
            model.CreatedById = currentUser.Id;
            //model.CreatedById = Guid.Empty;
            Guid id = await _standardRiskService.AddStandardRisk(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted Record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(StandardRiskDTO model)
        {
            if (_standardRiskService.IsStandardRiskExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "StandardRisk already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _standardRiskService.EditStandardRisk(model);
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
            var res = await _standardRiskService.GetAllStandardRisks();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{standardRiskId}")]
        public async Task<ActionResult> Delete(Guid standardRiskId)
        {
            var isDeleted = await _standardRiskService.DeleteStandardRisk(standardRiskId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted Record." });
        }

        [Route("GetById/{standardRiskId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid standardRiskId)
        {
            var data = await _standardRiskService.GetStandardRiskById(standardRiskId);
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
            List<SelectListDTO> res = _standardRiskService.GetStandardRiskSelectList();
            return Ok(res);
        }
        #endregion
    }
}

