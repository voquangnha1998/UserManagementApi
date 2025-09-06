using UserApi.DTOs;
using UserApi.Repositories;
using UserApi.Services;

namespace UserApiTest.Services
{
    public class AuthServiceTests
    {
        private readonly IUserService _service;

        public AuthServiceTests()
        {
            var repo = new InMemoryUserRepository();
            _service = new UserService(repo);
            _service.CreateAsync(new UserCreateDto
            {
                Name = "TestUser",
                Email = "test@gmail.com",
                Password = "MySecret123",
                Title = "Tester"
            }).Wait();
        }

        [Fact]
        public async Task SignIn_WithCorrectCredentials_ShouldReturnToken()
        {
            var token = await _service.SignInAsync("test@gmail.com", "MySecret123");
            Assert.NotNull(token);
            Assert.StartsWith("fake-jwt-token-for-", token);
        }

        [Fact]
        public async Task SignIn_WithWrongPassword_ShouldReturnNull()
        {
            var token = await _service.SignInAsync("test@gmail.com", "WrongPassword");
            Assert.Null(token);
        }

        [Fact]
        public async Task SignIn_WithUnknownEmail_ShouldReturnNull()
        {
            var token = await _service.SignInAsync("unknown@gmail.com", "MySecret123");
            Assert.Null(token);
        }
    }

}
