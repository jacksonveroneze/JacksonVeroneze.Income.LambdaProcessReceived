using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.SQSEvents;
using JacksonVeroneze.Income.LambdaProcessReceived.Util.Builders;

namespace JacksonVeroneze.Income.LambdaProcessReceived.UnitTests;

public class FunctionTest
{
    [Fact(DisplayName = nameof(Function)
                        + nameof(Function.FunctionHandler)
                        + " : FunctionHandler - Success")]
    public async Task Function_FunctionHandler_Success()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        SQSEvent sqsEvent = SqsEventBuilder.BuildSingle();

        TestLambdaLogger logger = new();
        TestLambdaContext context = new()
        {
            Logger = logger
        };

        Function function = new();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        await function.FunctionHandler(sqsEvent, context);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        logger.Buffer.ToString().Should()
            .Contain("Processed message");
    }
}