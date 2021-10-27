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
        { '1', ( "Gen a cat", async () => await http.PostAsJsonAsync(baseUri + "/api/fun/cat/generate", new { }) ) },
        { '2', ( "Gen a dog", async () => await http.PostAsJsonAsync(baseUri + "/api/fun/dog/generate", new { }) ) },
        { '3', ( "Groom pets", async () => await http.PostAsJsonAsync(baseUri + "/api/fun/pets/groom", new { }) ) },
        { '4', ( "Play with pets", async () => await http.PostAsJsonAsync(baseUri + "/api/fun/pets/play", new { }) ) }
    };

    ShowOptions(options);

    // execution loop
    bool ok = true;
    do
    {
        try
        {
            ok = await TakeAction(options);
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Error: " + ex.Message);
            Console.WriteLine("----------------------------------------");
        }
    }
    while (ok);
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