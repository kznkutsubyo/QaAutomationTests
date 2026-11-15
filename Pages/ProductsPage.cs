using Microsoft.Playwright;

namespace QaAutomationTests.Pages;
/// Page Object for the SauceDemo products catalog page.
public class ProductsPage
{
    public const string PageUrl = "https://www.saucedemo.com/inventory.html";

    private readonly IPage _page;

    public ProductsPage(IPage page)
    {
        _page = page;
    }

    // Use data-test='inventory-list' because id='inventory_container' is duplicated in the DOM
    public ILocator Title => _page.Locator("[data-test='title']");
    public ILocator InventoryList => _page.Locator("[data-test='inventory-list']");

    /// Returns the page header text.
    public async Task<string> GetTitleTextAsync()
    {
        return await Title.InnerTextAsync();
    }
}
