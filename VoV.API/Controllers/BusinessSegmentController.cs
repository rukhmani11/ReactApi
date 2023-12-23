using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Services.Interface;

namespace VoV.API.Controllers
{
    [Route("BusinessSegment")]
    public class BusinessSegmentController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IBusinessSegmentService _businessSegmentsService;
        #endregion

        #region Constructor
        public BusinessSegmentController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IBusinessSegmentService businessSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _businessSegmentsService = businessSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(BusinessSegmentDTO model)
        {
            if (_businessSegmentsService.IsBusinessSegmentExists(model.Code.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "BusinessSegment code already exists." });
            }
            model.CreatedById = currentUser.Id;
            Guid id = await _businessSegmentsService.AddBusinessSegment(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted Record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(BusinessSegmentDTO model)
        {
            if (_businessSegmentsService.IsBusinessSegmentExists(model.Code.Trim(), model.Id))
            {
                return BadRequest(new { isSuccess = false, message = "BusinessSegment code already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _businessSegmentsService.EditBusinessSegment(model);
            if (id == null || id == Guid.Empty)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully updated Record." });
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _businessSegmentsService.GetAllBusinessSegments();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{businessSegmentId}")]
        public async Task<ActionResult> Delete(Guid businessSegmentId)
        {
            var isDeleted = await _businessSegmentsService.DeleteBusinessSegment(businessSegmentId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted Record." });
        }

        [Route("GetById/{businessSegmentId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid businessSegmentId)
        {
            var data = await _businessSegmentsService.GetBusinessSegmentById(businessSegmentId);
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
            List<SelectListDTO> res = _businessSegmentsService.GetBusinessSegmentSelectList();
            return Ok(res);
        }
        #endregion
    }
}
