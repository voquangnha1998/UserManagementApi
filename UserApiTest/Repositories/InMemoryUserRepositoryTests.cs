using UserApi.Models;
using UserApi.Repositories;

namespace UserApiTest.Repositories
{
    public class InMemoryUserRepositoryTests
    {
        private readonly InMemoryUserRepository _repo = new();

        [Fact]
        public async Task AddUser_ShouldStoreUser()
        {
            var user = new User { Name = "Test", Email = "test@example.com" };
            await _repo.AddAsync(user);

            var result = await _repo.GetByIdAsync(user.Id);
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public async Task DeleteUser_ShouldMarkAsDeleted()
        {
            var user = new User { Name = "ToDelete", Email = "del@example.com" };
            await _repo.AddAsync(user);

            var deleted = await _repo.DeleteAsync(user.Id);
            var result = await _repo.GetByIdAsync(user.Id);

            Assert.True(deleted);
            Assert.Null(result);
        }
    }

}
