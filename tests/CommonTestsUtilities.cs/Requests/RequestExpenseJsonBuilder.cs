﻿using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestsUtilities.cs.Requests;


public class RequestExpenseJsonBuilder
{
    public static RequestExpenseJson Build()
    { 
        return new Faker<RequestExpenseJson>()
        .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
        .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
        .RuleFor(r => r.Date, faker => faker.Date.Past())
        .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
        .RuleFor(r => r.Type, faker => faker.PickRandom<PaymentType>());
    }
}
