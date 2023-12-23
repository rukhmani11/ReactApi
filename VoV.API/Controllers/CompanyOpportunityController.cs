using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("CompanyOpportunity")]
    public class CompanyOpportunityController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private ICompanyOpportunityService _companyOpportunityService;
        #endregion

        #region Constructor
        public CompanyOpportunityController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                ICompanyOpportunityService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _companyOpportunityService = businessSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(CompanyOpportunityDTO model)
        {
            if (_companyOpportunityService.IsCompanyOpportunityExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "CompanyOpportunity already exists." });
            }
            model.CreatedById = currentUser.Id;
           // model.CreatedById = Guid.Empty;
            Guid id = await _companyOpportunityService.AddCompanyOpportunity(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(CompanyOpportunityDTO model)
        {
            if (_companyOpportunityService.IsCompanyOpportunityExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "CompanyOpportunity already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _companyOpportunityService.EditCompanyOpportunity(model);
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
            var res = await _companyOpportunityService.GetAllCompanyOpportunitys();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{companyOpportunityId}")]
        public async Task<ActionResult> Delete(Guid companyOpportunityId)
        {
            var isDeleted = await _companyOpportunityService.DeleteCompanyOpportunity(companyOpportunityId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted record." });
        }

        [Route("GetById/{companyOpportunityId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid companyOpportunityId)
        {
            var data = await _companyOpportunityService.GetCompanyOpportunityById(companyOpportunityId);
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
            List<SelectListDTO> res = _companyOpportunityService.GetCompanyOpportunitySelectList();
            return Ok(res);
        }

        [Route("GetAllByCompanyId/{companyId}")]
        [HttpGet]
        public async Task<IActionResult> GetByCompanyId(Guid companyId)
        {
            var res = await _companyOpportunityService.GetCompanyOpportunitiesByCompanyId(companyId);
            return Ok(new { isSuccess = true, list = res });
        }

        [Route("GetByClientEmployeeId/{clientEmployeeId}")]
        [HttpGet]
        public async Task<IActionResult> GetByClientEmployeeId(Guid clientEmployeeId)
        {
            var res = await _companyOpportunityService.GetCompanyOpportunitiesByClientEmployeeId(clientEmployeeId);
            return Ok(new { isSuccess = true, list = res });
        }
        #endregion
    }
}

