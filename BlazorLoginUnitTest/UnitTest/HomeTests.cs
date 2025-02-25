using BlazorLoginTest.Components.Pages;
using BlazorLoginTest.Data;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BlazorLoginUnitTest.UnitTest
{
    public class HomeTests : TestContext
    {
        [Fact]
        public void NotAuthenticatedUser()
        {
            // Arrange
            var authContext = this.AddTestAuthorization();
            authContext.SetNotAuthorized();

            // Act
            var cut = RenderComponent<Home>();

            // Assert
            cut.MarkupMatches("        <div>\r\n            <p>You are NOT logged in</p>\r\n        </div>");
        }
        [Fact]
        public void AuthenticatedUser()
        {
            // Arrange
            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorized("user");

            // Act
            var cut = RenderComponent<Home>();

            // Assert
            cut.MarkupMatches("        <div>\r\n            <p>You are logged in as User</p>\r\n        </div>");
        }
        [Fact]
        public void AuthenticatedUserAdmin()
        {
            // Arrange
            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorized("user");
            authContext.SetRoles("Admin");

            // Act
            var cut = RenderComponent<Home>();

            // Assert
            cut.MarkupMatches("        <div>\r\n            <p>You are logged in as User</p>\r\n        </div>        <div>\r\n            <p>You are logged in as Admin</p>\r\n        </div>");
        }
    }
}