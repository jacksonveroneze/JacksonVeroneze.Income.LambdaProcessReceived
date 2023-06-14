using Amazon.Lambda.SQSEvents;

namespace JacksonVeroneze.Income.LambdaProcessReceived.Util.Builders;

[ExcludeFromCodeCoverage]
public static class SqsEventBuilder
{
    public static SQSEvent BuildSingle()
    {
        return Factory().Generate();
    }

    private static Faker<SQSEvent> Factory()
    {
        return new Faker<SQSEvent>("pt_BR")
            .RuleFor(f => f.Records, SqsMessageBuilder.BuildMany());
    }
}