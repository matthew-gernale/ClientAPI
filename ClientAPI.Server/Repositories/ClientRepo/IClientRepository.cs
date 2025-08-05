
namespace ClientAPI.Server.Repositories.ClientRepo
{
    public interface IClientRepository
    {
        Task<GeneralResponse<int>> CreateClient(AddClientDTO request);
        Task<PaginatedTableResponse<ClientDTO>> GetClientsPaginated(GetPaginatedDTO request);
        Task<GeneralResponse<ClientDetailsDTO>> GetClientDetails(int clientId);
        Task<GeneralResponse<object>> UpdateClientDetails(EditDetailsDTO request);
        Task<GeneralResponse<object>> DeleteUnderAgeClients();
    }
}
