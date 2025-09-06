using UserApi.DTOs;
using UserApi.Models;
using UserApi.Repositories;
using UserApi.Utils;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<(IEnumerable<User>, int)> SearchAsync(string? query, int page, int pageSize)
        {
            var all = await _repo.GetAllAsync();
            var filtered = string.IsNullOrWhiteSpace(query)
                ? all
                : all.Where(u => u.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                              || u.Email.Contains(query, StringComparison.OrdinalIgnoreCase));

            var total = filtered.Count();
            var paged = filtered.Skip((page - 1) * pageSize).Take(pageSize);
            return (paged, total);
        }

        public async Task<(bool, string?)> CreateAsync(UserCreateDto dto)
        {
            if (await _repo.GetByEmailAsync(dto.Email) is not null)
                return (false, "Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Title = dto.Title,
                Email = dto.Email,
                PasswordHash = PasswordHasher.Hash(dto.Password)
            };

            await _repo.AddAsync(user);
            return (true, null);
        }

        public async Task<(bool, string?)> UpdateAsync(Guid id, UserUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return (false, "User not found");

            var emailOwner = await _repo.GetByEmailAsync(dto.Email);
            if (emailOwner is not null && emailOwner.Id != id)
                return (false, "Email already in use");

            existing.Name = dto.Name;
            existing.Title = dto.Title;
            existing.Email = dto.Email;

            var success = await _repo.UpdateAsync(existing);
            return (success, success ? null : "Update failed");
        }

        public Task<bool> DeleteAsync(Guid id) => _repo.DeleteAsync(id);

        public async Task<string?> SignInAsync(string email, string password)
        {
            var abc = PasswordHasher.Hash("testuser1");
            var user = await _repo.GetByEmailAsync(email);
            if (user is null || !PasswordHasher.Verify(password, user.PasswordHash))
                return null;

            return $"fake-jwt-token-for-{user.Id}";
        }
    }

}
