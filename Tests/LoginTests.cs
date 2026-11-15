using Microsoft.Playwright;
using QaAutomationTests.Pages;
using static Microsoft.Playwright.Assertions;

namespace QaAutomationTests.Tests;

/// UI functional tests for SauceDemo authentication flows.
public class LoginTests : IAsyncLifetime
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IPage _page = null!;
    private LoginPage _loginPage = null!;
    private ProductsPage _productsPage = null!;

    /// Initializes an isolated browser instance and page objects before each test.
    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        _page = await _browser.NewPageAsync();
        _loginPage = new LoginPage(_page);
        _productsPage = new ProductsPage(_page);
    }

    /// Closes the browser and disposes Playwright resources after each test.
    public async Task DisposeAsync()
    {
        if (_browser is not null)
        {
            await _browser.DisposeAsync();
        }

        _playwright?.Dispose();
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShouldRedirectToProductsPage()
    {
        // Arrange
        await _loginPage.OpenAsync();

        // Act
        await _loginPage.LoginAsync("standard_user", "secret_sauce");

        // Assert
        await Expect(_page).ToHaveURLAsync(ProductsPage.PageUrl);
        await Expect(_productsPage.Title).ToBeVisibleAsync();
        await Expect(_productsPage.InventoryList).ToBeVisibleAsync();

        string actualTitle = await _productsPage.GetTitleTextAsync();
        Assert.Equal("Products", actualTitle);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ShouldShowErrorMessageAndStayOnLoginPage()
    {
        // Arrange
        await _loginPage.OpenAsync();

        // Act
        await _loginPage.LoginAsync("invalid_user", "wrong_password");

        // Assert
        await Expect(_loginPage.ErrorMessage).ToBeVisibleAsync();
        await Expect(_page).ToHaveURLAsync(LoginPage.PageUrl);
        await Expect(_loginPage.LoginButton).ToBeVisibleAsync();

        string errorText = await _loginPage.GetErrorMessageTextAsync();
        Assert.Contains("Username and password do not match any user in this service", errorText);
    }
}
