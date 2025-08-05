
namespace ClientAPI.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1:1 Relationship
            modelBuilder.Entity<User>()
                .HasOne(user => user.Client)
                .WithOne(client => client.User)
                .HasForeignKey<Client>(client => client.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Users
            var adminId = Guid.NewGuid();
            var client1Id = Guid.NewGuid();
            var client2Id = Guid.NewGuid();
            var client3Id = Guid.NewGuid();

            AuthUtil.CreatePasswordHash("Admin2025", out var adminHash, out var adminSalt);
            AuthUtil.CreatePasswordHash("Client.CRUZ", out var client1Hash, out var client1Salt);
            AuthUtil.CreatePasswordHash("Client.VERA", out var client2Hash, out var client2Salt);
            AuthUtil.CreatePasswordHash("Client.OPEZ", out var client3Hash, out var client3Salt);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    Email = "admin@gmail.com",
                    FirstName = "System",
                    MiddleName = "Admin",
                    LastName = "User",
                    Password = adminHash,
                    PasswordSalt = adminSalt,
                    Role = UserRoles.ADMIN,
                    IsActive = true,
                    RefreshToken = "",
                    RefreshTokenCreatedAt = TimeUtils.PHTime(),
                    RefreshTokenExpiresAt = TimeUtils.PHTime().AddDays(7)
                },
                new User
                {
                    Id = client1Id,
                    Email = "anna@gmail.com",
                    FirstName = "Anna",
                    MiddleName = "Gerona",
                    LastName = "Cruz",
                    Password = client1Hash,
                    PasswordSalt = client1Salt,
                    Role = UserRoles.CLIENT,
                    IsActive = true,
                    RefreshToken = "",
                    RefreshTokenCreatedAt = TimeUtils.PHTime(),
                    RefreshTokenExpiresAt = TimeUtils.PHTime().AddDays(7)
                },
                new User
                {
                    Id = client2Id,
                    Email = "mark@gmail.com",
                    FirstName = "Mark",
                    MiddleName = "Tan",
                    LastName = "Rivera",
                    Password = client2Hash,
                    PasswordSalt = client2Salt,
                    Role = UserRoles.CLIENT,
                    IsActive = true,
                    RefreshToken = "",
                    RefreshTokenCreatedAt = TimeUtils.PHTime(),
                    RefreshTokenExpiresAt = TimeUtils.PHTime().AddDays(7)
                },
                new User
                {
                    Id = client3Id,
                    Email = "sofie@gmail.com",
                    FirstName = "Sofia",
                    MiddleName = "Go",
                    LastName = "Lopez",
                    Password = client3Hash,
                    PasswordSalt = client3Salt,
                    Role = UserRoles.CLIENT,
                    IsActive = true,
                    RefreshToken = "",
                    RefreshTokenCreatedAt = TimeUtils.PHTime(),
                    RefreshTokenExpiresAt = TimeUtils.PHTime().AddDays(7)
                }
            );

            // Seed Clients
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    UserId = client1Id,
                    Birthday = new DateTime(1990, 2, 14),
                    SavingsDeposit = 1200m
                },
                new Client
                {
                    Id = 2,
                    UserId = client2Id,
                    Birthday = new DateTime(1985, 6, 1),
                    SavingsDeposit = 25000m
                },
                new Client
                {
                    Id = 3,
                    UserId = client3Id,
                    Birthday = new DateTime(1975, 11, 23),
                    SavingsDeposit = 3000m
                }
            );
        }



        public DbSet<User> Users => Set<User>();
        public DbSet<Client> Clients => Set<Client>();
    }
}
