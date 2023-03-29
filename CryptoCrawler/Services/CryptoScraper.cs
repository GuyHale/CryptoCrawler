using CryptoCrawler.Helpers;
using CryptoCrawler.Interfaces;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Reflection;
using WebDriverManager.DriverConfigs.Impl;

namespace CryptoCrawler.Services
{
    public class CryptoScraper : ICryptoScraper
    {
     
        private readonly IConfiguration _configuration;
        private readonly string _url;
        private ChromeDriver _driver;

        public CryptoScraper(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration.GetValue<string>("ScraperUrl") ?? string.Empty;
            _driver = SetDriver();
        }

        public IEnumerable<string[]> WebScraper()
        {
            LoadPage(_driver, _url);
            _driver.Navigate().GoToUrl(_url);
            return ResolveXpaths(GetXpaths());
        }

        private string[] GetXpaths()
        {
            string rankXpath = "//tbody[contains(@data-target, 'currencies.contentBox')]/descendant::td[contains(@class, 'table-number')]";
            string nameXpath = "//tbody[contains(@data-target, 'currencies.contentBox')]/descendant::a/span[contains(@class, 'lg:tw-flex')]";
            string priceXpath = "//tbody[contains(@data-target, 'currencies.contentBox')]/descendant::td[contains(@class, 'td-price')]/div/div/span";
            string marketCapXpath = "//tbody[contains(@data-target, 'currencies.contentBox')]/descendant::td[contains(@class, 'td-market_cap')]/span";

            return new[] {rankXpath, nameXpath, priceXpath, marketCapXpath};
        }

        private ChromeDriver CreateDriver()
        {
            //ChromeOptions options = new()
            //{
            //    BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            //};
            //options.AddArgument(LookUps.SeleniumWebDriverOptions.Headless.Description());
            return new ChromeDriver();
        }

        private void LoadPage(ChromeDriver driver, string url, TimeSpan? timeout = null)
        {
            driver.Url = url;
            WebDriverWait wait = new(driver, timeout ?? new TimeSpan(0, 0, 30));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        private IEnumerable<string[]> ResolveXpaths(string[] xpaths)
        {
            List<string[]> results = new();
            foreach (string xpath in xpaths)
            {
                var items = _driver.FindElements(By.XPath(xpath));
                var data = items.Select(x => x.Text).ToArray();
                results.Add(data);
            }
            _driver.Quit();
            return results;
        }

        private ChromeDriver SetDriver() => _driver ??= CreateDriver();
    }
}
