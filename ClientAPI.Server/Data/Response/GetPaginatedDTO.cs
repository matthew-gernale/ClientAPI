
namespace ClientAPI.Server.Response
{
    public class GetPaginatedDTO
    {
        public int Take { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public string? SearchValue { get; set; }
        public Guid? UserId { get; set; } 

        public UserRoles? UserRoles { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int ClientId { get; set; }   
    }
}
