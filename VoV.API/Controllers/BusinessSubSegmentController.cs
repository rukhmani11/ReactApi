using Microsoft.AspNetCore.Mvc;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Controllers
{
    [Route("BusinessSubSegment")]
    public class BusinessSubSegmentController : BaseApiController
    {
        #region Properties
        private readonly VoVDbContext _context;
        private IBusinessSubSegmentService _businessSubSegmentsService;
        #endregion

        #region Constructor
        public BusinessSubSegmentController(IHttpContextAccessor contextAccessor,
                VoVDbContext context,
                IBusinessSubSegmentService businessSubSegmentsService) : base(contextAccessor)
        {
            _context = context;
            _businessSubSegmentsService = businessSubSegmentsService;
        }
        #endregion

        #region Methods
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(BusinessSubSegmentDTO model)
        {
            if (_businessSubSegmentsService.IsBusinessSubSegmentExists(model.Name.Trim(), model.Id, model.BusinessSegmentId))
            {
                return BadRequest(new { isSuccess = false, message = "BusinessSubSegment already exists." });
            }
            model.CreatedById = currentUser.Id;
            Guid id = await _businessSubSegmentsService.AddBusinessSubSegment(model);
            // string currencyCode = await _businessSubSegmentsService.AddCurrency(model);
            return Ok(new { isSuccess = true, message = "Successfully inserted Record.", id = id });
        }

        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit(BusinessSubSegmentDTO model)
        {
            if (_businessSubSegmentsService.IsBusinessSubSegmentExists(model.Name.Trim(), model.Id, model.BusinessSegmentId))
            {
                return BadRequest(new { isSuccess = false, message = "BusinessSubSegment already exists." });
            }
            model.UpdatedById = currentUser.Id;
            Guid? id = await _businessSubSegmentsService.EditBusinessSubSegment(model);
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
            var res = await _businessSubSegmentsService.GetAllBusinessSubSegment();
            return Ok(new { isSuccess = true, list = res });
        }

        [HttpDelete("{businessSubSegmentId}")]
        public async Task<ActionResult> Delete(Guid businessSubSegmentId)
        {
            var isDeleted = await _businessSubSegmentsService.DeleteBusinessSubSegment(businessSubSegmentId);
            if (!isDeleted)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, message = "Successfully deleted Record." });
        }


        [Route("GetById/{businessSubSegmentId}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid businessSubSegmentId)
        {
            var data = await _businessSubSegmentsService.GetBusinessSubSegmentById(businessSubSegmentId);
            if (data == null)
            {
                return BadRequest(new { isSuccess = false, message = "No record found." });
            }
            return Ok(new { isSuccess = true, data = data });
        }


        [Route("GetbusinessSubSegmentBybusinessSegmentId/{businessSegmentId}")]
        [HttpGet]
        public IActionResult GetbusinessSubSegmentBybusinessSegmentId(Guid businessSegmentId)
        {

            List<SelectListDTO> res = _businessSubSegmentsService.GetbusinessSubSegmentBybusinessSegmentId(businessSegmentId);
            return Ok(res);
        }
        [Route("GetSelectList")]
        [HttpGet]
        public IActionResult GetSelectList()
        {
            List<SelectListDTO> res = _businessSubSegmentsService.GetBusinessSubSegmentSelectList();
            return Ok(res);
        }



        #endregion
    }
}
