using HtmlAgilityPack;
using Newtonsoft.Json;
using Scraping.Models;

var uri = new Uri("https://www.worldometers.info/coronavirus/");

//grab the HTML
Console.WriteLine($"Grabbing HTML for '{uri}'");
HttpClient client = new HttpClient();
var response = await client.GetStringAsync(uri);
Console.WriteLine($"Grabbed HTML for '{uri}'");

//parse the HTML
Console.WriteLine($"Parse HTML for '{uri}'");
var htmlDoc = new HtmlDocument();
htmlDoc.LoadHtml(response);

var table = htmlDoc.GetElementbyId("main_table_countries_today");
var tableBody = table.ChildNodes["tbody"];
var rows = tableBody.Descendants("tr").ToList();

var result = new List<ParsedResult>();
foreach (var row in rows.Skip(7))
{
    var columns = row.Descendants("td").ToList();

    result.Add(
    new ParsedResult
    {
        Country = columns[1].InnerText?.Trim(),
        TotalCases = columns[2].InnerText?.Trim(),
        NewCases = columns[3].InnerText?.Trim(),
        TotalDeaths = columns[4].InnerText?.Trim(),
        NewDeaths = columns[5].InnerText?.Trim(),
        TotalRecovered = columns[6].InnerText?.Trim(),
    }); 
}
Console.WriteLine($"Parsed HTML for '{uri}'");

//save json payload
var fileName = $@"{Path.GetTempPath()}\ScrapingResult_{DateTime.Now.ToFileTime()}.json";
Console.WriteLine($"Saving JSON payload to disk: {fileName}");
File.WriteAllText(fileName, JsonConvert.SerializeObject(result));
Console.WriteLine($"Saved JSON payload to disk: {fileName}");



