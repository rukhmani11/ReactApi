using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;

namespace VoV.API.Controllers
{

    [Route("StandardOpportunity")]
    public class StandardOpportunityController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IStandardOpportunityService _standardOpportunitiesService;
        #endregion

        #region Constructor
        public StandardOpportunityController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IStandardOpportunityService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _standardOpportunitiesService = businessSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(StandardOpportunityDTO model)
        {
            if (_standardOpportunitiesService.IsStandardOpportunityExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "StandardOpportunity already exists." });
            }
            model.CreatedById = currentUser.Id;
            Guid id = await _standardOpportunitiesService.AddStandardOpportunity(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted Record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(StandardOpportunityDTO model)
        {
            if (_standardOpportunitiesService.IsStandardOpportunityExists(model.Name.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "StandardOpportunity already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _standardOpportunitiesService.EditStandardOpportunity(model);
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
            var res = await _standardOpportunitiesService.GetAllStandardOpportunities();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{standardOpportunityId}")]
        public async Task<ActionResult> Delete(Guid standardOpportunityId)
        {
            var isDeleted = await _standardOpportunitiesService.DeleteStandardOpportunity(standardOpportunityId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted Record." });
        }

        [Route("GetById/{standardOpportunityId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid standardOpportunityId)
        {
            var data = await _standardOpportunitiesService.GetStandardOpportunityById(standardOpportunityId);
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
            List<SelectListDTO> res = _standardOpportunitiesService.GetStandardOpportunitySelectList();
            return Ok(res);
        }
        #endregion
    }
}

