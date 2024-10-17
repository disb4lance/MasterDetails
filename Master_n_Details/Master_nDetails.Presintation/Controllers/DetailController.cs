

using Microsoft.AspNetCore.Mvc;
using Service;
using Shared.DataTransferObjects;

namespace Master_n_Details.Presintation.Controllers
{
    [Route("api/masters/{masterId}/details")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        private readonly IServiceManager _service;

        public DetailController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailsForMaster(Guid masterId)
        {
            var result = await _service.DetailService.GetDetailsAsync(masterId, trackChanges: false);
            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "GetDetailForMaster")]
        public async Task<IActionResult> GetDetailForMaster(Guid masterId, Guid id)
        {
            var detail = await _service.DetailService.GetDetailsAsync(masterId, trackChanges: false);
            return Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetailForMaster(Guid masterId, [FromBody] DetailForCreatingDto detail)
        {
            var detailToReturn = await _service.DetailService.CreateDetailForMasterAsync(masterId, detail, trackChanges: true);
            return CreatedAtRoute("GetDetailForMaster", new { masterId, id = detailToReturn.Id }, detailToReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDetailForMaster(Guid masterId, Guid id)
        {
            await _service.DetailService.DeleteDetailForMasterAsync(masterId, id, trackChanges: true);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateDeteilForMastery(Guid masterId, Guid id, [FromBody] DetailForUpdateDto detail)
        {
            await _service.DetailService.UpdateDetailForMasterAsync(masterId, id, detail,
                 masterTrackChanges: true, detailTrackChanges: true);

            return NoContent();
        }
    }
}
