
namespace ClientAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }


        [HttpPost("add")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<GeneralResponse<int>>> CreateClient([FromBody] AddClientDTO request)
        {
            GeneralResponse<int> response = await _clientRepository.CreateClient(request);
            return ResponseHelper.GetStatusResponseWData(response);
        }

        [HttpGet("paginated")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<PaginatedTableResponse<ClientDTO>>> GetClientsPaginated([FromQuery] GetPaginatedDTO request)
        {
            PaginatedTableResponse<ClientDTO> response = await _clientRepository.GetClientsPaginated(request);
            return response.Count > 0 ? Ok(response) : NoContent();
        }

        [HttpGet("details/{clientId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<GeneralResponse<ClientDetailsDTO>>> GetClientDetails(int clientId)
        {
            GeneralResponse<ClientDetailsDTO> response = await _clientRepository.GetClientDetails(clientId);
            return ResponseHelper.GetStatusResponseWData(response);
        }

        [HttpPut("details")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<GeneralResponse<object>>> UpdateClientDetails([FromBody] EditDetailsDTO request)
        {
            GeneralResponse<object> response = await _clientRepository.UpdateClientDetails(request);
            return ResponseHelper.GetStatusResponse(response);
        }

        [HttpDelete("underage")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<GeneralResponse<object>>> DeleteUnderAgeClients()
        {
            GeneralResponse<object> response = await _clientRepository.DeleteUnderAgeClients();
            return ResponseHelper.GetStatusResponse(response);
        }
    }
}
