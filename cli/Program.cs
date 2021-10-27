using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .AddEnvironmentVariables()
    .Build();

var baseUri = config["Endpoints:apigw"];

using (var http = new HttpClient())
{
    // options
    var options = new Dictionary<char, (string, Func<Task<HttpResponseMessage>>)>
    {
        { '1', ( "Gen Pets", async () => await http.PostAsJsonAsync(baseUri + "/api/fun/pets/generate", new GenPetsDto(100)) ) }
    };

    ShowOptions(options);

    // execution loop
    while (await TakeAction(options)) { }
}

Console.WriteLine("Bye!");

static void ShowOptions(IDictionary<char, (string, Func<Task<HttpResponseMessage>>)> options)
{
    Console.WriteLine();
    Console.WriteLine("Which scenario to execute?: ");
    foreach (var k in options.Keys)
    {
        Console.WriteLine($"{k}) {options[k].Item1}");
    }
    Console.WriteLine();
    Console.WriteLine("To quit press 'q'.");
    Console.WriteLine();
}

static async Task<bool> TakeAction(IDictionary<char, (string, Func<Task<HttpResponseMessage>>)> options)
{
    var key = Console.ReadKey();
    switch (key.KeyChar)
    {
        case 'q':
            return false;
        default:
            if (options.ContainsKey(key.KeyChar))
            {
                var action = options[key.KeyChar].Item2;
                HttpResponseMessage resp = await action();
                Console.WriteLine($" - {resp.StatusCode}");
            }
            else
                Console.WriteLine(" - Sorry, inappropriate choice. Only 'q' and scenario number allowed.");
            return true;
    }
}

record GenPetsDto(double value);