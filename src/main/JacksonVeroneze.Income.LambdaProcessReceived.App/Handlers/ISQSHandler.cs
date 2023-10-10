using Amazon.Lambda.SQSEvents;

namespace JacksonVeroneze.Income.LambdaProcessReceived.App.Handlers;

public interface ISqsHandler
{
    Task<SQSBatchResponse> HandleAsync(SQSEvent evnt, 
        CancellationToken cancellationToken);
}