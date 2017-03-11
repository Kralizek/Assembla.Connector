<Query Kind="Statements">
  <NuGetReference>Kralizek.Assembla.Connector</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Logging</NuGetReference>
  <Namespace>Kralizek.Assembla</Namespace>
  <Namespace>Kralizek.Assembla.Connector</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

ILoggerFactory loggerFactory = new LoggerFactory();

HttpClient client = new HttpClient { BaseAddress = new Uri(@"https://api.assembla.com") };
client.DefaultRequestHeaders.Add("X-Api-Key", "yourApiKey");
client.DefaultRequestHeaders.Add("X-Api-Secret", "yourSecretKey");

IAssemblaClient assembla = new HttpAssemblaClient(client, loggerFactory.CreateLogger<HttpAssemblaClient>());

var spaces = await assembla.Spaces.GetAllAsync();

var items = spaces.Select(s => new {s.Id, s.WikiName, s.Name});

foreach (var item in items)
{
	Console.WriteLine(item.Name);
	Console.WriteLine($"\tId: {item.Id}");
	Console.WriteLine($"\tWikiName: \t{item.WikiName}");
	Console.WriteLine();
}