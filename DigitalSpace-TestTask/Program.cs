// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Reflection;
using System.Text.Json;
using DigitalSpace_TestTask.Data;
using DigitalSpace_TestTask.Entity;
using DigitalSpace_TestTask.Helpers;

var persons = FakeBackend.GeneratePersonList(10000);
            
var serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
var personsAsJson = JsonSerializer.Serialize(persons, serializerOptions);

var filePath = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\Persons.json";
File.WriteAllText(filePath, personsAsJson);
            
persons = null;
            
persons = JsonSerializer.Deserialize<PersonEntity[]?>(File.ReadAllText(filePath), serializerOptions);
            
var personsCreditCardCount = persons.Sum(p => p.CreditCardNumbers.Length);
var averageChildAge = Math.Round(persons
    .SelectMany(p => p.Children
        .Select(c => UnixConverter.YearsBetween(DateTime.UtcNow, c.BirthDate)))
    .DefaultIfEmpty()
    .Average(), 2);

Console.WriteLine($"Total persons count: {persons.Length}");
Console.WriteLine($"Total persons credit card count: {personsCreditCardCount}");
Console.WriteLine($"Avg child age: {averageChildAge}");
