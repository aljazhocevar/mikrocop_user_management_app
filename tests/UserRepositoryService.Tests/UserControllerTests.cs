using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Text;
using System.Net;
using System;

namespace UserRepositoryService.Tests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public UserControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Add("X-Api-Key", "testApiKey");
        }

        private async Task<Guid> CreateTestUser()
        {
            var create = new
            {
                UserName = "Test",
                FullName = "Test Aljaz",
                Email = "aljaz@example.com",
                Mobile = "041863446",
                Language = "SI",
                Culture = "si-SI",
                Password = "P@ssw0rd"
            };
            var resp = await _client.PostAsync("/api/users", new StringContent(JsonSerializer.Serialize(create), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();

            var body = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(body);
            return doc.RootElement.GetProperty("id").GetGuid();
        }

        [Fact]
        public async Task Create_User()
        {
            var id = await CreateTestUser();
            Assert.NotEqual(Guid.Empty, id);
        }

        [Fact]
        public async Task Get_User()
        {
            var id = await CreateTestUser();
            var resp = await _client.GetAsync($"/api/users/{id}");
            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        }

        [Fact]
        public async Task Update_User()
        {
            var id = await CreateTestUser();
            var update = new
            {
                UserName = "test User Name",
                FullName = "test Full Name",
                Email = "test@eemail.com",
                Mobile = "041863446",
                Language = "HR",
                Culture = "hr-HR"
            };
            var resp = await _client.PutAsync($"/api/users/{id}", new StringContent(JsonSerializer.Serialize(update), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.NoContent, resp.StatusCode);
        }

        [Fact]
        public async Task Delete_User()
        {
            var id = await CreateTestUser();
            var resp = await _client.DeleteAsync($"/api/users/{id}");
            Assert.Equal(HttpStatusCode.NoContent, resp.StatusCode);

            var get = await _client.GetAsync($"/api/users/{id}");
            Assert.Equal(HttpStatusCode.NotFound, get.StatusCode);
        }

        [Fact]
        public async Task Validate_UserPassword_Correct()
        {
            var id = await CreateTestUser();

            var payload = new
            {
                password = "P@ssw0rd"
            };

            var resp = await _client.PostAsync(
                $"/api/users/{id}/validate-password",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );

            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

            var body = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(body);
            var isValid = doc.RootElement.GetProperty("valid").GetBoolean();
            Assert.True(isValid, "Password should be valid");
        }

        [Fact]
        public async Task Validate_UserPassword_Incorrect()
        {
            var id = await CreateTestUser();

            var payload = new
            {
                password = "WrongPassword123"
            };

            var resp = await _client.PostAsync(
                $"/api/users/{id}/validate-password",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );

            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

            var body = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(body);
            var isValid = doc.RootElement.GetProperty("valid").GetBoolean();
            Assert.False(isValid, "Password should be invalid");
        }
    }
}
