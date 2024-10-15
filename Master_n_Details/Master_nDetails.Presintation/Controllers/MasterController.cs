

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Shared.DataTransferObjects;

namespace Master_n_Details.Presintation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IServiceManager _service;

        public MasterController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet(Name = "GetMasters")]
        public async Task<IActionResult> GetMasters()
        {
            var masters = await _service.MasterService.GetAllMastersAsync(trackChanges: false);
            return Ok(masters);
        }

        [HttpGet("{id:guid}", Name = "MasterById")]
        public async Task<IActionResult> GetMaster(Guid id)
        {
            var master = await _service.MasterService.GetMasterAsync(id, trackChanges: false);
            return Ok(master);
        }

        [HttpPost(Name = "CreateMaster")]

        public async Task<IActionResult> CreateMaster([FromBody] MasterForCreatingDto company)
        {
            var createdMaster = await _service.MasterService.CreateMasterAsync(company);
            return CreatedAtRoute("MasterById", new { id = createdMaster.Id }, createdMaster);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMaster(Guid id)
        {
            await _service.MasterService.DeleteMasterAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMaster(Guid id, [FromBody] MasterForUpdateDto company)
        {
            await _service.MasterService.UpdateMasterAsync(id, company, trackChanges: true);
            return NoContent();
        }

    }
}
