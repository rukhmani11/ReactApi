//using Microsoft.AspNetCore.Mvc;
//using VoV.Data.Context;
//using VoV.Data.DTOs;
//using VoV.Services.Interface;

//namespace VoV.API.Controllers
//{
//    [Route("StandardObservation")]
//    public class StandardObservationController : BaseApiController
//    {
//        #region Properties
//        private readonly VoVDbContext _context;
//        private IStandardObservationService _standardObservationService;
//        #endregion

//        #region Constructor
//        public StandardObservationController(IHttpContextAccessor contextAccessor,
//                VoVDbContext context,
//                IStandardObservationService businessSegmentsService) : base(contextAccessor)
//        {
//            _context = context;
//            _standardObservationService = businessSegmentsService;
//        }
//        #endregion

//        #region Methods
//        [Route("Add")]
//        [HttpPost]
//        public async Task<IActionResult> Add(StandardObservationDTO model)
//        {
//            if (_standardObservationService.IsStandardObservationExists(model.Name.Trim(), model.Id))
//            {
//                return BadRequest(new { isSuccess = false, message = "StandardObservation already exists." });
//            }
//            model.CreatedById = currentUser.Id;
//            //model.CreatedById = Guid.Empty;
//            Guid id = await _standardObservationService.AddStandardObservation(model);
//            return Ok(new { isSuccess = true, message = "Successfully inserted Record.", id = id });
//        }

//        [Route("Edit")]
//        [HttpPut]
//        public async Task<IActionResult> Edit(StandardObservationDTO model)
//        {
//            if (_standardObservationService.IsStandardObservationExists(model.Name.Trim(), model.Id))
//            {
//                return BadRequest(new { isSuccess = false, message = "StandardObservation already exists." });
//            }
//            model.UpdatedById = currentUser.Id;
//            Guid? id = await _standardObservationService.EditStandardObservation(model);
//            if (id == null || id == Guid.Empty)
//            {
//                return BadRequest(new { isSuccess = false, message = "No record found." });
//            }
//            return Ok(new { isSuccess = true, message = "Successfully updated record." });
//        }

//        [Route("GetAll")]
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var res = await _standardObservationService.GetAllStandardObservations();
//            return Ok(new { isSuccess = true, list = res });
//        }

//        [HttpDelete("{standardObservationId}")]
//        public async Task<ActionResult> Delete(Guid standardObservationId)
//        {
//            var isDeleted = await _standardObservationService.DeleteStandardObservation(standardObservationId);
//            if (!isDeleted)
//            {
//                return BadRequest(new { isSuccess = false, message = "No record found." });
//            }
//            return Ok(new { isSuccess = true, message = "Successfully deleted Record." });
//        }

//        [Route("GetById/{standardObservationId}")]
//        [HttpGet]
//        public async Task<IActionResult> GetById(Guid standardObservationId)
//        {
//            var data = await _standardObservationService.GetStandardObservationById(standardObservationId);
//            if (data == null)
//            {
//                return BadRequest(new { isSuccess = false, message = "No record found." });
//            }
//            return Ok(new { isSuccess = true, data = data });
//        }

//        [Route("GetSelectList")]
//        [HttpGet]
//        public IActionResult GetSelectList()
//        {
//            List<SelectListDTO> res = _standardObservationService.GetStandardObservationSelectList();
//            return Ok(res);
//        }
//        #endregion
//    }
//}
