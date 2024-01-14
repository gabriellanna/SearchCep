using Microsoft.AspNetCore.Mvc;
using Cep.Domain.Dtos.Address;
using Cep.Domain.Interfaces.Service;
using Cep.Domain.ViewModel;
using Cep.Domain.Models;

namespace Cep.Api.Controllers
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

        [HttpGet("GetAllState")]
        public async Task<ActionResult<IList<State>>> GetAllStateAsync()
        {
            try
            {
                return Ok(new ResponseViewModel<IList<State>>(true, null, await _service.GetAllState()));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(false, ex.Message, null));
            }
        }
    }
}