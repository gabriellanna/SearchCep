using Microsoft.AspNetCore.Mvc;
using SearchCep.Domain.Dtos.Address;
using SearchCep.Domain.Interfaces.Service;
using SearchCep.Domain.ViewModel;

namespace SearchCep.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        [HttpGet("GetCep/{cep}")]
        public async Task<ActionResult<AddressResponseDto>> GetCepAsync([FromRoute] string cep)
        {
            try
            {
                return Ok(new ResponseViewModel(true, null, await _service.GetCep(cep)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(false, ex.Message, null));
            }
        }
    }
}