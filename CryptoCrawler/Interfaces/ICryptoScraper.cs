namespace CryptoCrawler.Interfaces
{
    public interface ICryptoScraper
    {
        IEnumerable<string[]> WebScraper();
    }
}
