using BlazorLoginTest.Components.Pages;
using BlazorLoginTest.Data;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BlazorLoginUnitTest.UnitTest.HomeTests
{
    public class HomeTestView : TestContext
    {
        [Fact]
        public void NotAuthenticatedUserView()
        {
            // Arrange
            var authContext = this.AddTestAuthorization();
            authContext.SetNotAuthorized();

            // Act
            var cut = RenderComponent<Home>();

            // Assert
            cut.MarkupMatches("        <div>\r\n            <p>You are NOT logged in</p>\r\n        </div>         <div>\r\n            <p>You are NOT logged in as Admin</p>\r\n        </div>");
        }
        [Fact]
        public void AuthenticatedUserView()
        {
            // Arrange
            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorized("user");

            // Act
            var cut = RenderComponent<Home>();

            // Assert
            cut.MarkupMatches("        <div>\r\n            <p>You are logged in as User</p>\r\n        </div>         <div>\r\n            <p>You are NOT logged in as Admin</p>\r\n        </div>");
        }
        [Fact]
        public void AuthenticatedUserAdminView()
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