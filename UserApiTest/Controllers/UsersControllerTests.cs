using Microsoft.AspNetCore.Mvc;
using UserApi.Controllers;
using UserApi.DTOs;
using UserApi.Repositories;
using UserApi.Services;

namespace UserApiTest.Controllers
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            var repo = new InMemoryUserRepository();
            var service = new UserService(repo);
            _controller = new UsersController(service);
        }

        [Fact]
        public async Task Create_ShouldReturn201()
        {
            var dto = new UserCreateDto
            {
                Name = "NewUser",
                Email = "new@gmail.com",
                Password = "secure",
                Title = "Intern"
            };

            var result = await _controller.Create(dto);
            var response = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(201, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            var result = await _controller.GetAll(null, 1, 10);
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(ok.Value);
        }

        [Fact]
        public async Task Delete_NonExistingUser_ShouldReturnNotFound()
        {
            var result = await _controller.Delete(Guid.NewGuid());
            Assert.IsType<NotFoundResult>(result);
        }
    }

}
