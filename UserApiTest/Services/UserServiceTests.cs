using UserApi.DTOs;
using UserApi.Repositories;
using UserApi.Services;

namespace UserApiTest.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _service;

        public UserServiceTests()
        {
            var repo = new InMemoryUserRepository();
            _service = new UserService(repo);
        }

        [Fact]
        public async Task CreateUser_ShouldSucceed()
        {
            var dto = new UserCreateDto
            {
                Name = "Alice",
                Title = "Dev",
                Email = "alice@example.com",
                Password = "123456"
            };

            var (success, error) = await _service.CreateAsync(dto);
            Assert.True(success);
            Assert.Null(error);
        }

        [Fact]
        public async Task CreateUser_WithDuplicateEmail_ShouldFail()
        {
            var dto1 = new UserCreateDto { Name = "Bob", Email = "bob@example.com", Password = "abc" };
            var dto2 = new UserCreateDto { Name = "Bobby", Email = "bob@example.com", Password = "xyz" };

            await _service.CreateAsync(dto1);
            var (success, error) = await _service.CreateAsync(dto2);

            Assert.False(success);
            Assert.Equal("Email already exists", error);
        }

        [Fact]
        public async Task Search_ShouldReturnPagedResults()
        {
            for (int i = 0; i < 10; i++)
            {
                await _service.CreateAsync(new UserCreateDto
                {
                    Name = $"User{i}",
                    Email = $"user{i}@test.com"
                });
            }

            var (results, total) = await _service.SearchAsync("user", page: 2, pageSize: 5);
            Assert.Equal(5, results.Count());
            Assert.Equal(20, total);
        }
    }

}
