using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;

namespace VoV.API.Controllers
{
    [Route("CompanyRisk")]
    public class CompanyRiskController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private ICompanyRiskService _companyRisksService;
        #endregion

        #region Constructor
        public CompanyRiskController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                ICompanyRiskService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _companyRisksService = businessSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(CompanyRiskDTO model)
        {
            if (_companyRisksService.IsCompanyRiskExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "CompanyRisk already exists." });
            }
            model.CreatedById = currentUser.Id;
            // model.CreatedById = Guid.Empty;
            Guid id = await _companyRisksService.AddCompanyRisk(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted data.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(CompanyRiskDTO model)
        {
            if (_companyRisksService.IsCompanyRiskExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "CompanyRisk already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _companyRisksService.EditCompanyRisk(model);
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
            var res = await _companyRisksService.GetAllCompanyRisks();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{companyRiskId}")]
        public async Task<ActionResult> Delete(Guid companyRiskId)
        {
            var isDeleted = await _companyRisksService.DeleteCompanyRisk(companyRiskId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted data." });
        }

        [Route("GetById/{companyRiskId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid companyRiskId)
        {
            var data = await _companyRisksService.GetCompanyRiskById(companyRiskId);
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
            List<SelectListDTO> res = _companyRisksService.GetCompanyRiskSelectList();
            return Ok(res);
        }

        [Route("GetAllByCompanyId/{companyId}")]
        [HttpGet]
        public async Task<IActionResult> GetBySocietyBuildingId(Guid companyId)
        {
            var res = await _companyRisksService.GetCompanyRisksByCompanyId(companyId);
            return Ok(new { isSuccess = true, list = res });
        }

        [Route("GetByClientEmployeeId/{clientEmployeeId}")]
        [HttpGet]
        public async Task<IActionResult> GetByClientEmployeeId(Guid clientEmployeeId)
        {
            var res = await _companyRisksService.GetCompanyRisksByClientEmployeeId(clientEmployeeId);
            return Ok(new { isSuccess = true, list = res });
        }
        #endregion
    }
}

