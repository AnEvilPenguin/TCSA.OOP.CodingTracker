using Bogus;
using TCSA.OOP.CodingTracker.Model;

namespace TCSA.OOP.CodingTracker.Controllers;

internal class SampleDataController
{
    internal IEnumerable<Session> GenerateSessions(int quantity)
    {
        var faker = GetBaseFaker()
            .RuleFor(s => s.Started, f => f.Date.Past())
            .RuleFor(
                s=> s.Finished, 
                (f,s) => f.Date.Between(s.Started, s.Started.AddDays(3)));
        
        return faker.Generate(quantity);
    }

    internal IEnumerable<Session> GenerateOpenSessions(int quantity)
    {
        var faker = GetBaseFaker()
            .RuleFor(s => s.Started, f => f.Date.Recent());
        
        return faker.Generate(quantity);
    }

    private Faker<Session> GetBaseFaker() =>
        new Faker<Session>()
            .RuleFor(s => s.Name, 
                f => $"{f.Hacker.Noun()}-{f.Commerce.Product()}");
}