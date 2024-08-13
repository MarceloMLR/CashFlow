using Bogus;

using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;

namespace CommonTestsUtilities.cs.Entities;


public class ExpenseBuilder
{
    public static List<Expense> Collection(User user,  uint count = 2)
    {
        var list = new List<Expense>();

        if (count == 0)
            count = 1;

        var expenseId = 1;

        for (int i = 0; i < count; i++)
        {
            var expense = Build(user);
            expense.Id = expenseId++;

            list.Add(expense);
        }

        return list;
    }

    public static Expense Build(User user)
    {
        return new Faker<Expense>()
            .RuleFor(x => x.Id, _ => 1)
            .RuleFor(x => x.Title, faker => faker.Commerce.ProductName())
            .RuleFor(x => x.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(x => x.Date, faker => faker.Date.Past())
            .RuleFor(x => x.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(x => x.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(x => x.UserId, _ => user.Id);
    }
}
