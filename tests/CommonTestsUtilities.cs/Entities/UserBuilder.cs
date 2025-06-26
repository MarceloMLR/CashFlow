using Bogus;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CommonTestsUtilities.cs.Cryptography;

namespace CommonTestsUtilities.cs.Entities
{
    public class UserBuilder
    {
        public static User Build(string role = Roles.TEAM_MEMBER)
        {
            var passwordEncripter = new PasswordEncripterBuilder().Build();

            var user = new Faker<User>()
                .RuleFor(u => u.Id, _ => 1)
                .RuleFor(u => u.Name, faker => faker.Person.FirstName)
                .RuleFor(u => u.Email, (faker, user) => faker.Internet.Email(user.Name))
                .RuleFor(user => user.Password, (_, user) => passwordEncripter.Encrypt(user.Password))
                .RuleFor(u => u.Guid, _ => Guid.NewGuid())
                .RuleFor(u => u.Role, _ => role);

            return user;
        }
    }
}
