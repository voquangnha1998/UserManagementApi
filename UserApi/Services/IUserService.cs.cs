using UserApi.DTOs;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<(IEnumerable<User>, int)> SearchAsync(string? query, int page, int pageSize);
        Task<(bool Success, string? Error)> CreateAsync(UserCreateDto dto);
        Task<(bool Success, string? Error)> UpdateAsync(Guid id, UserUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<string?> SignInAsync(string email, string password);
    }
}
