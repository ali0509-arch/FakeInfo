using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using Xunit;

namespace FakeInfo.IntegrationTests;

public class FrontendTests : PageTest
{
    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true
        };
    }

    private const string BaseUrl = "http://localhost:5028/index.html";

    //  Happy path – single person
    [Fact]
    public async Task GeneratePerson_Works()
    {
        await Page.GotoAsync(BaseUrl);

        await Page.ClickAsync("#generatePersonBtn");

        var result = Page.Locator("#singleResult");

        await Expect(result).Not.ToBeEmptyAsync();
        await Expect(result).ToContainTextAsync("CPR");
    }

    //  Happy path – bulk
    [Fact]
    public async Task GenerateBulk_Works()
    {
        await Page.GotoAsync(BaseUrl);

        await Page.FillAsync("#bulkCount", "5");
        await Page.ClickAsync("#generateBulkBtn");

        var result = Page.Locator("#bulkResult");

        await Expect(result).Not.ToBeEmptyAsync();
    }

    //  Boundary – minimum (2)
    [Fact]
    public async Task Bulk_MinBoundary_2_Works()
    {
        await Page.GotoAsync(BaseUrl);

        await Page.FillAsync("#bulkCount", "2");
        await Page.ClickAsync("#generateBulkBtn");

        await Expect(Page.Locator("#bulkResult")).Not.ToBeEmptyAsync();
    }

    //  Invalid – under minimum
    [Fact]
    public async Task Bulk_Invalid_1_Shows_Error()
    {
        await Page.GotoAsync(BaseUrl);

        await Page.FillAsync("#bulkCount", "1");
        await Page.ClickAsync("#generateBulkBtn");

        await Expect(Page.Locator("#errorMessage")).ToBeVisibleAsync();
    }

    //  Invalid – over maximum
    [Fact]
    public async Task Bulk_OverMax_Shows_Error()
    {
        await Page.GotoAsync(BaseUrl);

        await Page.FillAsync("#bulkCount", "101");
        await Page.ClickAsync("#generateBulkBtn");

        await Expect(Page.Locator("#errorMessage")).ToBeVisibleAsync();
    }
}