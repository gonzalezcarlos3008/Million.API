using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Million.API.Controllers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTest
    {
        private AuthController _controller;
        private IConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                { "Jwt:SecretKey", "xvnRG7s0k9@YqPZx!8wL$C3e*D5mO1vNq&r2T6aF!KpWyZ" }
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _controller = new AuthController(_configuration);
        }

        [Test]
        public void Login_ReturnsOkWithToken()
        {
            // Act
            var result = _controller.Login();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var token = okResult.Value!.GetType().GetProperty("token").GetValue(okResult.Value);
            Assert.NotNull(token);
            Assert.IsTrue(token is string);

            // Validate the structure of the token
            var handler = new JwtSecurityTokenHandler();
            Assert.DoesNotThrow(() => handler.ReadJwtToken(token.ToString()));
        }

        [Test]
        public void GenerateJwtToken_ReturnsValidToken()
        {
            // Act
            var token = _controller.GetType()
                .GetMethod("GenerateJwtToken", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(_controller, null) as string;

            // Assert
            Assert.NotNull(token);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.NotNull(jwtToken);
            Assert.Greater(jwtToken.ValidTo, DateTime.UtcNow);
        }
    }
}
