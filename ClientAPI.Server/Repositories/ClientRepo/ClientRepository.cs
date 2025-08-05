
namespace ClientAPI.Server.Repositories.ClientRepo
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor; 
        private const decimal TAX_RATE = 0.005m;

        public ClientRepository(DataContext context,
            IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<GeneralResponse<int>> CreateClient(AddClientDTO request)
        {
            try
            {
                int clientCount = await _context.Clients.CountAsync();
                if (clientCount >= 12)
                    return ResponseHelper.ErrorResponseWData<int>("Maximum number of clients (12) reached.", HttpStatusCode.BadRequest);


                Enum.TryParse(UserUtils.GetUserRole(_contextAccessor), out UserRoles role);
                if (role != UserRoles.ADMIN)
                    return ResponseHelper.ErrorResponseWData<int>("Unauthorized access.", HttpStatusCode.Unauthorized);

                if (await _context.Users.AnyAsync(user => user.Email == request.Email))
                    return ResponseHelper.ErrorResponseWData<int>("Email already exist.", HttpStatusCode.BadRequest);

                if (!TimeUtils.IsAgeInRange(request.DateOfBirth))
                    return ResponseHelper.ErrorResponseWData<int>("Client must be between 18 and 80 years old.", HttpStatusCode.BadRequest);

                if (request.InitialDeposit < 1000)
                    return ResponseHelper.ErrorResponseWData<int>("Minimun deposit is not met.", HttpStatusCode.BadRequest);

                User newUser = CreateUserData(request);
                Client newClient = CreateClientData(request, newUser);

                int result = await _context.SaveChangesAsync();
                return result > 0
                    ? ResponseHelper.SuccessResponseWData(newClient.Id)
                    : ResponseHelper.ErrorResponseWData<int>("Failed to create new client.", HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorResponseWData<int>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private User CreateUserData(AddClientDTO request)
        {
            var suffix = request.LastName.Length >= 4
                    ? request.LastName.Substring(request.LastName.Length - 4)
                    : request.LastName;

            var newPass = "Client." + suffix.ToUpper();

            AuthUtil.CreatePasswordHash(newPass, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                Email = request.Email,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                Role = UserRoles.CLIENT,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                RefreshToken = "",
                RefreshTokenCreatedAt = TimeUtils.PHTime(),
                RefreshTokenExpiresAt = TimeUtils.PHTime().AddDays(7)
            };
            _context.Users.Add(newUser);

            return newUser;
        }

        private Client CreateClientData(AddClientDTO request, User newUser)
        {
            var newClient = new Client
            {
                User = newUser,
                UserId = newUser.Id,
                Birthday = request.DateOfBirth,
                SavingsDeposit = request.InitialDeposit
            };
            _context.Clients.Add(newClient);

            return newClient;
        }

        public async Task<PaginatedTableResponse<ClientDTO>> GetClientsPaginated(GetPaginatedDTO request)
        {
            var response = new PaginatedTableResponse<ClientDTO>(); 
            try
            {
                IQueryable<Client> query = _context.Clients
                    .AsNoTracking()
                    .Where(client => client.User.IsActive)
                    .Include(client => client.User)
                    .OrderBy(client => client.User.FirstName);

                IQueryable<Client>? searchQuery = SearchQuery(request.SearchValue, query);
                if(searchQuery != null) query = searchQuery;

                response.Count = await query.CountAsync();
                if(response.Count == 0) return response;

                List<Client> dbClients = await query
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

                response.ResponseData = dbClients
                    .Select(client => ModelMapper.ToClientDTO(client))
                    .ToList();

                return response;
            }
            catch
            {
                return response;
            }
        }
         
        private IQueryable<Client>? SearchQuery(string? searchValue, IQueryable<Client> query)
        {
            if(string.IsNullOrEmpty(searchValue)) return query;
            return query.Where(client =>
                (client.User.FirstName + " " + client.User.MiddleName + " " + client.User.LastName).Contains(searchValue) ||
                client.User.Email.Contains(searchValue) ||
                client.Id.ToString().Contains(searchValue));
        }

        public async Task<GeneralResponse<ClientDetailsDTO>> GetClientDetails(int clientId)
        {
            try
            {
                Enum.TryParse(UserUtils.GetUserRole(_contextAccessor), out UserRoles role);
                if (role != UserRoles.ADMIN) 
                    return ResponseHelper.ErrorResponseWData<ClientDetailsDTO>("Unauthorized access.", HttpStatusCode.Unauthorized);

                Client? dbClient = await _context.Clients
                    .AsNoTracking()
                    .Where(client => client.Id == clientId)
                    .Include(client => client.User)
                    .FirstOrDefaultAsync();

                if (dbClient == null)
                    return ResponseHelper.ErrorResponseWData<ClientDetailsDTO>("Client details not found.", HttpStatusCode.NotFound);

                return ResponseHelper.SuccessResponseWData(ModelMapper.ToClientDetailsDTO(dbClient));
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorResponseWData<ClientDetailsDTO>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<GeneralResponse<object>> UpdateClientDetails(EditDetailsDTO request)
        {
            try
            {
                Enum.TryParse(UserUtils.GetUserRole(_contextAccessor), out UserRoles role);
                if (role != UserRoles.ADMIN)
                    return ResponseHelper.ErrorResponse("Unauthorized access.", HttpStatusCode.Unauthorized);

                Client? dbClient = await _context.Clients
                    .Where(client => client.Id == request.ClientId)
                    .Include(client => client.User)
                    .FirstOrDefaultAsync();

                if (dbClient == null)
                    return ResponseHelper.ErrorResponse("Client not found.", HttpStatusCode.NotFound);

                if (request.SavingsDeposit < 1000)
                    return ResponseHelper.ErrorResponse("Minimum deposit is not met.", HttpStatusCode.BadRequest);

                if (!TimeUtils.IsAgeInRange(request.DateOfBirth))
                    return ResponseHelper.ErrorResponse("Client must be between 18 and 80 years old.", HttpStatusCode.BadRequest);

                dbClient.User.FirstName = request.FirstName;
                dbClient.User.LastName = request.LastName;
                dbClient.User.MiddleName = request.MiddleName;
                dbClient.Birthday = request.DateOfBirth;
                dbClient.SavingsDeposit = request.SavingsDeposit;

                int result = await _context.SaveChangesAsync();
                return result > 0
                    ? ResponseHelper.SuccessResponse()
                    : ResponseHelper.ErrorResponse("Failed to update this client.", HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorResponse(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<GeneralResponse<object>> DeleteUnderAgeClients()
        {
            try
            {
                Enum.TryParse(UserUtils.GetUserRole(_contextAccessor), out UserRoles role);
                if (role != UserRoles.ADMIN)
                    return ResponseHelper.ErrorResponse("Unauthorized access.", HttpStatusCode.Unauthorized);

                var cutoffDate = TimeUtils.PHTime().AddYears(-18);

                List<Client>? underAgeClients = await _context.Clients
                    .Where(client => client.Birthday > cutoffDate)
                    .Include(client => client.User)
                    .ToListAsync();

                if (underAgeClients == null || underAgeClients.Count == 0)
                    return ResponseHelper.ErrorResponse("No underage clients found.", HttpStatusCode.NotFound);

                _context.Users.RemoveRange(underAgeClients.Select(client => client.User));
                _context.Clients.RemoveRange(underAgeClients);
                int result = await _context.SaveChangesAsync();

                return result > 0
                    ? ResponseHelper.SuccessResponse()
                    : ResponseHelper.ErrorResponse("Failed to delete underage clients.", HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorResponse(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
