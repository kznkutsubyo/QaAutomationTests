using Microsoft.Playwright;

namespace QaAutomationTests.Pages;


/// Page Object for the SauceDemo login page.
public class LoginPage
{
    public const string PageUrl = "https://www.saucedemo.com/";

    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    // Use data-test attributes for resilient element selection
    private ILocator UsernameInput => _page.Locator("[data-test='username']");
    private ILocator PasswordInput => _page.Locator("[data-test='password']");
    public ILocator LoginButton => _page.Locator("[data-test='login-button']");
    public ILocator ErrorMessage => _page.Locator("[data-test='error']");

    /// Navigates to the login page.
    public async Task OpenAsync()
    {
        await _page.GotoAsync(PageUrl);
    }

    /// Fills in credentials and submits the login form.
    public async Task LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }

    /// Returns the displayed login error text.
    public async Task<string> GetErrorMessageTextAsync()
    {
        return await ErrorMessage.InnerTextAsync();
    }
}
