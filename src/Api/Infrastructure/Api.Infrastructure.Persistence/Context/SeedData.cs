using Api.Core.Domain.Models;
using Bogus;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreateDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.EmailAddress, x => x.Internet.Email())
                .RuleFor(x => x.UserName, x => x.Internet.UserName())
                .RuleFor(x => x.UserName, x => x.Internet.UserName())
                .RuleFor(x => x.Password, x => PasswordEncryptor.Encrypt(x.Internet.Password()))
                .RuleFor(x => x.EmailConfirmed, x => x.PickRandom(true, false))
                .Generate(500);

            return result;
        } 

        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();

            dbContextBuilder.UseSqlServer(configuration["SourDictionaryDbConntectionString"]);

            var context = new SourDictionaryContext(dbContextBuilder.Options);

            var users = GetUsers();
            var userIds = users.Select(x => x.Id);

            await context.Users.AddRangeAsync(users);

            var guids = Enumerable.Range(0, 150).Select(x => Guid.NewGuid()).ToList();
            int counter = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(x => x.Id, guids[counter++])
                .RuleFor(x => x.CreateDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Subject, x => x.Lorem.Sentence(5, 5))
                .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
                .RuleFor(x => x.CreatedById, x => x.PickRandom(userIds))
                .Generate(150);

            await context.Entries.AddRangeAsync(entries);
        }


    }
}
