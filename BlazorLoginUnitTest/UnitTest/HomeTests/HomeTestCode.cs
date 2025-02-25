using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using BlazorLoginTest.Data;

public class AuthenticationTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<AuthenticationStateProvider> _authStateProviderMock;

    public AuthenticationTests()
    {
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        _authStateProviderMock = new Mock<AuthenticationStateProvider>();
    }

    [Fact]
    public async Task AuthenticatedUserCode()
    {
        // Arrange
        var claims = new[] { new Claim(ClaimTypes.Name, "testuser") };
        var identity = new ClaimsIdentity(claims, "testAuthType");
        var user = new ClaimsPrincipal(identity);
        var authState = new AuthenticationState(user);

        _authStateProviderMock.Setup(a => a.GetAuthenticationStateAsync()).ReturnsAsync(authState);

        // Act
        var isAuthenticated = user.Identity.IsAuthenticated;

        // Assert
        Assert.True(isAuthenticated);
    }

    [Fact]
    public async Task AuthenticatedUserAdminCode()
    {
        // Arrange
        var user = new ApplicationUser { UserName = "adminuser" };
        _userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        // Act
        var isAdmin = await _userManagerMock.Object.IsInRoleAsync(user, "Admin");

        // Assert
        Assert.True(isAdmin);
    }
    [Fact]
    public async Task NotAuthenticatedUserCode()
    {
        // Arrange
        var identity = new ClaimsIdentity(); // No claims, not authenticated
        var user = new ClaimsPrincipal(identity);
        var authState = new AuthenticationState(user);

        _authStateProviderMock.Setup(a => a.GetAuthenticationStateAsync()).ReturnsAsync(authState);

        // Act
        var isAuthenticated = user.Identity.IsAuthenticated;

        // Assert
        Assert.False(isAuthenticated);
    }
}