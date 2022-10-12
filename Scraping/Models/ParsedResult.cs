namespace Scraping.Models
{
    public class ParsedResult
    {
        public string? Country { get; set; }

        public string? TotalCases { get; set; }

        public string? NewCases { get; set; }
        
        public string? TotalDeaths { get; set; }
        
        public string? NewDeaths { get; set; }

        public string? TotalRecovered { get; set; }
    }
}
