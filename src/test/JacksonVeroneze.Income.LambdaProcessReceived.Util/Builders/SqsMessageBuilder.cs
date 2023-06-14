using Amazon.Lambda.SQSEvents;

namespace JacksonVeroneze.Income.LambdaProcessReceived.Util.Builders;

[ExcludeFromCodeCoverage]
public static class SqsMessageBuilder
{
    public static IList<SQSEvent.SQSMessage> BuildMany(
        int? qtde = null)
    {
        qtde ??= new Random().Next(2, 5);

        return Factory().Generate(qtde.Value);
    }

    private static Faker<SQSEvent.SQSMessage> Factory()
    {
        return new Faker<SQSEvent.SQSMessage>("pt_BR")
            .RuleFor(f => f.Body, s => s.Random.Words());
    }
}