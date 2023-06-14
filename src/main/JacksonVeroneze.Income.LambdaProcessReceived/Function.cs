using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace JacksonVeroneze.Income.LambdaProcessReceived;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {
    }

    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
    /// to respond to SQS messages.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<SQSBatchResponse> FunctionHandler(
        SQSEvent evnt,
        ILambdaContext context)
    {
        SQSBatchResponse response = new()
        {
            BatchItemFailures = new List<SQSBatchResponse.BatchItemFailure>()
        };

        context.Logger.LogCritical($"Received Message of count {evnt.Records.Count}");

        foreach (SQSEvent.SQSMessage message in evnt.Records)
        {
            try
            {
                await ProcessMessageAsync(message, context);
            }
            catch (Exception e)
            {
                context.Logger.LogError(e.Message);

                response.BatchItemFailures.Add(new SQSBatchResponse.BatchItemFailure()
                {
                    ItemIdentifier = message.MessageId
                });
            }
        }

        int failures = response.BatchItemFailures.Count;
        int success = evnt.Records.Count - failures;

        context.Logger.LogCritical($"Status - Success: {success} - Error: {failures}");

        return response;
    }

    private async Task ProcessMessageAsync(
        SQSEvent.SQSMessage message,
        ILambdaContext context)
    {
        context.Logger.LogInformation($"Process message ID: {message.MessageId}");

        if (message.Body.Contains("Error", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("Message contains word error");
        }

        await Task.CompletedTask;
    }
}