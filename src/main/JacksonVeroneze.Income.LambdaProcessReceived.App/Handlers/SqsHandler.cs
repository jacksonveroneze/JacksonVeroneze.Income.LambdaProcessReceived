using Amazon.Lambda.SQSEvents;
using BatchItemFailure = Amazon.Lambda.SQSEvents.SQSBatchResponse.BatchItemFailure;

namespace JacksonVeroneze.Income.LambdaProcessReceived.App.Handlers;

public class SqsHandler : ISqsHandler
{
    public async Task<SQSBatchResponse> HandleAsync(SQSEvent evnt,
        CancellationToken cancellationToken)
    {
        var tasks = evnt.Records.Select(InternalRunAsync);

        var result = await Task.WhenAll(tasks);

        var failures = result.Where(item => !item.Success)
            .Select(item => item.Message)
            .ToList();

        return new SQSBatchResponse(failures);
    }

    private async Task<(bool Success, BatchItemFailure? Message)> InternalRunAsync(
        SQSEvent.SQSMessage message)
    {
        try
        {
            if (message.Body.Contains("Error", 
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Message contains word error");
            }

            await Task.CompletedTask;

            return (true, null);
        }
        catch
        {
            return (false, new BatchItemFailure
            {
                ItemIdentifier = message.MessageId
            });
        }
    }
}