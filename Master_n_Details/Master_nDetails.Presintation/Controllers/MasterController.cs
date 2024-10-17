

using Entities.Exceptions;
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

        [HttpGet("MasterByNumber")]
        public async Task<IActionResult> MasterByNumber(string number)
        {
            var master = await _service.MasterService.GetMasterByName(number, trackChanges: false);
            return Ok(master);
        }

        [HttpPost(Name = "CreateMaster")]

        public async Task<IActionResult> CreateMaster([FromBody] MasterForCreatingDto master)
        {
            try
            {
                var createdMaster = await _service.MasterService.CreateMasterAsync(master);
               return CreatedAtRoute("MasterByNumber", new { number = createdMaster.Number }, createdMaster);

            }
            catch (BusinessException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMaster(Guid id)
        {
            await _service.MasterService.DeleteMasterAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMaster(Guid id, [FromBody] MasterForUpdateDto master)
        {

            try
            {
                await _service.MasterService.UpdateMasterAsync(id, master, trackChanges: true);
                return NoContent();
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
