using System.Collections.Concurrent;
using UserApi.Models;

namespace UserApi.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<Guid, User> _users = new();
        public InMemoryUserRepository()
        {
            // Seed data
            for (int i = 1; i <= 10; i++)
            {
                var user = new User
                {
                    Name = $"User {i}",
                    Title = $"Title {i}",
                    Email = $"user{i}@gmail.com",
                    PasswordHash = "rl3rgi4NcZkpAEcacZnQ2VuOfJ0FxAqCRaKB/SwdZoQ=",
                };
                _users[user.Id] = user;
            }
        }
        public Task<IEnumerable<User>> GetAllAsync() =>
            Task.FromResult(_users.Values.Where(u => !u.IsDeleted));

        public Task<User?> GetByIdAsync(Guid id) =>
            Task.FromResult(_users.TryGetValue(id, out var user) && !user.IsDeleted ? user : null);

        public Task<User?> GetByEmailAsync(string email) =>
            Task.FromResult(_users.Values.FirstOrDefault(u => u.Email == email && !u.IsDeleted));

        public Task AddAsync(User user)
        {
            _users[user.Id] = user;
            return Task.CompletedTask;
        }

        public Task<bool> UpdateAsync(User user)
        {
            if (!_users.ContainsKey(user.Id)) return Task.FromResult(false);
            _users[user.Id] = user;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            if (!_users.TryGetValue(id, out var user)) return Task.FromResult(false);
            user.IsDeleted = true;
            return Task.FromResult(true);
        }
    }

}
